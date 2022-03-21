using Microsoft.AspNetCore.Mvc;
using SKINET.Business.Interfaces;
using SKINET.Business.Models;

namespace SKINET.App.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCartById(string id)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCart(id);

            return Ok(shoppingCart ?? new ShoppingCart(id));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdateShoppingCart(ShoppingCart shoppingCart)
        {
            var updatedShoppingCart = await _shoppingCartRepository.CreateOrUpdateShoppingCart(shoppingCart);

            return Ok(updatedShoppingCart);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteShoppingCart(string id)
        {
            await _shoppingCartRepository.DeleteShoppingCart(id);

            return NoContent();
        }
    }
}
