using Microsoft.EntityFrameworkCore;
using paperProject.Models;

namespace paperProject.Services
{
    public class PaperProjectContext : DbContext
    {
        public DbSet<City> City { get; set; }
        public DbSet<Train> Train { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Ship> Ship { get; set; }
        public DbSet<TrainStation> TrainStation { get; set; }
        public DbSet<SearchLine> SearchLine { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=PaperProject.db");
        }
    }
}