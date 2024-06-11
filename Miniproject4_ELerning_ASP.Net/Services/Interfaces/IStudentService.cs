using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Student;

namespace Miniproject4_ELerning_ASP_MVC.Services.Interfaces
{
    public interface IStudentService
    {

        Task CreateAsync(Student student);
        Task DeleteAsync(Student student);
        Task EditAsync();
        Task<IEnumerable<StudentVM>> GetAllAsync(int? take = null);
        Task<Student> GetByIdAsync(int id);
        Task<bool> ExistAsync(string fullname);

    }
}
