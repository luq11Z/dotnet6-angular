using Microsoft.Extensions.Configuration;
using SKINET.Business.Interfaces;
using SKINET.Business.Interfaces.IServices;
using SKINET.Business.Interfaces.Repositories;
using SKINET.Business.Models;
using SKINET.Business.Models.OrderAggregate;
using Stripe;
using Product = SKINET.Business.Models.Product;
using Order = SKINET.Business.Models.OrderAggregate.Order;
using SKINET.Business.Specifications;

namespace SKINET.Business.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IShoppingCartRepository shoppingCartRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<ShoppingCart> CreateOrUpdatePaymentIntent(string shoppingCartId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var shoppingCart = await _shoppingCartRepository.GetShoppingCart(shoppingCartId);

            if (shoppingCart == null)
            {
                return null;
            }

            var shippingPrice = 0m;

            if (shoppingCart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetById((int)shoppingCart.DeliveryMethodId);
                shippingPrice = deliveryMethod.Price;
            }

            foreach (var item in shoppingCart.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetById(item.Id);

                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent intent;

            if (string.IsNullOrEmpty(shoppingCart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)shoppingCart.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options);
                shoppingCart.PaymentIntentId = intent.Id;
                shoppingCart.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)shoppingCart.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                };

                await service.UpdateAsync(shoppingCart.PaymentIntentId, options);
            }

            await _shoppingCartRepository.CreateOrUpdateShoppingCart(shoppingCart);

            return shoppingCart;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpec(spec);

            if (order == null)
            {
                return null;
            }

            order.Status = OrderStatus.PaymentReceived;
            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Complete();

            return order;
        }

        public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpec(spec);

            if (order == null)
            {
                return null;
            }

            order.Status = OrderStatus.PaymentFailed;
            await _unitOfWork.Complete();

            return order;
        }
       
    }
}
