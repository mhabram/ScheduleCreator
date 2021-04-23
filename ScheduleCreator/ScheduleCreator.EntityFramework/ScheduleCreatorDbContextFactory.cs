using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.EntityFramework
{
    public class ScheduleCreatorDbContextFactory
    {
        public ScheduleCreatorDbContext CreateDbContext(string[] args = null) // this works
        {
            var optionsBuilder = new DbContextOptionsBuilder<ScheduleCreatorDbContext>();
            optionsBuilder.UseSqlite("Data Source = C:\\Temp\\ScheduleCreator.db");
         
            return new ScheduleCreatorDbContext(optionsBuilder.Options);
        }
    }
}
