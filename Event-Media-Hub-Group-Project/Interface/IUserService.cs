using Event_Media_Hub_Group_Project.Models;

namespace Event_Media_Hub_Group_Project.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> ListUsers();
        Task<UserDto> GetUser(int id);
        Task<ServiceResponse> CreateUser(UserDto userDto);
        Task<ServiceResponse> UpdateUserDetails(int id, UserDto userDto);
        Task<ServiceResponse> DeleteUser(int id);
        IEnumerable<User> GetAllUsers();

    }
}
