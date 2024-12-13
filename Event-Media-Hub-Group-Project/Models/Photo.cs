using Event_Media_Hub_Group_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Event_Media_Hub_Group_Project.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime UploadDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }

    public class PhotoDto
    {
        public int PhotoId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTime UploadDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<UserDto> Users { get; internal set; }
    }
}