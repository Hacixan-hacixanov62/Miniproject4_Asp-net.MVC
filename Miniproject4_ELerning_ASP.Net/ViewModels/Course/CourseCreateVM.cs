using System.ComponentModel.DataAnnotations;

namespace Miniproject4_ELerning_ASP_MVC.ViewModels.Course
{
    public class CourseCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }
        public int Rating { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
