using Microsoft.AspNetCore.Hosting;
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
            DbContextOptionsBuilder<ScheduleCreatorDbContext> options = new DbContextOptionsBuilder<ScheduleCreatorDbContext>();
            //options.UseSqlite($"Data Source = {_appHost.ContentRootPath}/ScheduleCreator.db");
            options.UseSqlServer("Server=LAPTOP-9TI2U43D\\MSSQLSERVER01;Database=ScheduleCreator;Trusted_Connection=True;");

            return new ScheduleCreatorDbContext(options.Options);
        }
    }
}
