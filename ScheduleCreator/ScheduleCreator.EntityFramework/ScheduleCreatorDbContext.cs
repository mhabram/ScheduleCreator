using Microsoft.EntityFrameworkCore;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.EntityFramework
{
    public class ScheduleCreatorDbContext : DbContext
    {
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<ScheduleModel> Schedules { get; set; }
        public DbSet<DateModel> Dates { get; set; }
        public DbSet<PreferencesModel> Preferences { get; set; }

        public ScheduleCreatorDbContext(DbContextOptions options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Schedule>().OwnsMany(e => e.Employees);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
