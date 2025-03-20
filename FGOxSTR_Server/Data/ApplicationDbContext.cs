using FGOxSTR_Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace FGOxSTR_Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public int GetUserCount()
        {   
            return Users.Count();
        }
    }
}