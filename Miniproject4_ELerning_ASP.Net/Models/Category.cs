using System.ComponentModel.DataAnnotations;

namespace Miniproject4_ELerning_ASP_MVC.Models
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Image { get; set; }
    }
}
