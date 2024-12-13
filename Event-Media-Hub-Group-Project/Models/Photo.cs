using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Event_Media_Hub_Group_Project.Models
{
    /// <summary>
    /// Represents a photo, including details such as title, description, price, and the user who uploaded it.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Gets or sets the unique identifier for the photo.
        /// </summary>
        [Key]
        public int PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the title of the photo.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the photo.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the photo.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the upload date of the photo.
        /// </summary>
        public DateTime UploadDate { get; set; }

        /// <summary>
        /// Gets or sets the user identifier for the user who uploaded the photo.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who uploaded the photo.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the collection of users associated with the photo.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for a photo, used for transferring photo data.
    /// </summary>
    public class PhotoDto
    {
        /// <summary>
        /// The photo file uploaded by the user. It should be of type IFormFile.
        /// </summary>
        public IFormFile PhotoFile { get; set; }  // Change this from 'object' to 'IFormFile'

        /// <summary>
        /// Gets or sets the unique identifier for the photo.
        /// </summary>
        public int PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the title of the photo.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the photo.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the photo.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the upload date of the photo.
        /// </summary>
        public DateTime UploadDate { get; set; }

        /// <summary>
        /// Gets or sets the user identifier for the user who uploaded the photo.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who uploaded the photo.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the list of users associated with the photo.
        /// </summary>
        public List<UserDto> Users { get; set; }

        /// <summary>
        /// The file path of the uploaded photo (the relative URL where the image is stored).
        /// </summary>
        public string FilePath { get; set; }
    }





}
