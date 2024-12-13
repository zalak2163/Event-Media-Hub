using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Event_Media_Hub_Group_Project.Controllers
{
    public class EventPageController : Controller
    {
        private readonly IEventService _eventService;

        // Constructor
        public EventPageController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // Index action (optional)
        public IActionResult Index()
        {
            return View();
        }

        // List of Events
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var events = await _eventService.ListEvents();
            return View(events);
        }

        // Details of an Event
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

        // New Event Form
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

        // Create Event (POST)
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

        // Edit Event Form (GET)
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

        // Update Event (POST)
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

        // Confirm Event Deletion (GET)
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

        // Delete Event (POST)
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