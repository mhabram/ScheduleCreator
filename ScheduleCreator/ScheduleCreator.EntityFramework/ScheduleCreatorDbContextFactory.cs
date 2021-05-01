using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.EntityFramework
{
    public class ScheduleCreatorDbContextFactory
    {
        string databasePath = string.Concat("Data Source = ", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\\ScheduleCreator\\ScheduleCreator.db");
        string directoryPath = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\\ScheduleCreator");
        
        public ScheduleCreatorDbContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ScheduleCreatorDbContext>();

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            optionsBuilder.UseSqlite(databasePath);
         
            return new ScheduleCreatorDbContext(optionsBuilder.Options);
        }
    }
}
