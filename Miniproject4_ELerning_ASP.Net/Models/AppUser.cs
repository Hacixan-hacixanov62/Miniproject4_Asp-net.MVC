using Microsoft.AspNetCore.Identity;

namespace Miniproject4_ELerning_ASP_MVC.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
