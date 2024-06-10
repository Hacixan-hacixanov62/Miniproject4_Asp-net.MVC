using System.ComponentModel.DataAnnotations;

namespace Miniproject4_ELerning_ASP_MVC.ViewModels.Instructors
{
    public class InstructorCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        public string Field { get; set; }

        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Address { get; set; }

        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
