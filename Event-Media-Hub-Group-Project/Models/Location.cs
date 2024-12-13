using System.ComponentModel.DataAnnotations;

namespace Event_Media_Hub_Group_Project.Models
{
    /// <summary>
    /// Represents a location where events can be held, including details like name, address, and capacity.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets or sets the unique identifier for the location.
        /// </summary>
        [Key]
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the address of the location.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the maximum capacity of the location.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the collection of events that take place at this location.
        /// </summary>
        public virtual ICollection<Event> Events { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for a location, used for transferring location data.
    /// </summary>
    public class LocationDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the location.
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the address of the location.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the maximum capacity of the location.
        /// </summary>
        public int Capacity { get; set; }
    }
}
