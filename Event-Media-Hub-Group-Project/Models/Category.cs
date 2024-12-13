using System.ComponentModel.DataAnnotations;

namespace Event_Media_Hub_Group_Project.Models
{
    /// <summary>
    /// Represents a category in the system, typically used as a reference for grouping events or other entities.
    /// This class is used for database operations and contains the primary key as well as the name of the category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// This is the primary key for the category in the database.
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// The name is used to identify and group events or other items into categories.
        /// </summary>
        public string CategoryName { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) used to transfer category data between layers or over the network.
    /// This class is typically used in API responses or when interacting with external systems.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// This identifier is used for identification in the system.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// This is a readable name used for display purposes, such as "Music", "Sports", etc.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
