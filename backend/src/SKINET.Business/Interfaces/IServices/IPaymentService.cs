using SKINET.Business.Models;
using SKINET.Business.Models.OrderAggregate;

namespace SKINET.Business.Interfaces.IServices
{
    public interface IPaymentService
    {
        Task<ShoppingCart> CreateOrUpdatePaymentIntent(string shoppingCartId);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
