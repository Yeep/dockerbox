using Microsoft.EntityFrameworkCore;

namespace api.DataContext
{
    public class BoxTaskDBContext : DbContext
    {
        public BoxTaskDBContext(DbContextOptions<BoxTaskDBContext> options)
            : base(options)
        {
        }

        public DbSet<Models.BoxTask> Tasks { get; set; }
    }
}