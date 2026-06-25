using Microsoft.AspNetCore.Mvc;
using BhaviAmrinder.Application.DTOs;
using BhaviAmrinder.Domain.Entities;
using BhaviAmrinder.Application.IServices;
using Microsoft.AspNetCore.Authorization;

namespace BhaviAmrinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Admin")]
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
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }
    }
}