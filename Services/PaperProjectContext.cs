using Microsoft.EntityFrameworkCore;
using paperProject.Models;

namespace paperProject.Services
{
    public class PaperProjectContext:DbContext
    {
        public DbSet<City> City { get; set; }
        public DbSet<Train> Train{get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=PaperProject.db");
        }
    }
}