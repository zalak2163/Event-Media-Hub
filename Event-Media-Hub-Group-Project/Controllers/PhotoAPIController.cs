using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Media_Hub_Group_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoAPIController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoAPIController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        // GET: api/photoapi/list
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

        // GET: api/photoapi/{id}
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

        // POST: api/photoapi/add
        [HttpPost("Add")]
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

        // PUT: api/photoapi/update/{id}
        [HttpPut("Update/{id}")]
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

        // DELETE: api/photoapi/delete/{id}
        [HttpDelete("Delete/{id}")]
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
