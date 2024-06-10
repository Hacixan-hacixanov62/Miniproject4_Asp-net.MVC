using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Abouts;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Categories;

namespace Miniproject4_ELerning_ASP_MVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
      

        public CategoryService( AppDbContext context,
                                IWebHostEnvironment env)
        {         
            _context = context;
            _env = env;
            
        }

        public async Task CreateAsync(CategoryCreateVM request)
        {
            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = _env.GenerateFilePath("img", fileName);

            await request.Image.SaveFileToLocalAsync(path);

            await _context.Categories.AddAsync(new Category
            {
                Name = request.Name,
                Image = fileName
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            string imgPath = _env.GenerateFilePath("img", category.Image);
            imgPath.DeleteFileFromLocal();

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<IEnumerable<CategoryVM>> GetAllAsync(int? take = null)
        {
            IEnumerable<Category> categories;
            if (take == null)
            {
                categories  = await _context.Categories.ToListAsync();
            }
            else
            {
                categories = await _context.Categories.Take((int)take).ToListAsync();
            }

            return categories.Select(m => new CategoryVM { Id = m.Id, Image = m.Image,Name = m.Name, CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy") });

        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
            var categories = await _context.Categories
                .ToListAsync();

            return new SelectList(categories, "Id", "Name");
        }


        public async Task<CategoryVM> GetByIdAsync(int id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            return new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                CreatedDate = category.CreatedDate.ToString("MM.dd.yyyy")
            };
        }
    }
}
