using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Sliders;

namespace Miniproject4_ELerning_ASP_MVC.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<SliderVM>> GetAllAsync(int? take = null);
        Task CreateAsync(SliderCreateVM request);
        Task<bool> ExistAsync(string title, string description);
        Task DeleteAsync(int id);
        Task<SliderVM> GetByIdAsync(int id);

    }
}
