using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryAPIController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryAPIController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("List")]
        public async Task<IEnumerable<CategoryDto>> ListCategories()
        {
            return await _categoryService.ListCategories();
        }

        [HttpGet("{id}")]
        public async Task<CategoryDto> GetCategory(int id)
        {
            return await _categoryService.GetCategory(id);
        }

        [HttpPost("Add")]
        [Authorize]
        public async Task<ServiceResponse> CreateCategory(CategoryDto categoryDto)
        {
            return await _categoryService.CreateCategory(categoryDto);
        }

        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<ServiceResponse> UpdateCategory(int id, CategoryDto categoryDto)
        {
            return await _categoryService.UpdateCategory(id, categoryDto);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ServiceResponse> DeleteCategory(int id)
        {
            return await _categoryService.DeleteCategory(id);
        }
    }
}