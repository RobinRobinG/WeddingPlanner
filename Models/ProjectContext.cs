using Microsoft.EntityFrameworkCore;
 
namespace WeddingPlanner.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<WeddingPlan> WeddingPlans { get; set; }
        public DbSet<WeddingGuest> WeddingGuests { get; set; }
    }
}