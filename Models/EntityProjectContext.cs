using Microsoft.EntityFrameworkCore;
 
namespace EntityProject.Models
{
    public class EntityProjectContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Activity> activities { get; set; }
        public DbSet<Participant> participants { get; set; }
        // base() calls the parent class' constructor passing the "options" parameter along
        public EntityProjectContext(DbContextOptions<EntityProjectContext> options) : base(options) { }
    }
}
