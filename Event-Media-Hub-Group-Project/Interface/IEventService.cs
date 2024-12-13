using Event_Media_Hub_Group_Project.Models;

namespace Event_Media_Hub_Group_Project.Interface
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> ListEvents();
        Task<EventDto> GetEvent(int id);
        Task<ServiceResponse> CreateEvent(EventDto eventDto);
        Task<ServiceResponse> UpdateEventDetails(int id, EventDto eventDto);
        Task<ServiceResponse> Deleteevent(int id);
        Task<List<Location>> GetLocations();
        Task<List<User>> GetUsers();
    }
}
