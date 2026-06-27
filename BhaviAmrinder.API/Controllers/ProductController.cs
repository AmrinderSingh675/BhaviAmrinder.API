using Microsoft.AspNetCore.Mvc;
using BhaviAmrinder.Application.DTOs;
using BhaviAmrinder.Domain.Entities;
using BhaviAmrinder.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using BhaviAmrinder.API.Helpers;

namespace BhaviAmrinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Admin")] To test the role based Auth
    public class ProductController : ControllerBase
    {
        private readonly IGenericService<ProductDto, Product> _productService;

        public ProductController(IGenericService<ProductDto, Product> productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto dto)
        {
            var result = await _productService.CreateAsync(dto);
            return Ok(new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Product created successfully.",
                Data = result
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto dto)
        {
            var result = await _productService.CreateAsync(dto);
            if (result == null)
            {
                return NotFound(new ApiResponse<ProductDto>
                {
                    Success = false,
                    Message = "Product not found.",
                    Data = null
                });
            }
            return Ok(new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Product updated successfully.",
                Data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Product fetched successfully.",
                Data = product
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(new ApiResponse<List<ProductDto>>
            {
                Success = true,
                Message = "Products fetched successfully.",
                Data = products
            });
        }
    }
}