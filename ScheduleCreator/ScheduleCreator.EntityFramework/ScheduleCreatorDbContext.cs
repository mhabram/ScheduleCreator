using Microsoft.EntityFrameworkCore;
using ScheduleCreator.Domain.Models;

namespace ScheduleCreator.EntityFramework
{
    public class ScheduleCreatorDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PreferenceDay> PreferenceDays { get; set; }
        public DbSet<Preferences> Preferences { get; set; }
        public DbSet<Day> Days { get; set; }

        public ScheduleCreatorDbContext(DbContextOptions<ScheduleCreatorDbContext> options) : base(options) { }
    }
}