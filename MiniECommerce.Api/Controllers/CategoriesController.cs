using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Core.DTOs;
using MiniECommerce.Services.Abstracts;

namespace MiniECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody]CategoryDto categoryDto)
        {
            await _categoryService.AddCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.Name }, categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest("ID mismatch.");

            await _categoryService.UpdateCategoryAsync(categoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
