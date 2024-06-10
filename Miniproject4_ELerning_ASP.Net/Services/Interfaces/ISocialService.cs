using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Course;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Sliders;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Socials;

namespace Miniproject4_ELerning_ASP_MVC.Services.Interfaces
{
    public interface ISocialService
    {
        Task DeleteAsync(Social social);
        Task<bool> ExistAsync(string social);
        Task CreateAsync( Social social);
        Task<IEnumerable<SocialVM>> GetAllAsync(int? take = null);
        Task<Social> GetByIdAsync(int id);
 
         Task EditAsync();

    }
}
