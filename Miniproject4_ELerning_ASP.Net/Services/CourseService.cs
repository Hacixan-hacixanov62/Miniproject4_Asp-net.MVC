using Microsoft.EntityFrameworkCore;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Categories;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Course;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Instructors;

namespace Miniproject4_ELerning_ASP_MVC.Services
{
    public class CourseService : ICourseServicecs
    {
        public readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;
        public CourseService(AppDbContext context,
                             IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        public async Task CreateAsync(CourseCreateVM request)
        {
            List<CourseImage> images = new();

            foreach (var item in request.Images)
            {
                string fileName = $"{Guid.NewGuid()}-{item.FileName}";

                string path = _env.GenerateFilePath("img", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new CourseImage { Name = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            Course course = new()
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                InstructorId = request.InstructorId,
                Rating = request.Rating,
                Price = decimal.Parse(request.Price),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CourseImages = images
            };

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Course course)
        {

            foreach (var item in course.CourseImages)
            {
                string path = _env.GenerateFilePath("img", item.Name);
                path.DeleteFileFromLocal();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteCourseImageAsync(MainAndDeleteImageVM data)
        //{
        //    var course = await _context.Courses
        //        .Where(m => m.Id == data.CourseId)
        //        .Include(m => m.CourseImages)
        //        .FirstOrDefaultAsync();

        //    var courseImage = course.CourseImages.FirstOrDefault(m => m.Id == data.ImageId);

        //    _context.CourseImages.Remove(courseImage);
        //    await _context.SaveChangesAsync();

        //    string path = _env.GenerateFilePath("assets/images", courseImage.Name);
        //    path.DeleteFileFromLocal();
        //}

        public async Task EditAsync(Course course, CourseEditVM request)
        {
            if (request.NewImages is not null)
            {
                foreach (var item in request.NewImages)
                {
                    string fileName = $"{Guid.NewGuid()}-{item.FileName}";

                    string path = _env.GenerateFilePath("img", fileName);

                    await item.SaveFileToLocalAsync(path);

                    course.CourseImages.Add(new CourseImage { Name = fileName });
                }
            }

            course.Name = request.Name;
            course.Description = request.Description;
            course.CategoryId = request.CategoryId;
            course.InstructorId = request.InstructorId;
            course.Rating = request.Rating;
            course.Price = decimal.Parse(request.Price);
            course.StartDate = request.StartDate;
            course.EndDate = request.EndDate;
            course.CategoryId = request.CategoryId;


            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseVM>> GetAllAsync()
        {
            return await _context.Courses
                .Include(m => m.CourseImages)
                .Include(m => m.Category)
                .Include(m => m.Instructor)
                .Select(m => new CourseVM
                {
                    Name = m.Name,
                    Price = m.Price,
                    Rating = m.Rating,
                    Category = m.Category.Name,
                    Instructor = m.Instructor.FullName,
                    MainImage = m.CourseImages.FirstOrDefault(i => i.IsMain).Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Courses
               .OrderByDescending(m => m.Id)
               .Skip((page - 1) * take)
               .Take(take)
               .Include(m => m.CourseImages)
               .Include(m => m.Category)
               .ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<Course> GetByIdWithAllDatasAsync(int id)
        {
            return await _context.Courses
              .Where(m => m.Id == id)
              .Include(m => m.Category)
              .Include(m => m.Instructor)
              .Include(m => m.CourseImages)
              .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Courses.CountAsync();
        }

        public IEnumerable<CourseAdminVM> GetMappedDatas(IEnumerable<Course> courses)
        {
            return courses.Select(m => new CourseAdminVM
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                CategoryId = m.CategoryId,
                InstructorId = m.InstructorId,
                Rating = m.Rating,
                MainImage = m.CourseImages.FirstOrDefault(i => i.IsMain)?.Name
            });
        }

        //public async Task SetMainImageAsync(MainAndDeleteImageVM data)
        //{
        //    var course = await _context.Courses
        //     .Where(m => m.Id == data.CourseI)
        //     .Include(m => m.CourseImages)
        //     .FirstOrDefaultAsync();

        //    var courseImage = course.CourseImages.FirstOrDefault(m => m.Id == data.ImageId);

        //    course.CourseImages.FirstOrDefault(m => m.IsMain).IsMain = false;
        //    courseImage.IsMain = true;
        //    await _context.SaveChangesAsync();
        //}
    }
}
