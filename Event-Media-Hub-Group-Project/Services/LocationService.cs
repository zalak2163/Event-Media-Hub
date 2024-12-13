using Event_Media_Hub_Group_Project.Data;
using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Media_Hub_Group_Project.Services
{
    /// <summary>
    /// Service for managing locations, including CRUD operations and location details.
    /// </summary>
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;

        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all locations.
        /// </summary>
        /// <returns>A list of <see cref="LocationDto"/> representing locations.</returns>
        public async Task<IEnumerable<LocationDto>> ListLocations()
        {
            var locations = await _context.Locations.ToListAsync();
            var locationDtos = locations.Select(location => new LocationDto
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName,
                Address = location.Address,
                Capacity = location.Capacity
            }).ToList();

            return locationDtos;
        }

        /// <summary>
        /// Retrieves a single location by its ID.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <returns>A <see cref="LocationDto"/> representing the location, or null if not found.</returns>
        public async Task<LocationDto> GetLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            return location == null ? null : new LocationDto
            {
                LocationId = location.LocationId,
                LocationName = location.LocationName,
                Address = location.Address,
                Capacity = location.Capacity
            };
        }

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="locationDto">The <see cref="LocationDto"/> containing the location details to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> CreateLocation(LocationDto locationDto)
        {
            var serviceResponse = new ServiceResponse();

            var location = new Location
            {
                LocationName = locationDto.LocationName,
                Address = locationDto.Address,
                Capacity = locationDto.Capacity
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = location.LocationId;
            serviceResponse.Messages.Add($"Location created successfully: {location.LocationName}");

            return serviceResponse;
        }

        /// <summary>
        /// Updates the details of an existing location by its ID.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="locationDto">The <see cref="LocationDto"/> containing the updated location details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> UpdateLocationDetails(int id, LocationDto locationDto)
        {
            var serviceResponse = new ServiceResponse();

            var existingLocation = await _context.Locations.FindAsync(id);
            if (existingLocation == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Location not found.");
                return serviceResponse;
            }

            existingLocation.LocationName = locationDto.LocationName ?? existingLocation.LocationName;
            existingLocation.Address = locationDto.Address ?? existingLocation.Address;
            existingLocation.Capacity = locationDto.Capacity != 0 ? locationDto.Capacity : existingLocation.Capacity;

            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            serviceResponse.Messages.Add($"Location updated successfully: {existingLocation.LocationName}");

            return serviceResponse;
        }

        /// <summary>
        /// Deletes a location by its ID.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> DeleteLocation(int id)
        {
            var serviceResponse = new ServiceResponse();

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Location not found.");
                return serviceResponse;
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
            serviceResponse.Messages.Add($"Location deleted successfully: {location.LocationName}");

            return serviceResponse;
        }
    }
}
