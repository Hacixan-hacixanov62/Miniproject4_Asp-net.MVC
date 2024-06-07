using Microsoft.EntityFrameworkCore;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Abouts;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Informations;

namespace Miniproject4_ELerning_ASP_MVC.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutService(AppDbContext context,
                            IWebHostEnvironment env)
        {  
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(AboutCreateVM request)
        {
            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = _env.GenerateFilePath("img", fileName);

            await request.Image.SaveFileToLocalAsync(path);

            await _context.Abouts.AddAsync(new About
            {
                Title = request.Title,
                Description = request.Description,
                Image = fileName
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {

            var about = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == id);

            string imgPath = _env.GenerateFilePath("img", about.Image);
            imgPath.DeleteFileFromLocal();

            _context.Abouts.Remove(about);

            await _context.SaveChangesAsync();
        }

        public async  Task<bool> ExistAsync(string title, string description)
        {
            return await _context.Abouts.AnyAsync(m => m.Title.Trim() == title.Trim() || m.Description.Trim() == description.Trim());

        }

        public async Task<IEnumerable<AboutVM>> GetAllAsync(int? take = null)
        {
            IEnumerable<About> abouts;
            if (take == null)
            {
                abouts = await _context.Abouts.ToListAsync();
            }
            else
            {
                abouts = await _context.Abouts.Take((int)take).ToListAsync();
            }

            return abouts.Select(m => new AboutVM { Id = m.Id, Title = m.Title, Description = m.Description, Image = m.Image, CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy") });

        }

        public async Task<AboutVM> GetByIdAsync(int id)
        {
          About about = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == id);

            return new AboutVM
            {
                Id = about.Id,
                Title = about.Title,
                Description = about.Description,
                Image = about.Image,
                CreatedDate = about.CreatedDate.ToString("MM.dd.yyyy")
            };
        }
    }
}
