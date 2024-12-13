using Event_Media_Hub_Group_Project.Models;

namespace Event_Media_Hub_Group_Project.Interface
{
    /// <summary>
    /// Interface for category-related operations. Provides methods for managing categories including listing, 
    /// retrieving, creating, updating, and deleting categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Lists all categories.
        /// </summary>
        /// <returns>A list of category data transfer objects (DTOs).</returns>
        Task<IEnumerable<CategoryDto>> ListCategories();

        /// <summary>
        /// Retrieves the details of a specific category identified by the given ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A DTO representing the category with the specified ID.</returns>
        Task<CategoryDto> GetCategory(int id);

        /// <summary>
        /// Creates a new category using the provided category DTO.
        /// </summary>
        /// <param name="categoryDto">The data transfer object containing the category information to be created.</param>
        /// <returns>A service response indicating the result of the category creation operation.</returns>
        Task<ServiceResponse> CreateCategory(CategoryDto categoryDto);

        /// <summary>
        /// Updates the details of an existing category identified by the given ID with the provided category DTO.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDto">The updated category data transfer object.</param>
        /// <returns>A service response indicating the result of the category update operation.</returns>
        Task<ServiceResponse> UpdateCategory(int id, CategoryDto categoryDto);

        /// <summary>
        /// Deletes an existing category identified by the given ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A service response indicating the result of the category deletion operation.</returns>
        Task<ServiceResponse> DeleteCategory(int id);
    }
}
