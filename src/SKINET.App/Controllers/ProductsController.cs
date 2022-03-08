using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SKINET.App.Dtos;
using SKINET.Business.Interfaces;
using SKINET.Business.Models;

namespace SKINET.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            var products = await _productRepository.GetProductsBrandsTypes();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductBrandType(id);  
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
