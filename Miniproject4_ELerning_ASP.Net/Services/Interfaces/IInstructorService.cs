using Microsoft.AspNetCore.Mvc.Rendering;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Categories;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Instructors;

namespace Miniproject4_ELerning_ASP_MVC.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorVM>> GetAllAsync(int? take = null);
        Task<bool> ExistAsync(string email);
        Task CreateAsync(InstructorCreateVM request);
        Task DeleteAsync(int id);
        Task<InstructorVM> GetByIdMappedAsync(int id);
        Task<Instructor> GetByIdAsync(int id);
        Task<SelectList> GetAllSelectedAsync();
    }
}
