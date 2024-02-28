using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_API.Dtos.CategoryDtos;
using RealEstate_API.Repositories.Abstract;

namespace RealEstate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryRepository categoryRepository) : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

        [HttpGet("Categories")]
        public async Task<IActionResult> CategoryList()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                // Eğer categoryDto null ise BadRequest döndür
                return BadRequest("Invalid category data");
            }
            try
            {
                await _categoryRepository.CreateCategory(categoryDto);
                return Ok("Category added!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding category: {ex.Message}");
            }

        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
           await _categoryRepository.DeleteCategory(id);
            return Ok("Category Deleted.");
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDto category)
        {
            await _categoryRepository.UpdateCategory(category);
            return Ok("Category Updated successfully!");
        }

        [HttpGet("GetCategory{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return Ok(category);
        }

    }
}
