using Event_Media_Hub_Group_Project.Models;

namespace Event_Media_Hub_Group_Project.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> ListCategories();
        Task<CategoryDto> GetCategory(int id);
        Task<ServiceResponse> CreateCategory(CategoryDto categoryDto);
        Task<ServiceResponse> UpdateCategory(int id, CategoryDto categoryDto);
        Task<ServiceResponse> DeleteCategory(int id);
    }
}
