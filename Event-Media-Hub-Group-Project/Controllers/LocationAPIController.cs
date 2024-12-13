using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    /// <summary>
    /// API Controller for managing locations.
    /// Provides functionality for listing, retrieving, creating, updating, and deleting locations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LocationAPIController : ControllerBase
    {
        private readonly ILocationService _locationService;

        /// <summary>
        /// Constructor for initializing the LocationAPIController.
        /// </summary>
        /// <param name="locationService">The location service instance.</param>
        public LocationAPIController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Retrieves a list of all locations.
        /// </summary>
        /// <returns>A collection of <see cref="LocationDto"/>.</returns>
        [HttpGet("List")]
        public async Task<IEnumerable<LocationDto>> ListLocations()
        {
            return await _locationService.ListLocations();
        }

        /// <summary>
        /// Retrieves a specific location by its ID.
        /// </summary>
        /// <param name="id">The ID of the location.</param>
        /// <returns>The <see cref="LocationDto"/> for the given ID if found; otherwise, null.</returns>
        [HttpGet("{id}")]
        public async Task<LocationDto> GetLocation(int id)
        {
            return await _locationService.GetLocation(id);
        }

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="locationDto">The details of the location to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the creation.</returns>
        [HttpPost("Add")]
        [Authorize]
        public async Task<ServiceResponse> CreateLocation(LocationDto locationDto)
        {
            return await _locationService.CreateLocation(locationDto);
        }

        /// <summary>
        /// Updates the details of an existing location.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="locationDto">The updated location details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the update.</returns>
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<ServiceResponse> UpdateLocationDetails(int id, LocationDto locationDto)
        {
            return await _locationService.UpdateLocationDetails(id, locationDto);
        }

        /// <summary>
        /// Deletes a location by its ID.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the deletion.</returns>
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ServiceResponse> DeleteLocation(int id)
        {
            return await _locationService.DeleteLocation(id);
        }
    }
}
