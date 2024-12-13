using Event_Media_Hub_Group_Project.Data;
using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Media_Hub_Group_Project.Services
{
    /// <summary>
    /// Service for managing events, including CRUD operations and handling event details.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all events.
        /// </summary>
        /// <returns>A list of <see cref="EventDto"/> representing events.</returns>
        public async Task<IEnumerable<EventDto>> ListEvents()
        {
            var events = await _context.Events.ToListAsync();
            return events.Select(e => new EventDto
            {
                EventId = e.EventId,
                EventName = e.EventName,
                Date = e.Date,
                Description = e.Description,
                LocationId = e.LocationId,
                LocationName = e.LocationName,
                UserId = e.UserId,
                UserName = e.UserName
            }).ToList();
        }

        /// <summary>
        /// Retrieves a single event by its ID.
        /// </summary>
        /// <param name="id">The ID of the event to retrieve.</param>
        /// <returns>An <see cref="EventDto"/> representing the event, or null if not found.</returns>
        public async Task<EventDto> GetEvent(int id)
        {
            var eventItem = await _context.Events.FirstOrDefaultAsync(e => e.EventId == id);
            if (eventItem == null)
                return null;

            return new EventDto
            {
                EventId = eventItem.EventId,
                EventName = eventItem.EventName,
                Date = eventItem.Date,
                Description = eventItem.Description,
                LocationId = eventItem.LocationId,
                LocationName = eventItem.LocationName,
                UserId = eventItem.UserId,
                UserName = eventItem.UserName
            };
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="eventDto">The <see cref="EventDto"/> containing the event details to create.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> CreateEvent(EventDto eventDto)
        {
            ServiceResponse serviceResponse = new();

            // Find Location by LocationId (Improved logic)
            var location = await _context.Locations
                                          .FirstOrDefaultAsync(l => l.LocationId == eventDto.LocationId);
            if (location == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Location not found.");
                return serviceResponse;
            }

            // Find User by UserId (Improved logic)
            var user = await _context.Users
                                     .FirstOrDefaultAsync(u => u.UserId == eventDto.UserId);
            if (user == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("User not found.");
                return serviceResponse;
            }

            var eventItem = new Event
            {
                EventName = eventDto.EventName,
                Date = eventDto.Date,
                Description = eventDto.Description,
                LocationId = location.LocationId,
                LocationName = location.LocationName, // Ensure LocationName is set correctly
                UserId = user.UserId,
                UserName = user.UserName // Ensure UserName is set correctly
            };

            _context.Events.Add(eventItem);
            await _context.SaveChangesAsync();

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = eventItem.EventId;
            serviceResponse.Messages.Add($"Event created successfully: {eventItem.EventName}");

            return serviceResponse;
        }

        /// <summary>
        /// Updates the details of an existing event by its ID.
        /// </summary>
        /// <param name="id">The ID of the event to update.</param>
        /// <param name="eventDto">The <see cref="EventDto"/> containing the updated event details.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> UpdateEventDetails(int id, EventDto eventDto)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
                return new ServiceResponse
                {
                    Status = ServiceResponse.ServiceStatus.NotFound,
                    Messages = { "Event not found." }
                };

            // Update Location by LocationId (Improved logic)
            if (eventDto.LocationId != existingEvent.LocationId)
            {
                var location = await _context.Locations
                                              .FirstOrDefaultAsync(l => l.LocationId == eventDto.LocationId);
                if (location == null)
                {
                    return new ServiceResponse
                    {
                        Status = ServiceResponse.ServiceStatus.Error,
                        Messages = { "Location not found." }
                    };
                }

                existingEvent.LocationId = location.LocationId;
                existingEvent.LocationName = location.LocationName; // Ensure LocationName is updated
            }

            // Update User by UserId (Improved logic)
            if (eventDto.UserId != existingEvent.UserId)
            {
                var user = await _context.Users
                                         .FirstOrDefaultAsync(u => u.UserId == eventDto.UserId);
                if (user == null)
                {
                    return new ServiceResponse
                    {
                        Status = ServiceResponse.ServiceStatus.Error,
                        Messages = { "User not found." }
                    };
                }

                existingEvent.UserId = user.UserId;
                existingEvent.UserName = user.UserName; // Ensure UserName is updated
            }

            // Update other fields
            existingEvent.EventName = eventDto.EventName ?? existingEvent.EventName;
            existingEvent.Date = eventDto.Date != default ? eventDto.Date : existingEvent.Date;
            existingEvent.Description = eventDto.Description ?? existingEvent.Description;

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Status = ServiceResponse.ServiceStatus.Updated,
                Messages = { $"Event updated successfully: {existingEvent.EventName}" }
            };
        }

        /// <summary>
        /// Deletes an event by its ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>A <see cref="ServiceResponse"/> indicating the result of the operation.</returns>
        public async Task<ServiceResponse> Deleteevent(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
                return new ServiceResponse
                {
                    Status = ServiceResponse.ServiceStatus.NotFound,
                    Messages = { "Event cannot be deleted because it does not exist." }
                };

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Status = ServiceResponse.ServiceStatus.Deleted,
                Messages = { "Event deleted successfully." }
            };
        }

        /// <summary>
        /// Retrieves a list of all locations.
        /// </summary>
        /// <returns>A list of <see cref="Location"/> representing all locations.</returns>
        public async Task<List<Location>> GetLocations()
        {
            return await _context.Locations.ToListAsync();
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of <see cref="User"/> representing all users.</returns>
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
