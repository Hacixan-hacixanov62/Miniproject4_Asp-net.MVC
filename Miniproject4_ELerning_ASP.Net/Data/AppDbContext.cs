using Microsoft.EntityFrameworkCore;
using Miniproject4_ELerning_ASP_MVC.Models;

namespace Miniproject4_ELerning_ASP_MVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
        }
    }
}
