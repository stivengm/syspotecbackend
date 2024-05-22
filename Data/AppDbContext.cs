using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Gender> Gender { get; set; }

        public DbSet<Company> Company { get; set; }

        public DbSet<IdentificationType> IdentificationType { get; set; }

        public DbSet<UserContract> UserContract { get; set; }

        public DbSet<State> State { get; set; }

        public DbSet<Contract> Contract { get; set; }

    }
}
