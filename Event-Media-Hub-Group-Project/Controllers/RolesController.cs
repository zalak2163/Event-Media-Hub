using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    /// <summary>
    /// Controller for managing roles in the application.
    /// Provides actions for viewing and creating roles.
    /// </summary>
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _manager;

        /// <summary>
        /// Constructor for initializing the RolesController.
        /// </summary>
        /// <param name="roleManager">The RoleManager for managing roles.</param>
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _manager = roleManager;
        }

        /// <summary>
        /// Displays the list of all roles in the system.
        /// </summary>
        /// <returns>A View showing the list of all roles.</returns>
        public IActionResult Index()
        {
            var roles = _manager.Roles.ToList();
            return View(roles);
        }

        /// <summary>
        /// Displays the form for creating a new role.
        /// </summary>
        /// <returns>A View for creating a new role.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the creation of a new role.
        /// Checks if the role already exists, and if not, creates the role.
        /// </summary>
        /// <param name="role">The role data to be created.</param>
        /// <returns>Redirects to the roles list view if the creation is successful.</returns>
        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            // Check if the role already exists
            if (!_manager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
            {
                // Create a new role if it does not exist
                _manager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}
