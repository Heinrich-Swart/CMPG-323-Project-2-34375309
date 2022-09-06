using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _34375309_Project2.Authentication
{
    public class AppDBContext_Auth : IdentityDbContext<AppUser_Auth>
    {
        public AppDBContext_Auth(DbContextOptions<AppDBContext_Auth> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}