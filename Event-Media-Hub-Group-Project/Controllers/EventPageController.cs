using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Event_Media_Hub_Group_Project.Controllers
{
    /// <summary>
    /// Controller for managing event pages.
    /// Provides functionality for listing, viewing, creating, editing, and deleting events.
    /// </summary>
    public class EventPageController : Controller
    {
        private readonly IEventService _eventService;

        // Constructor
        public EventPageController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Displays the index page of the event.
        /// </summary>
        /// <returns>View of the event index page.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retrieves and displays a list of all events.
        /// </summary>
        /// <returns>A view with a list of events.</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var events = await _eventService.ListEvents();
            return View(events);
        }

        /// <summary>
        /// Displays the details of a specific event based on its ID.
        /// </summary>
        /// <param name="id">The ID of the event to retrieve.</param>
        /// <returns>A view displaying the event details if found; otherwise, an error view.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var eventDto = await _eventService.GetEvent(id);
            if (eventDto == null)
            {
                return View("Error");
            }
            return View(eventDto);
        }

        /// <summary>
        /// Displays the form to create a new event.
        /// </summary>
        /// <returns>A view with the event creation form, including location and user dropdowns.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> New()
        {
            var model = new EventDto();

            // Fetch locations and users for dropdowns
            var locations = await _eventService.GetLocations();
            var users = await _eventService.GetUsers();

            // Populate the dropdown lists for locations and users with 'Id - Name' format
            model.Locations = locations.Select(l => new SelectListItem
            {
                Value = l.LocationId.ToString(),
                Text = $"{l.LocationId} - {l.LocationName}"
            }).ToList();

            model.Users = users.Select(u => new SelectListItem
            {
                Value = u.UserId.ToString(),
                Text = $"{u.UserId} - {u.UserName}"
            }).ToList();

            return View(model);
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="eventDto">The details of the event to create.</param>
        /// <returns>Redirects to the details page of the newly created event if successful; otherwise, shows an error.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(EventDto eventDto)
        {
            ServiceResponse response = await _eventService.CreateEvent(eventDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "EventPage", new { id = response.CreatedId });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        /// <summary>
        /// Displays the form to edit an existing event.
        /// </summary>
        /// <param name="id">The ID of the event to edit.</param>
        /// <returns>A view with the event edit form, including location and user dropdowns, or an error view if the event is not found.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var eventDto = await _eventService.GetEvent(id);
            if (eventDto == null)
            {
                return View("Error");
            }

            // Fetch locations and users for dropdowns
            var locations = await _eventService.GetLocations();
            var users = await _eventService.GetUsers();

            // Populate the dropdown lists for locations and users with 'Id - Name' format
            eventDto.Locations = locations.Select(l => new SelectListItem
            {
                Value = l.LocationId.ToString(),
                Text = $"{l.LocationId} - {l.LocationName}"
            }).ToList();

            eventDto.Users = users.Select(u => new SelectListItem
            {
                Value = u.UserId.ToString(),
                Text = $"{u.UserId} - {u.UserName}"
            }).ToList();

            return View(eventDto);
        }

        /// <summary>
        /// Updates the details of an existing event.
        /// </summary>
        /// <param name="id">The ID of the event to update.</param>
        /// <param name="eventDto">The updated event details.</param>
        /// <returns>Redirects to the details page of the updated event if successful; otherwise, shows an error.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(int id, EventDto eventDto)
        {
            ServiceResponse response = await _eventService.UpdateEventDetails(id, eventDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "EventPage", new { id = id });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        /// <summary>
        /// Confirms event deletion by displaying the event details before deletion.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>A view with the event details for confirmation before deletion, or an error view if the event is not found.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var eventDto = await _eventService.GetEvent(id);
            if (eventDto == null)
            {
                return View("Error");
            }
            return View(eventDto);
        }

        /// <summary>
        /// Deletes a specific event.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>Redirects to the event list page if successful; otherwise, shows an error.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _eventService.Deleteevent(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "EventPage");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
