using Event_Media_Hub_Group_Project.Models;

namespace Event_Media_Hub_Group_Project.Interface
{
    /// <summary>
    /// Interface for location-related operations. Provides methods for managing locations,
    /// including listing, retrieving, creating, updating, and deleting locations.
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Retrieves a list of all locations.
        /// </summary>
        /// <returns>A list of location data transfer objects (DTOs).</returns>
        Task<IEnumerable<LocationDto>> ListLocations();

        /// <summary>
        /// Retrieves the details of a specific location identified by the given ID.
        /// </summary>
        /// <param name="id">The ID of the location to retrieve.</param>
        /// <returns>A DTO representing the location with the specified ID.</returns>
        Task<LocationDto> GetLocation(int id);

        /// <summary>
        /// Creates a new location using the provided location DTO.
        /// </summary>
        /// <param name="locationDto">The data transfer object containing the location information to be created.</param>
        /// <returns>A service response indicating the result of the location creation operation.</returns>
        Task<ServiceResponse> CreateLocation(LocationDto locationDto);

        /// <summary>
        /// Updates the details of an existing location identified by the given ID with the provided location DTO.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="locationDto">The updated location data transfer object.</param>
        /// <returns>A service response indicating the result of the location update operation.</returns>
        Task<ServiceResponse> UpdateLocationDetails(int id, LocationDto locationDto);

        /// <summary>
        /// Deletes an existing location identified by the given ID.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>A service response indicating the result of the location deletion operation.</returns>
        Task<ServiceResponse> DeleteLocation(int id);
    }
}
