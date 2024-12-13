using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    /// <summary>
    /// Controller for managing user pages, including actions for listing, creating, editing, and deleting users.
    /// </summary>
    public class UserPageController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor to initialize UserPageController with IUserService.
        /// </summary>
        /// <param name="userService">The user service for managing user-related operations.</param>
        public UserPageController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Displays the index page of the UserPage.
        /// </summary>
        /// <returns>The Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retrieves a list of all users and displays them.
        /// </summary>
        /// <returns>The List view with the list of users.</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userService.ListUsers();
            return View(users);
        }

        /// <summary>
        /// Displays details of a specific user identified by the given ID.
        /// </summary>
        /// <param name="id">The ID of the user to display.</param>
        /// <returns>The Details view with the specified user's details.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUser(id);
            return View(user);
        }

        /// <summary>
        /// Returns a view for creating a new user.
        /// </summary>
        /// <returns>The New user creation view.</returns>
        public IActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Handles the creation of a new user with the provided user data.
        /// </summary>
        /// <param name="userDto">The data for the new user.</param>
        /// <returns>A redirect to the Details page for the created user or an error view if creation failed.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            var response = await _userService.CreateUser(userDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "UserPage", new { id = response.CreatedId });
            }

            return View("Error", new ErrorViewModel { Errors = response.Messages });
        }

        /// <summary>
        /// Displays a view for editing the details of an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <returns>The Edit view with the user's current details or an error view if user not found.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userDto = await _userService.GetUser(id);

            if (userDto == null)
            {
                return View("Error");
            }

            return View(userDto);
        }

        /// <summary>
        /// Handles the update of an existing user's details.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userDto">The updated user data.</param>
        /// <returns>A redirect to the Details page or an error view if the update failed.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(int id, UserDto userDto)
        {
            var response = await _userService.UpdateUserDetails(id, userDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "UserPage", new { id = id });
            }

            return View("Error", new ErrorViewModel { Errors = response.Messages });
        }

        /// <summary>
        /// Confirms if the user wants to delete a specific user identified by the given ID.
        /// </summary>
        /// <param name="id">The ID of the user to confirm deletion.</param>
        /// <returns>The ConfirmDelete view with the user's data for confirmation.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var userDto = await _userService.GetUser(id);

            if (userDto == null)
            {
                return View("Error");
            }

            return View(userDto);
        }

        /// <summary>
        /// Handles the deletion of a user identified by the given ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A redirect to the List page or an error view if deletion failed.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.DeleteUser(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "UserPage");
            }

            return View("Error");
        }
    }
}
