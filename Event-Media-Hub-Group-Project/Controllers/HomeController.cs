using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Event_Media_Hub_Group_Project.Controllers
{
    /// <summary>
    /// Controller for managing home page views and error handling.
    /// Provides functionality for displaying the homepage, privacy page, and error page.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor for initializing the HomeController.
        /// </summary>
        /// <param name="logger">The logger instance for logging information.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Displays the homepage view.
        /// </summary>
        /// <returns>A view representing the homepage.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        /// <returns>A view representing the privacy page.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the error page with error details.
        /// </summary>
        /// <returns>A view with error details, including the request ID.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
