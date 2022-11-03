using CRUDOPERATIONAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDOPERATIONAPI.Context
{
    public class CrudOperationReactContext : DbContext
    {
        public CrudOperationReactContext(DbContextOptions<CrudOperationReactContext> options) : base(options)
        {

        }
        public DbSet<SuperVillain> SuperVillain { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }

    }
}
