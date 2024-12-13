using Event_Media_Hub_Group_Project.Data;
using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Media_Hub_Group_Project.Services
{
    /// <summary>
    /// Service for managing user data, including CRUD operations and user details.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of <see cref="UserDto"/> representing users.</returns>
        public async Task<IEnumerable<UserDto>> ListUsers()
        {
            var users = await _context.Users.ToListAsync();

            var userDtos = users.Select(user => new UserDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
            }).ToList();

            return userDtos;
        }

        /// <summary>
        /// Retrieves a specific user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A <see cref="UserDto"/> representing the user, or null if not found.</returns>
        public async Task<UserDto> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return null;
            }

            return new UserDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userDto">The <see cref="UserDto"/> containing the user details to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> CreateUser(UserDto userDto)
        {
            var serviceResponse = new ServiceResponse();

            var user = new User()
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Role = userDto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = user.UserId;
            serviceResponse.Messages.Add($"User created successfully: {user.UserName}");

            return serviceResponse;
        }

        /// <summary>
        /// Updates the details of an existing user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userDto">The <see cref="UserDto"/> containing the updated user details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> UpdateUserDetails(int id, UserDto userDto)
        {
            var serviceResponse = new ServiceResponse();

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("User not found.");
                return serviceResponse;
            }

            existingUser.UserName = userDto.UserName ?? existingUser.UserName;
            existingUser.Email = userDto.Email ?? existingUser.Email;
            existingUser.Role = userDto.Role ?? existingUser.Role;

            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            serviceResponse.Messages.Add($"User updated successfully: {existingUser.UserName}");

            return serviceResponse;
        }

        /// <summary>
        /// Deletes a user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("User cannot be deleted because it does not exist.");
                return serviceResponse;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
            serviceResponse.Messages.Add("User deleted successfully.");

            return serviceResponse;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of <see cref="UserDto"/> representing users.</returns>
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(user => new UserDto()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role
            }).ToList();
        }

        /// <summary>
        /// Placeholder method to retrieve all users (not implemented).
        /// </summary>
        /// <returns>Throws <see cref="NotImplementedException"/>.</returns>
        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
