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
        private readonly IProductRepository _productRepository;
        private readonly IProductBrandRepository _productBrandRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper, IProductBrandRepository productBrandRepository,
            IProductTypeRepository productTypeRepository)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

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

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var specifications = new ProductsWithBrandsAndTypesSpecification(id);

            var product = await _productRepository.GetWithSpec(specifications);

            if (product != null)
            {
                return _mapper.Map<ProductDTO>(product);
            }

            return NotFound(new ApiResponse(404));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.GetAll());
        }

        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.GetAll());
        }

    }
}
