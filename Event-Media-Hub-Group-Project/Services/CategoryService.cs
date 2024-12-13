using Event_Media_Hub_Group_Project.Data;
using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Media_Hub_Group_Project.Services
{
    /// <summary>
    /// Service for managing categories, including CRUD operations.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all categories.
        /// </summary>
        /// <returns>A list of <see cref="CategoryDto"/> representing categories.</returns>
        public async Task<IEnumerable<CategoryDto>> ListCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            var categoryDtos = categories.Select(category => new CategoryDto()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            }).ToList();

            return categoryDtos;
        }

        /// <summary>
        /// Retrieves a single category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A <see cref="CategoryDto"/> representing the category, or null if not found.</returns>
        public async Task<CategoryDto> GetCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return null;
            }

            return new CategoryDto()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">The <see cref="CategoryDto"/> containing category information.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> CreateCategory(CategoryDto categoryDto)
        {
            var serviceResponse = new ServiceResponse();

            var category = new Category()
            {
                CategoryName = categoryDto.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = category.CategoryId;
            serviceResponse.Messages.Add($"Category created successfully: {category.CategoryName}");

            return serviceResponse;
        }

        /// <summary>
        /// Updates an existing category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDto">The <see cref="CategoryDto"/> containing the updated category information.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var serviceResponse = new ServiceResponse();

            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Category not found.");
                return serviceResponse;
            }

            existingCategory.CategoryName = categoryDto.CategoryName ?? existingCategory.CategoryName;

            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            serviceResponse.Messages.Add($"Category updated successfully: {existingCategory.CategoryName}");

            return serviceResponse;
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> DeleteCategory(int id)
        {
            var serviceResponse = new ServiceResponse();

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Category not found.");
                return serviceResponse;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
            serviceResponse.Messages.Add("Category deleted successfully.");

            return serviceResponse;
        }
    }
}
