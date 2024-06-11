using Microsoft.EntityFrameworkCore;
using Miniproject4_ELerning_ASP_MVC.Data;
using Miniproject4_ELerning_ASP_MVC.Models;
using Miniproject4_ELerning_ASP_MVC.Services.Interfaces;
using Miniproject4_ELerning_ASP_MVC.ViewModels.Student;

namespace Miniproject4_ELerning_ASP_MVC.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Student student)
        {
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string fullname)
        {
            return await _context.Students.AnyAsync(m => m.FullName.Trim() == fullname.Trim());
        }

        public async Task<IEnumerable<StudentVM>> GetAllAsync(int? take = null)
        {
            IEnumerable<Student> student;

            if (take is null)
            {
                student = await _context.Students.ToListAsync();
            }
            else
            {
                student = await _context.Students.Take((int)take).ToListAsync();
            }
            return student.Select(m => new StudentVM { Id = m.Id, FullName = m.FullName, Profession = m.Profession, Image = m.Image, Biography = m.Biography });
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.Where(m => m.Id == id).FirstOrDefaultAsync();
        }
    }
}
