using CarWashFacility.Model;

using Microsoft.EntityFrameworkCore;

namespace CarWashFacility.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<WashingProgram> Programs { get; set; }
        public DbSet<ProgramStep> ProgramSteps { get; set; }
        public DbSet<Step> Steps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Activities)
                .HasForeignKey(a => a.CustomerId);

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Program)
                .WithMany(c => c.Activities)
                .HasForeignKey(a => a.ProgramId);

            modelBuilder.Entity<ProgramStep>()
                .HasOne(a => a.Program)
                .WithMany(c => c.ProgramSteps)
                .HasForeignKey(a => a.ProgramId);

            modelBuilder.Entity<ProgramStep>()
                .HasOne(a => a.Step)
                .WithMany(c => c.ProgramSteps)
                .HasForeignKey(a => a.StepId);
        }
    }
}
