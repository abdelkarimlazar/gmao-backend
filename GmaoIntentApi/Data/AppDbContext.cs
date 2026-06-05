using Microsoft.EntityFrameworkCore;
using GmaoIntentApi.Models;

namespace GmaoIntentApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<MaintenanceTask> MaintenanceTasks { get; set; }
        public DbSet<Breakdown> Breakdowns { get; set; }
        public DbSet<InterventionHistory> InterventionHistories { get; set; }
        public DbSet<IntentDemandeIntervention> IntentDemandes { get; set; }
        public DbSet<IntentHistoriqueStatut> IntentHistoriques { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(user => user.Role)
                .HasMaxLength(50)
                .HasDefaultValue("User");

            modelBuilder.Entity<Equipment>()
                .Property(equipment => equipment.Status)
                .HasConversion<int>();

            modelBuilder.Entity<MaintenanceTask>()
                .Property(task => task.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Breakdown>()
                .Property(breakdown => breakdown.Status)
                .HasConversion<int>();

            modelBuilder.Entity<MaintenanceTask>()
                .HasOne(task => task.User)
                .WithMany()
                .HasForeignKey(task => task.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Breakdown>()
                .HasOne(breakdown => breakdown.User)
                .WithMany()
                .HasForeignKey(breakdown => breakdown.ReportedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}