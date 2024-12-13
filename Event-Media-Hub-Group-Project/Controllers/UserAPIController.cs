using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    /// <summary>
    /// API Controller for managing user-related operations.
    /// Includes actions for retrieving, creating, updating, and deleting users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for initializing the UserAPIController with a user service.
        /// </summary>
        /// <param name="userService">The user service for managing users.</param>
        public UserAPIController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves the list of all users.
        /// </summary>
        /// <returns>A list of UserDto objects representing all users.</returns>
        [HttpGet("List")]
        public async Task<IEnumerable<UserDto>> ListUsers()
        {
            return await _userService.ListUsers();
        }

        /// <summary>
        /// Retrieves a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A UserDto object representing the requested user.</returns>
        [HttpGet("{id}")]
        public async Task<UserDto> GetUser(int id)
        {
            return await _userService.GetUser(id);
        }

        /// <summary>
        /// Creates a new user with the provided user data.
        /// </summary>
        /// <param name="userDto">The data of the user to create.</param>
        /// <returns>A ServiceResponse object indicating the result of the creation operation.</returns>
        [HttpPost("Add")]
        [Authorize]
        public async Task<ServiceResponse> CreateUser(UserDto userDto)
        {
            return await _userService.CreateUser(userDto);
        }

        /// <summary>
        /// Updates the details of an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userDto">The updated user data.</param>
        /// <returns>A ServiceResponse object indicating the result of the update operation.</returns>
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<ServiceResponse> UpdateUserDetails(int id, UserDto userDto)
        {
            return await _userService.UpdateUserDetails(id, userDto);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A ServiceResponse object indicating the result of the deletion operation.</returns>
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ServiceResponse> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id);
        }
    }
}
