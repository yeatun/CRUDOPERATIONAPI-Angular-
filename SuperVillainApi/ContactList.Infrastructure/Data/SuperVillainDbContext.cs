using ContactList.Core.Entities;
using ContactList.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ContactList.Infrastructure.Data
{
    public class SuperVillainDbContext : IdentityDbContext <ApplicationUser>
    {
        public SuperVillainDbContext(DbContextOptions<SuperVillainDbContext> options) : base(options)
        {

        }

        public DbSet<SuperVillain> SuperVillain { get; set; }
    }
}
