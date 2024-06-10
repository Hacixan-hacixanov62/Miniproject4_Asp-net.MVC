using Microsoft.EntityFrameworkCore;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Helpers.Extensions;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Abouts;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Course;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Socials;

namespace Miniproject4_ELerning_ASP_MVC.Services
{
    public class SocialService : ISocialService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SocialService(AppDbContext context,
                             IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(Social social)
        {
            await _context.Socials.AddAsync(social);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Social social)
        {

            _context.Socials.Remove(social);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string social)
        {
            return await _context.Socials.AnyAsync(m => m.Name.Trim() == social.Trim());
        }

        public async Task<IEnumerable<SocialVM>> GetAllAsync(int? take = null)
        {
            IEnumerable<Social> socials;
            if (take == null)
            {
                socials = await _context.Socials.ToListAsync();
            }
            else
            {
                socials = await _context.Socials.Take((int)take).ToListAsync();
            }

            return socials.Select(m => new SocialVM { Name = m.Name });

        }

        public async Task<Social> GetByIdAsync(int id)
        {
            return await _context.Socials.FindAsync(id);
        }
    }
}
