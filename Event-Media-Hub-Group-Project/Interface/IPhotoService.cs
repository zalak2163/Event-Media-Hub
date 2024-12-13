using Event_Media_Hub_Group_Project.Models;

namespace Event_Media_Hub_Group_Project.Interface
{
    public interface IPhotoService
    {
        Task<IEnumerable<PhotoDto>> ListPhotos();  // Fetches a list of all photos.

        Task<PhotoDto> GetPhoto(int id);  // Fetches details of a specific photo by ID.

        Task<ServiceResponse> CreatePhoto(PhotoDto photoDto);  // Creates a new photo.

        Task<ServiceResponse> UpdatePhotoDetails(int id, PhotoDto photoDto);  // Updates an existing photo by ID.

        Task<ServiceResponse> DeletePhoto(int id);  // Deletes a photo by ID.
    }
}
