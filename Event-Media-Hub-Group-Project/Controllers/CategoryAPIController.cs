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

        /// <summary>
        /// Retrieves the list of all categories.
        /// </summary>
        /// <returns>A list of CategoryDto objects representing all categories.</returns>
        [HttpGet("List")]
        public async Task<IEnumerable<CategoryDto>> ListCategories()
        {
            return await _categoryService.ListCategories();
        }

        /// <summary>
        /// Retrieves a specific category by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A CategoryDto object representing the category.</returns>
        [HttpGet("{id}")]
        public async Task<CategoryDto> GetCategory(int id)
        {
            return await _categoryService.GetCategory(id);
        }

        /// <summary>
        /// Creates a new category.
        /// This action requires authorization.
        /// </summary>
        /// <param name="categoryDto">The CategoryDto object containing the information to create a new category.</param>
        /// <returns>A ServiceResponse object containing the result of the operation.</returns>
        [HttpPost("Add")]
        [Authorize]
        public async Task<ServiceResponse> CreateCategory(CategoryDto categoryDto)
        {
            return await _categoryService.CreateCategory(categoryDto);
        }

        /// <summary>
        /// Updates an existing category by its ID.
        /// This action requires authorization.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDto">The CategoryDto object containing the updated information.</param>
        /// <returns>A ServiceResponse object containing the result of the operation.</returns>
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<ServiceResponse> UpdateCategory(int id, CategoryDto categoryDto)
        {
            return await _categoryService.UpdateCategory(id, categoryDto);
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// This action requires authorization.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A ServiceResponse object containing the result of the operation.</returns>
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ServiceResponse> DeleteCategory(int id)
        {
            return await _categoryService.DeleteCategory(id);
        }
    }
}
