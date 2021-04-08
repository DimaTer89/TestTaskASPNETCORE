using System;
using System.Collections.Generic;

namespace TestTask.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public List<LinkPhoto> LinkPhoto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
