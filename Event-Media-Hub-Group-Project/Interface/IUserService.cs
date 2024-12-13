using Event_Media_Hub_Group_Project.Models;

namespace Event_Media_Hub_Group_Project.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> ListUsers();  // Retrieves a list of all users as DTOs.

        Task<UserDto> GetUser(int id);  // Retrieves details of a specific user by ID as a DTO.

        Task<ServiceResponse> CreateUser(UserDto userDto);  // Creates a new user using the provided UserDto.

        Task<ServiceResponse> UpdateUserDetails(int id, UserDto userDto);  // Updates an existing user's details using the UserDto.

        Task<ServiceResponse> DeleteUser(int id);  // Deletes a user by their ID.

        IEnumerable<User> GetAllUsers();  // Retrieves all users in the system as User entities.
    }
}
