using Microsoft.EntityFrameworkCore;
using paperProject.Models;

namespace paperProject.Services
{
    public class PaperProjectContext:DbContext
    {
        public DbSet<City> City { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=PaperProject.db");
        }
    }
}