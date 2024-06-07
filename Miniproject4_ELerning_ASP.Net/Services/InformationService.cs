using Microsoft.EntityFrameworkCore;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Informations;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Sliders;

namespace Miniproject4_ELerning_ASP_MVC.Services
{
    public class InformationService : IInformationService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public InformationService(AppDbContext context,
                                  IWebHostEnvironment env)

        {
            _context = context;
            _env = env;
            
        }

        public async Task CreateAsync(InformationCreateVM request)
        {
            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = _env.GenerateFilePath("img", fileName);

            await request.Image.SaveFileToLocalAsync(path);

            await _context.Informations.AddAsync(new Information
            {
                Title = request.Title,
                Description = request.Description,
                Image = fileName
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ınformation = await _context.Informations.FirstOrDefaultAsync(m => m.Id == id);

            string imgPath = _env.GenerateFilePath("img", ınformation.Image);
            imgPath.DeleteFileFromLocal();

            _context.Informations.Remove(ınformation);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string title, string description)
        {
            return await _context.Informations.AnyAsync(m => m.Title.Trim() == title.Trim() || m.Description.Trim() == description.Trim());
        }

        public async Task<IEnumerable<InformationVM>> GetAllAsync(int? take = null)
        {
            IEnumerable<Information> informations;
            if (take == null)
            {
                informations = await _context.Informations.ToListAsync();
            }
            else
            {
                informations = await _context.Informations.Take((int)take).ToListAsync();
            }

            return informations.Select(m => new InformationVM { Id = m.Id, Title = m.Title, Description = m.Description, Image = m.Image, CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy") });

        }

        public async Task<InformationVM> GetByIdAsync(int id)
        {
            Information ınformation = await _context.Informations.FirstOrDefaultAsync(m => m.Id == id);

            return new InformationVM
            {
                Id = ınformation.Id,
                Title = ınformation.Title,
                Description = ınformation.Description,
                Image = ınformation.Image,
                CreatedDate = ınformation.CreatedDate.ToString("MM.dd.yyyy")
            };
        }
    }
}
