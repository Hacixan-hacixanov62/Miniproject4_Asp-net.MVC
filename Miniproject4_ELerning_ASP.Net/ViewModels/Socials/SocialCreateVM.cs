using System.ComponentModel.DataAnnotations;

namespace Miniproject4_ELerning_ASP_MVC.ViewModels.Socials
{
    public class SocialCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(50)]
        public string Name{ get; set; }
       
    }
}
