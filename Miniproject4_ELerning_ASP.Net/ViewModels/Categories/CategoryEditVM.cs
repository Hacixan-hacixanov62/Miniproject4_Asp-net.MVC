using System.ComponentModel.DataAnnotations;

namespace Miniproject4_ELerning_ASP_MVC.ViewModels.Categories
{
    public class CategoryEditVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
