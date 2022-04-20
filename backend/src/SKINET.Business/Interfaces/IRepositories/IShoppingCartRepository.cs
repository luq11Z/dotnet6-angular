using SKINET.Business.Models;

namespace SKINET.Business.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCart(string shoppingCartId);
        Task<ShoppingCart> CreateOrUpdateShoppingCart(ShoppingCart shoppingCart);
        Task<bool> DeleteShoppingCart(string shoppingCartId);
    }
}
