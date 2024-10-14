using Microsoft.EntityFrameworkCore;
using Tanki.Domain.Models;
using Tanki.Persistence.Configs;

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
            modelBuilder.ApplyConfiguration(new UserModelBuilder());
            modelBuilder.ApplyConfiguration(new RoomModelBuilder());

            base.OnModelCreating(modelBuilder);
        }
    }
}