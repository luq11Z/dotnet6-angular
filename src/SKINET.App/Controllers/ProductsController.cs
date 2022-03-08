using Microsoft.AspNetCore.Mvc;
using SKINET.Business.Interfaces;

namespace SKINET.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProductsBrandsTypes();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return "single product";
        }
    }
}
