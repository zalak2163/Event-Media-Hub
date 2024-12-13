using System.ComponentModel.DataAnnotations;

namespace Event_Media_Hub_Group_Project.Models
{
    /// <summary>
    /// Represents a user, including their name, email, role, and the events and photos associated with them.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role of the user (e.g., Admin, User).
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the collection of events associated with the user.
        /// </summary>
        public virtual ICollection<Event> Events { get; set; }

        /// <summary>
        /// Gets or sets the collection of photos associated with the user.
        /// </summary>
        public virtual ICollection<Photo> Photos { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for a user, used for transferring user data.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role of the user (e.g., Admin, User).
        /// </summary>
        public string Role { get; set; }
    }
}
