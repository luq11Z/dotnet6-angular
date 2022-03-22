using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SKINET.App.Dtos;
using SKINET.Business.Interfaces;
using SKINET.Business.Models;

namespace SKINET.App.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCartById(string id)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCart(id);

            return Ok(shoppingCart ?? new ShoppingCart(id));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> CreateOrUpdateShoppingCart(ShoppingCartDto shoppingCart)
        {
            var cart = _mapper.Map<ShoppingCartDto, ShoppingCart>(shoppingCart);

            var updatedShoppingCart = await _shoppingCartRepository.CreateOrUpdateShoppingCart(cart);

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
