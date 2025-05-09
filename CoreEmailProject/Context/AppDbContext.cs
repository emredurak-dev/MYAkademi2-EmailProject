using CoreEmailProject.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreEmailProject.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-B3ABM7G;initial catalog=DbCoreEmail;integrated security=true;trust server certificate=true;");
        }
        public DbSet<EmailMessage> EmailMessages { get; set; }
    }
}
