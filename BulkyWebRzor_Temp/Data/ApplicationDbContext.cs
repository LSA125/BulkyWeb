using Microsoft.EntityFrameworkCore;
namespace BulkyWebRzor_Temp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BulkyWebRzor_Temp.Models.Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BulkyWebRzor_Temp.Models.Category>().HasData(
                               new BulkyWebRzor_Temp.Models.Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                                new BulkyWebRzor_Temp.Models.Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                                 new BulkyWebRzor_Temp.Models.Category { Id = 3, Name = "History", DisplayOrder = 3 });
        }
    }
}
