using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SKINET.App.Dtos;
using SKINET.App.Errors;
using SKINET.App.Extensions;
using SKINET.Business.Interfaces.Repositories;
using SKINET.Business.Models;
using SKINET.Data.Specifications;

namespace SKINET.App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //[Cached(180)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts([FromQuery] ProductParamsSpecification productParams)
        {
            var specifications = new ProductsWithBrandsAndTypesSpecification(productParams);
            var countSpecifications = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await _unitOfWork.Repository<Product>().Count(countSpecifications);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(specifications);
            var data = _mapper.Map<List<ProductDTO>>(products);

            return Ok(new Pagination<ProductDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        //[Cached(180)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var specifications = new ProductsWithBrandsAndTypesSpecification(id);

            var product = await _unitOfWork.Repository<Product>().GetAllWithSpec(specifications);

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
            return Ok(await _unitOfWork.Repository<ProductBrand>().GetAll());
        }

        [Cached(180)]
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            return Ok(await _unitOfWork.Repository<ProductType>().GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductCreateDto productToCreate)
        {
            if (productToCreate.Price == 0)
            {
                return BadRequest(new ApiResponse(400, "Price must be greater than 0"));
            }

            var product = _mapper.Map<ProductCreateDto, Product>(productToCreate);
            product.PictureUrl = "images/products/placeholder.png";

            _unitOfWork.Repository<Product>().Add(product);

            var result = await _unitOfWork.Complete();

            if (result <= 0) 
            {
                return BadRequest(new ApiResponse(400, "Problem creating product"));
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, ProductCreateDto productToUpdate)
        {
            var product = await _unitOfWork.Repository<Product>().GetById(id);

            if (product != null)
            {
                _mapper.Map(productToUpdate, product);

                _unitOfWork.Repository<Product>().Update(product);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    return BadRequest(new ApiResponse(400, "Problem creating product"));
                }

                return NoContent();
            }

            return NotFound(new ApiResponse(404, "Product not found"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetById(id);

            if (product != null)
            {
                _unitOfWork.Repository<Product>().Delete(product);

                var result = await _unitOfWork.Complete();

                if (result <= 0)
                {
                    return BadRequest(new ApiResponse(400, "Problem creating product"));
                }

                return NoContent();
            }

            return NotFound(new ApiResponse(404, "Product not found"));
        }

    }
}
