﻿using System.ComponentModel.DataAnnotations;

namespace Event_Media_Hub_Group_Project.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
    public class LocationDto
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
    }
}
