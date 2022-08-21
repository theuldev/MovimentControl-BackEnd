using Microsoft.EntityFrameworkCore;
using MovimentControl.Api.Entities;
using MovimentControl.Api.Models;

namespace MovimentControl.Api.Persistence
{
    public class MovimentControlContext : DbContext
    {
        public MovimentControlContext(DbContextOptions<MovimentControlContext> options) : base(options)
        {
            
        }
        public DbSet<ClientInputModel> _clients => Set<ClientInputModel>();
        public DbSet<User> _users => Set<User>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClientInputModel>(
                e => e.ToTable("tb_Client")
            );
            builder.Entity<User>(
                e => e.ToTable("tb_User")
            );
        }
    }
}