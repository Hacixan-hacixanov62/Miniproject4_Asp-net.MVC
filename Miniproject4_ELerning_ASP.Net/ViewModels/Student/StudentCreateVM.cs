namespace Miniproject4_ELerning_ASP_MVC.ViewModels.Student
{
    public class StudentCreateVM
    {
        public string FullName { get; set; }
        public string? Biography { get; set; }
        public string Profession { get; set; }
        public IFormFile Image { get; set; }

    }
}
