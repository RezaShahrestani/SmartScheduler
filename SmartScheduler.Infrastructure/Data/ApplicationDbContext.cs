using Microsoft.EntityFrameworkCore;
using SmartScheduler.Domain.Entities;

namespace SmartScheduler.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
