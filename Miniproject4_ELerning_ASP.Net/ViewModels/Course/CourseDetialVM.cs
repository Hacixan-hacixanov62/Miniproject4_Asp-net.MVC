using Miniproject4_ELerning_ASP_MVC.Models;

namespace Miniproject4_ELerning_ASP_MVC.ViewModels.Course
{
    public class CourseDetialVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Instructor { get; set; }
        public string Category{ get; set; }
        public int Rating { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public ICollection<CourseImageVM> CourseImages { get; set; }
    }
}
