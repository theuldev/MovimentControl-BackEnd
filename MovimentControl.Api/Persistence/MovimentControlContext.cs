using Microsoft.EntityFrameworkCore;
using MovimentControl.Api.Models;

namespace MovimentControl.Api.Persistence
{
    public class MovimentControlContext : DbContext
    {
        public MovimentControlContext(DbContextOptions<MovimentControlContext> options) : base(options)
        {

        }
        public DbSet<ClientInputModel> _clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClientInputModel>(
                e => e.ToTable("tb_Client")
            );
        }
    }
}