using System.ComponentModel.DataAnnotations;

namespace Miniproject4_ELerning_ASP_MVC.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
