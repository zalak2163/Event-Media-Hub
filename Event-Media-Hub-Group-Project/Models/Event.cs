using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Event_Media_Hub_Group_Project.Models
{
    /// <summary>
    /// Represents an event in the system. This entity includes information about the event's name, description, date,
    /// location, and associated users.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets or sets the unique identifier for the event.
        /// This is the primary key for the event in the database.
        /// </summary>
        [Key]
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the name of the event.
        /// The name is used to identify and describe the event, such as "Music Concert" or "Tech Conference".
        /// </summary>
        public string EventName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the event is scheduled to take place.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the description of the event.
        /// This is a detailed text describing the event's purpose, schedule, and other relevant information.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location associated with the event.
        /// This is a navigation property to the location where the event will be held.
        /// </summary>
        public virtual Location Locations { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the location where the event will be held.
        /// This serves as a foreign key to the Location entity.
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the location where the event will take place.
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the user organizing or associated with the event.
        /// This serves as a foreign key to the User entity.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user associated with the event.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the collection of users associated with the event.
        /// This represents many-to-many relationships where multiple users can be associated with an event.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) used to transfer event data between layers or over the network.
    /// This class is typically used in API responses or when interacting with external systems.
    /// </summary>
    public class EventDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the event.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the event is scheduled.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the description of the event.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the location identifier associated with the event.
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the location where the event will take place.
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the user identifier associated with the event.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user associated with the event.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a list of location options for event creation or editing in the form of <see cref="SelectListItem"/>.
        /// </summary>
        public List<SelectListItem> Locations { get; set; }

        /// <summary>
        /// Gets or sets a list of user options for event association in the form of <see cref="SelectListItem"/>.
        /// </summary>
        public List<SelectListItem> Users { get; set; }
    }
}
