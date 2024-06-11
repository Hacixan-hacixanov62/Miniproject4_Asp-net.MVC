using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Miniproject4_ELerning_ASP_MVC.Models;

namespace Miniproject4_ELerning_ASP_MVC.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Information>Informations { get; set; }
        public DbSet<About>Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Student> Students { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Information>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<About>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Instructor>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Course>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Social>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Student>().HasQueryFilter(m => !m.SoftDeleted);

            base.OnModelCreating(modelBuilder);

        }

    }
}
