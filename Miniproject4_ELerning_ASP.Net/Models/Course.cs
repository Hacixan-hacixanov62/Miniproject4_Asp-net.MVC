namespace Miniproject4_ELerning_ASP_MVC.Models
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }
        public int Rating { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }
        public Category Category { get; set; }
        public Instructor Instructor { get; set; }
        public ICollection<CourseImage> CourseImages { get; set; }

    }
}
