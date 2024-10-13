using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tanki.Domain.Models;

namespace Tanki.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }

        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}