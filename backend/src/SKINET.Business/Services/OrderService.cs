using SKINET.Business.Interfaces;
using SKINET.Business.Interfaces.IServices;
using SKINET.Business.Interfaces.Repositories;
using SKINET.Business.Models;
using SKINET.Business.Models.OrderAggregate;
using SKINET.Business.Specifications;
using SKINET.Data.Specifications;

namespace SKINET.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, IShoppingCartRepository shoppingCartRepository, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _shoppingCartRepository = shoppingCartRepository;
            _paymentService = paymentService;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string cartId, Address shippingAddress)
        {
            var shoppinhCart = await _shoppingCartRepository.GetShoppingCart(cartId);
            
            var items = new List<OrderItem>();
            foreach (var item in shoppinhCart.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetById(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.Pictures.FirstOrDefault(p => p.IsMain)?.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetById(deliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var spec = new OrderByPaymentIntentIdSpecification(shoppinhCart.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetWithSpec(spec);

            if (existingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(shoppinhCart.PaymentIntentId);
            }

            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal, shoppinhCart.PaymentIntentId);
            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                return null;
            }

            //await _shoppingCartRepository.DeleteShoppingCart(cartId);

            return order;
        }

        public async Task<List<Order>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().GetAllWithSpec(spec);
        }

        public async Task<List<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAll();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetWithSpec(spec);
        }
    }
}
