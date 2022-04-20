using SKINET.Business.Models.OrderAggregate;

namespace SKINET.Business.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string cartId, Address shippingAddress);
        Task<List<Order>> GetAllOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
        Task<List<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
