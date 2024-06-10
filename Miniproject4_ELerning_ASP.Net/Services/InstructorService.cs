using Microsoft.EntityFrameworkCore;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Categories;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Instructors;
using System.Xml.Linq;

namespace Miniproject4_ELerning_ASP_MVC.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public InstructorService( AppDbContext context,
                                  IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            
        }

        public async Task CreateAsync(InstructorCreateVM request)
        {
            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = _env.GenerateFilePath("img", fileName);

            await request.Image.SaveFileToLocalAsync(path);

            await _context.Instructors.AddAsync(new Instructor
            {
                FullName = request.FullName,
                Field = request.Field,
                Address = request.Address,
                Image = fileName,
                Email = request.Email
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id);

            string imgPath = _env.GenerateFilePath("img", instructor.Image);
            imgPath.DeleteFileFromLocal();

            _context.Instructors.Remove(instructor);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string email)
        {
            return await _context.Instructors.AnyAsync(m => m.Email.Trim() == email.Trim());
        }

        public async Task<IEnumerable<InstructorVM>> GetAllAsync(int? take = null)
        {

            IEnumerable<Instructor> instruc;
            if (take == null)
            {
                instruc = await _context.Instructors.ToListAsync();
            }
            else
            {
                instruc = await _context.Instructors.Take((int)take).ToListAsync();
            }

            return instruc.Select(m => new InstructorVM { Id = m.Id, Image = m.Image,
                                                          FullName = m.FullName,Field = m.Field,
                                                          Email = m.Email,Address = m.Address,
            });

        }

        public async Task<InstructorVM> GetByIdMappedAsync(int id)
        {
            Instructor instruc = await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id);

            return new InstructorVM
            {
                Id = instruc.Id,
                FullName = instruc.FullName,
                Field = instruc.Field,
                Email = instruc.Email,
                Image = instruc.Image,
            };
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            return  await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
