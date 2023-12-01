using iPractice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPractice.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Psychologist> Psychologists { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeSlot>().HasKey(timeslot => timeslot.Id);
            modelBuilder.Entity<Appointment>().HasKey(appointment => appointment.Id);
            modelBuilder.Entity<Psychologist>().HasKey(psychologist => psychologist.Id);
            modelBuilder.Entity<Client>().HasKey(client => client.Id);

            modelBuilder.Entity<Psychologist>().HasMany(p => p.Clients).WithMany(b => b.Psychologists);
            modelBuilder.Entity<Psychologist>().HasMany(p => p.Appointments).WithOne(b => b.Psychologist);
            modelBuilder.Entity<Psychologist>().HasMany(p => p.Availability);

            modelBuilder.Entity<Client>().HasMany(p => p.Psychologists).WithMany(b => b.Clients);
            modelBuilder.Entity<Client>().HasMany(p => p.Appointments).WithOne(b => b.Client);
        }
    }
}
