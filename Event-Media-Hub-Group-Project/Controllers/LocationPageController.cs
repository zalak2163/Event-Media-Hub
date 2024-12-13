﻿using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    public class LocationPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ILocationService _locationService;

        public LocationPageController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return View(await _locationService.ListLocations());
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _locationService.GetLocation(id));
        }

        [Authorize]
        public ActionResult New()
        {
            return View();
        }
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