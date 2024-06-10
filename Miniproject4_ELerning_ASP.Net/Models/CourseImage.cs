namespace Miniproject4_ELerning_ASP_MVC.Models
{
    public class CourseImage : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
