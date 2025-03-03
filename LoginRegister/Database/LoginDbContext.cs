using LoginRegister.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginRegister.Database
{
    public class LoginDbContext:DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext>options):base(options) 
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
