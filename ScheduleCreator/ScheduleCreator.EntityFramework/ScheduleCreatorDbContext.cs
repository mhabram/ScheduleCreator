using Microsoft.EntityFrameworkCore;
using ScheduleCreator.Domain.Models;

namespace ScheduleCreator.EntityFramework
{
    public class ScheduleCreatorDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Date> Dates { get; set; }
        public DbSet<Preferences> Preferences { get; set; }
        public DbSet<EmployeeSchedule> EmployeeSchedules { get; set; }

        public ScheduleCreatorDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeSchedule>()
                .HasOne(e => e.Employee)
                .WithMany(es => es.EmployeeSchedules)
                .HasForeignKey(ei => ei.EmployeeId);

            modelBuilder.Entity<EmployeeSchedule>()
                .HasOne(s => s.Schedule)
                .WithMany(es => es.EmployeeSchedules)
                .HasForeignKey(si => si.ScheduleId);
            //modelBuilder.Entity<Schedule>().OwnsMany(e => e.Employees);
            //base.OnModelCreating(modelBuilder);
        }
    }
}
