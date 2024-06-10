using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Course;

namespace Miniproject4_ELerning_ASP_MVC.Services.Interfaces
{
    public interface ICourseServicecs
    {
        Task<IEnumerable<CourseVM>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task<Course> GetByIdWithAllDatasAsync(int id);
        Task<IEnumerable<Course>> GetAllPaginateAsync(int page, int take);
        Task<int> GetCountAsync();
        IEnumerable<CourseAdminVM> GetMappedDatas(IEnumerable<Course> courses);
        Task CreateAsync(CourseCreateVM request);
        Task DeleteAsync(Course course);
        Task EditAsync(Course course, CourseEditVM request);
        //Task DeleteCourseImageAsync(MainAndDeleteImageVM data);
        //Task SetMainImageAsync(MainAndDeleteImageVM data);
    }
}
