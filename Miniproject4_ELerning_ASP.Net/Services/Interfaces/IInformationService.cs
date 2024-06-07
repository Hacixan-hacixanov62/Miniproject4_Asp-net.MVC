using Miniproject4_ELerning_ASP_MVC.ViewModels.Informations;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Sliders;

namespace Miniproject4_ELerning_ASP_MVC.Services.Interfaces
{
    public interface IInformationService
    {
        Task<IEnumerable<InformationVM>> GetAllAsync(int? take = null);
        Task CreateAsync(InformationCreateVM request);
        Task<bool> ExistAsync(string title, string description);
        Task DeleteAsync(int id);
        Task<InformationVM> GetByIdAsync(int id);
    }
}
