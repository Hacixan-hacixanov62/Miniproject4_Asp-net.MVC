using Miniproject4_ELerning_ASP_MVC.ViewModels.Abouts;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Informations;

namespace Miniproject4_ELerning_ASP_MVC.Services.Interfaces
{
    public interface IAboutService
    {
        Task<IEnumerable<AboutVM>> GetAllAsync(int? take = null);
        Task CreateAsync(AboutCreateVM request);
        Task<bool> ExistAsync(string title, string description);
        Task DeleteAsync(int id);
        Task<AboutVM> GetByIdAsync(int id);
    }
}
