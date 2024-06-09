using Miniproject4_ELerning_ASP_MVC.ViewModels.Abouts;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Categories;

namespace Miniproject4_ELerning_ASP_MVC.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryVM>> GetAllAsync(int? take = null);
        Task CreateAsync(CategoryCreateVM request);
        Task<bool> ExistAsync(string name);
        Task DeleteAsync(int id);
        Task<CategoryVM> GetByIdAsync(int id);
    }
}
