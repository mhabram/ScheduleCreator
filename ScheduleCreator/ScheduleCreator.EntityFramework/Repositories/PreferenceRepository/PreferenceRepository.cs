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

        public async Task AddPreferences(int employeId, Preferences preferences)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            Employee employee = await context.Employees.FindAsync(employeId);
            preferences.Employee = employee;

            await context.Preferences.AddAsync(preferences);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePreferences(int preferenceId, IList<PreferenceDay> preferenceDays, sbyte holidays)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            Preferences pref = await context.Preferences
                .Include(p => p.PreferenceDays)
                .Where(p => p.PreferencesId == preferenceId)
                .FirstOrDefaultAsync();

            for (int i = 0; i < preferenceDays.Count; i++)
            {
                pref.PreferenceDays[i].FreeDayChosen = preferenceDays[i].FreeDayChosen;
            }

            pref.FreeWorkingDays = holidays;
            context.Entry(pref);
            await context.SaveChangesAsync();
        }

        public async Task<Preferences> GetPreferences(int employeId, string internalPreferenceId)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();

            Preferences preferences = await context.Preferences
                .Where(p => p.InternalPreferenceId == internalPreferenceId)
                .Where(e => e.Employee.EmployeeId == employeId)
                .Include(d => d.PreferenceDays)
                .FirstOrDefaultAsync();

            return preferences;
        }

        public async Task<List<Preferences>> GetPreferences(string internalPreferenceId)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();

            List<Preferences> preferences = await context.Preferences
                .Where(p => p.InternalPreferenceId == internalPreferenceId)
                .Include(d => d.PreferenceDays)
                .Include(e => e.Employee)
                .ToListAsync();

            return preferences;
        }
    }
}
