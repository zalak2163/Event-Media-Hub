using Event_Media_Hub_Group_Project.Models;

namespace Event_Media_Hub_Group_Project.Interface
{
    /// <summary>
    /// Interface for event-related operations. Provides methods for managing events including listing, 
    /// retrieving, creating, updating, and deleting events, as well as managing related locations and users.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Retrieves a list of all events.
        /// </summary>
        /// <returns>A list of event data transfer objects (DTOs).</returns>
        Task<IEnumerable<EventDto>> ListEvents();

        /// <summary>
        /// Retrieves the details of a specific event identified by the given ID.
        /// </summary>
        /// <param name="id">The ID of the event to retrieve.</param>
        /// <returns>A DTO representing the event with the specified ID.</returns>
        Task<EventDto> GetEvent(int id);

        /// <summary>
        /// Creates a new event using the provided event DTO.
        /// </summary>
        /// <param name="eventDto">The data transfer object containing the event information to be created.</param>
        /// <returns>A service response indicating the result of the event creation operation.</returns>
        Task<ServiceResponse> CreateEvent(EventDto eventDto);

        /// <summary>
        /// Updates the details of an existing event identified by the given ID with the provided event DTO.
        /// </summary>
        /// <param name="id">The ID of the event to update.</param>
        /// <param name="eventDto">The updated event data transfer object.</param>
        /// <returns>A service response indicating the result of the event update operation.</returns>
        Task<ServiceResponse> UpdateEventDetails(int id, EventDto eventDto);

        /// <summary>
        /// Deletes an existing event identified by the given ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>A service response indicating the result of the event deletion operation.</returns>
        Task<ServiceResponse> Deleteevent(int id);

        /// <summary>
        /// Retrieves a list of locations associated with events.
        /// </summary>
        /// <returns>A list of locations available for event associations.</returns>
        Task<List<Location>> GetLocations();

        /// <summary>
        /// Retrieves a list of users associated with events.
        /// </summary>
        /// <returns>A list of users available for event associations.</returns>
        Task<List<User>> GetUsers();
    }
}
