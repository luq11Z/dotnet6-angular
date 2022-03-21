using SKINET.Business.Interfaces;
using SKINET.Business.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace SKINET.Data.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDatabase _database;

        public ShoppingCartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<ShoppingCart> GetShoppingCart(string shoppingCartId)
        {
            var data = await _database.StringGetAsync(shoppingCartId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart>(data);
        }

        public async Task<ShoppingCart> CreateOrUpdateShoppingCart(ShoppingCart shoppingCart)
        {
           var created = await _database.StringSetAsync(shoppingCart.Id, JsonSerializer.Serialize(shoppingCart), TimeSpan.FromDays(30));

            if (!created)
            {
                return null;
            }

            return await GetShoppingCart(shoppingCart.Id);
        }

        public async Task<bool> DeleteShoppingCart(string shoppingCartId)
        {
            return await _database.KeyDeleteAsync(shoppingCartId);
        }
    }
}
