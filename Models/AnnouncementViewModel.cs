using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    public class AnnouncementViewModel
    {
        [Required(ErrorMessage = "The title must be named")]
        [MaxLength(100, ErrorMessage = "To much letters in the word.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The description must be named")]
        [MaxLength(1000, ErrorMessage = "To much letters in the word.")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CreatedDate { get; set; }
        public string MainLinkPhoto { get; set; }
        public bool isFullAd { get; set; }
    }
}
