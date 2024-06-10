namespace Miniproject4_ELerning_ASP_MVC.ViewModels.Course
{
    public class CourseVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }
        public int Rating { get; set; }
        public string MainImage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
