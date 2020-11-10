using Microsoft.EntityFrameworkCore;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.EntityFramework
{
    public class ScheduleCreatorDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public ScheduleCreatorDbContext(DbContextOptions options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Schedule>().OwnsMany(e => e.Employees);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
