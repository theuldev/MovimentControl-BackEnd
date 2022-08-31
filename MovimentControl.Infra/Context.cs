using Microsoft.EntityFrameworkCore;
using MovimentControl.Domain.Models;
using MovimentControl.Domain.Models.Client;


namespace MovimentControl.Infra
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=DESKTOP-0S1A340;Database=ClientDB;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }

        }
        public DbSet<Client> _clients => Set<Client>();
        public DbSet<UserInputModel> _users => Set<UserInputModel>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>(
                e => e.ToTable("tb_Clients")
            );
            builder.Entity<UserInputModel>(
                e => e.ToTable("tb_Users")
            );
        }
    }
}
