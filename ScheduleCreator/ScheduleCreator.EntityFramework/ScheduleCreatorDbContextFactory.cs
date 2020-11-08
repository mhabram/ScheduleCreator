using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.EntityFramework
{
    public class ScheduleCreatorDbContextFactory : IDesignTimeDbContextFactory<ScheduleCreatorDbContext>
    {
        public ScheduleCreatorDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<ScheduleCreatorDbContext>();
            options.UseSqlite("Data Source = ScheduleCreator.db");

            return new ScheduleCreatorDbContext(options.Options);
        }
    }
}
