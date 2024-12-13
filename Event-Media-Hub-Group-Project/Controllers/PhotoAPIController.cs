using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    /// <summary>
    /// Controller for managing photos in the application. 
    /// Provides actions for listing, retrieving, creating, updating, and deleting photos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoAPIController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        /// <summary>
        /// Constructor for initializing the PhotoAPIController.
        /// </summary>
        /// <param name="photoService">The photo service instance.</param>
        public PhotoAPIController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Retrieves a list of all photos.
        /// </summary>
        /// <returns>A list of photos in the form of PhotoDto.</returns>
        /// <response code="200">Returns a list of photos</response>
        /// <response code="404">No photos found</response>
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> ListPhotos()
        {
            var photos = await _photoService.ListPhotos();
            if (photos == null || !photos.Any())
            {
                return NotFound(new { message = "No photos found." });
            }

            return Ok(photos);
        }

        /// <summary>
        /// Retrieves details of a specific photo by its ID.
        /// </summary>
        /// <param name="id">The ID of the photo to retrieve.</param>
        /// <returns>The details of the photo.</returns>
        /// <response code="200">Returns the photo details</response>
        /// <response code="404">Photo not found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoDto>> GetPhoto(int id)
        {
            var photo = await _photoService.GetPhoto(id);

            if (photo == null)
            {
                return NotFound(new { message = $"Photo with id {id} not found." });
            }

            return Ok(photo);
        }

        /// <summary>
        /// Creates a new photo.
        /// </summary>
        /// <param name="photoDto">The details of the photo to create.</param>
        /// <returns>The created photo details, or an error message if creation fails.</returns>
        /// <response code="201">Photo created successfully</response>
        /// <response code="400">Invalid photo data</response>
        [HttpPost("Add")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse>> CreatePhoto([FromBody] PhotoDto photoDto)
        {
            if (photoDto == null)
            {
                return BadRequest(new { message = "Photo data is required." });
            }

            var serviceResponse = await _photoService.CreatePhoto(photoDto);

            if (serviceResponse.Status == ServiceResponse.ServiceStatus.Created)
            {
                return CreatedAtAction(nameof(GetPhoto), new { id = serviceResponse.CreatedId }, serviceResponse);
            }

            return BadRequest(serviceResponse);
        }

        /// <summary>
        /// Updates the details of an existing photo.
        /// </summary>
        /// <param name="id">The ID of the photo to update.</param>
        /// <param name="photoDto">The updated details of the photo.</param>
        /// <returns>The updated photo details or an error message if update fails.</returns>
        /// <response code="200">Photo updated successfully</response>
        /// <response code="400">Invalid photo data</response>
        /// <response code="404">Photo not found</response>
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse>> UpdatePhotoDetails(int id, [FromBody] PhotoDto photoDto)
        {
            if (photoDto == null)
            {
                return BadRequest(new { message = "Invalid photo data." });
            }

            var serviceResponse = await _photoService.UpdatePhotoDetails(id, photoDto);

            if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(new { message = $"Photo with id {id} not found." });
            }

            if (serviceResponse.Status == ServiceResponse.ServiceStatus.Error)
            {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        /// <summary>
        /// Deletes an existing photo by its ID.
        /// </summary>
        /// <param name="id">The ID of the photo to delete.</param>
        /// <returns>A response indicating the result of the deletion.</returns>
        /// <response code="200">Photo deleted successfully</response>
        /// <response code="404">Photo not found</response>
        /// <response code="400">Error during deletion</response>
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse>> DeletePhoto(int id)
        {
            var serviceResponse = await _photoService.DeletePhoto(id);

            if (serviceResponse.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(new { message = $"Photo with id {id} not found." });
            }

            if (serviceResponse.Status == ServiceResponse.ServiceStatus.Error)
            {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }
    }
}
