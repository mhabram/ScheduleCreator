using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.PreferenceRepository
{
    public class PreferenceRepository : IPreferenceRepository
    {
        private readonly ScheduleCreatorDbContextFactory _contextFactory;

        public PreferenceRepository(ScheduleCreatorDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Preferences> AddPreference(Preferences preferences, int employeId)
        {
            using (ScheduleCreatorDbContext context = _contextFactory.CreateDbContext())
            {
                Employee employee =  await context.Employees.FindAsync(employeId);
                preferences.Employee = employee;


                await context.Preferences.AddAsync(preferences);
                await context.SaveChangesAsync();

                return preferences;
            }
        }
    }
}
