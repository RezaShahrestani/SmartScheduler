using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SmartScheduler.Domain.Entities;

namespace SmartScheduler.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("StrongPassword;)")
            };

            modelBuilder.Entity<User>().HasData(adminUser);
        }


    }

}
