using Event_Media_Hub_Group_Project.Interface;
using Event_Media_Hub_Group_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Event_Media_Hub_Group_Project.Controllers
{
    /// <summary>
    /// Controller for managing photos on the webpage.
    /// Provides actions for viewing, creating, editing, and deleting photos.
    /// </summary>
    public class PhotoPageController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// Constructor for initializing the PhotoPageController.
        /// </summary>
        /// <param name="photoService">The photo service instance.</param>
        /// <param name="webHostEnvironment">The web hosting environment for file upload handling.</param>
        public PhotoPageController(IPhotoService photoService, IWebHostEnvironment webHostEnvironment)
        {
            _photoService = photoService;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Displays the home page for photos.
        /// </summary>
        /// <returns>The View for the photo home page.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lists all photos in the system.
        /// </summary>
        /// <returns>A View showing all the photos.</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var photos = await _photoService.ListPhotos();
            return View(photos);  // Ensure the List view is rendered
        }

        /// <summary>
        /// Displays the details of a specific photo by its ID.
        /// </summary>
        /// <param name="id">The ID of the photo to view.</param>
        /// <returns>A View showing the details of the specified photo.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var photo = await _photoService.GetPhoto(id);
            return View(photo);
        }

        /// <summary>
        /// Displays the form for creating a new photo.
        /// </summary>
        /// <returns>A View for the photo creation form.</returns>
        [Authorize]
        public IActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Handles the creation of a new photo.
        /// </summary>
        /// <param name="photoDto">The data of the new photo to create.</param>
        /// <returns>Redirects to the photo details page if creation is successful, or shows an error if not.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PhotoDto photoDto)
        {
            string filePath = null;

            // Handle file upload
            if (photoDto.PhotoFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + photoDto.PhotoFile.FileName;
                filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photoDto.PhotoFile.CopyToAsync(fileStream);
                }

                filePath = "/images/" + uniqueFileName;
            }

            // Create a new photo object and pass the file path to it
            photoDto.FilePath = filePath;

            var response = await _photoService.CreatePhoto(photoDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "PhotoPage", new { id = response.CreatedId });
            }

            return View("Error", new ErrorViewModel { Errors = response.Messages });
        }

        /// <summary>
        /// Displays the form for editing an existing photo by its ID.
        /// </summary>
        /// <param name="id">The ID of the photo to edit.</param>
        /// <returns>A View for editing the specified photo.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var photoDto = await _photoService.GetPhoto(id);

            if (photoDto == null)
            {
                return View("Error");
            }

            return View(photoDto);
        }

        /// <summary>
        /// Handles the update of a specific photo's details.
        /// </summary>
        /// <param name="id">The ID of the photo to update.</param>
        /// <param name="photoDto">The updated photo data.</param>
        /// <returns>Redirects to the updated photo details page or shows an error if the update fails.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(int id, PhotoDto photoDto)
        {
            string filePath = photoDto.FilePath; // Retain the current file path if no new file is uploaded

            // Handle file upload if a new file is provided
            if (photoDto.PhotoFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + photoDto.PhotoFile.FileName;
                filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photoDto.PhotoFile.CopyToAsync(fileStream);
                }

                filePath = "/images/" + uniqueFileName;
            }

            // Set the file path to the updated one
            photoDto.FilePath = filePath;

            var response = await _photoService.UpdatePhotoDetails(id, photoDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "PhotoPage", new { id = id });
            }

            return View("Error", new ErrorViewModel { Errors = response.Messages });
        }

        /// <summary>
        /// Confirms the deletion of a specific photo by its ID.
        /// </summary>
        /// <param name="id">The ID of the photo to delete.</param>
        /// <returns>A View asking for confirmation to delete the specified photo.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var photoDto = await _photoService.GetPhoto(id);

            if (photoDto == null)
            {
                return View("Error");
            }

            return View(photoDto);
        }

        /// <summary>
        /// Deletes a specific photo by its ID.
        /// </summary>
        /// <param name="id">The ID of the photo to delete.</param>
        /// <returns>Redirects to the photo list page if deletion is successful, or shows an error if not.</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _photoService.DeletePhoto(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "PhotoPage");
            }

            return View("Error");
        }
    }
}
