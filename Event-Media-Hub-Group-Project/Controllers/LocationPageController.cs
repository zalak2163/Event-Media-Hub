using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    /// <summary>
    /// Controller for managing locations in the web application.
    /// Provides actions for listing, retrieving, creating, updating, and deleting locations.
    /// </summary>
    public class LocationPageController : Controller
    {
        private readonly ILocationService _locationService;

        /// <summary>
        /// Constructor for initializing the LocationPageController.
        /// </summary>
        /// <param name="locationService">The location service instance.</param>
        public LocationPageController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Displays the index page for locations.
        /// </summary>
        /// <returns>The Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retrieves and displays a list of all locations.
        /// </summary>
        /// <returns>A view with the list of locations.</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return View(await _locationService.ListLocations());
        }

        /// <summary>
        /// Displays the details of a specific location based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <returns>A view with the location details.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _locationService.GetLocation(id));
        }

        /// <summary>
        /// Displays the form to create a new location.
        /// </summary>
        /// <returns>The view for creating a new location.</returns>
        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Handles the creation of a new location.
        /// </summary>
        /// <param name="locationDto">The details of the location to be created.</param>
        /// <returns>Redirects to the location details view if successful, otherwise shows an error view.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(LocationDto locationDto)
        {
            ServiceResponse response = await _locationService.CreateLocation(locationDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "LocationPage", new { id = response.CreatedId });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        /// <summary>
        /// Displays the form to edit an existing location based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the location to edit.</param>
        /// <returns>The view for editing the location, or an error view if not found.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            LocationDto? LocationDto = await _locationService.GetLocation(id);
            if (LocationDto == null)
            {
                return View("Error");
            }
            else
            {
                return View(LocationDto);
            }
        }

        /// <summary>
        /// Handles the update of an existing location's details.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="locationDto">The updated location details.</param>
        /// <returns>Redirects to the location details view if successful, otherwise shows an error view.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(int id, LocationDto locationDto)
        {
            ServiceResponse response = await _locationService.UpdateLocationDetails(id, locationDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "LocationPage", new { id = id });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        /// <summary>
        /// Confirms the deletion of a location by displaying a confirmation page.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>The view for confirming the deletion of the location.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            LocationDto? locationDto = await _locationService.GetLocation(id);
            if (locationDto == null)
            {
                return View("Error");
            }
            else
            {
                return View(locationDto);
            }
        }

        /// <summary>
        /// Handles the deletion of a location.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>Redirects to the list of locations if successful, otherwise shows an error view.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _locationService.DeleteLocation(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "LocationPage");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
