using Microsoft.EntityFrameworkCore;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public async Task<ICollection<Preferences>> GetPreferences(string internalPreferenceId)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();

            List<Preferences> preferences = await context.Preferences.Where(p => p.InternalPreferenceId == internalPreferenceId)
                .ToListAsync();

            return preferences;
        }
    }
}
