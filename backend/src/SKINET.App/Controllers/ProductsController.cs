using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SKINET.App.Dtos;
using SKINET.App.Errors;
using SKINET.App.Extensions;
using SKINET.Business.Interfaces;
using SKINET.Business.Models;
using SKINET.Data.Specifications;

namespace SKINET.App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductBrand> _productBrandRepository;
        private readonly IRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IRepository<Product> productRepository, IMapper mapper, IRepository<ProductBrand> productBrandRepository,
            IRepository<ProductType> productTypeRepository)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        [Cached(180)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts([FromQuery] ProductParamsSpecification productParams)
        {
            var specifications = new ProductsWithBrandsAndTypesSpecification(productParams);
            var countSpecifications = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await _productRepository.Count(countSpecifications);

            var products = await _productRepository.GetAllWithSpec(specifications);
            var data = _mapper.Map<List<ProductDTO>>(products);

            return Ok(new Pagination<ProductDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [Cached(180)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var specifications = new ProductsWithBrandsAndTypesSpecification(id);

            var product = await _productRepository.GetWithSpec(specifications);

            if (product != null)
            {
                return Ok(_mapper.Map<ProductDTO>(product));
            }

            return NotFound(new ApiResponse(404));
        }

        [Cached(180)]
        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.GetAll());
        }

        [Cached(180)]
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.GetAll());
        }

    }
}
