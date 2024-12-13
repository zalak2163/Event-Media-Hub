using Event_Media_Hub_Group_Project.Models;


namespace Event_Media_Hub_Group_Project.Interface
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> ListLocations();
        Task<LocationDto> GetLocation(int id);
        Task<ServiceResponse> CreateLocation(LocationDto locationDto);
        Task<ServiceResponse> UpdateLocationDetails(int id, LocationDto locationDto);
        Task<ServiceResponse> DeleteLocation(int id);
    }
}
