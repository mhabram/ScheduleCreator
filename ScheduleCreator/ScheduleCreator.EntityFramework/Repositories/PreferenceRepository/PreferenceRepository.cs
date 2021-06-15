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

        public async Task AddPreferences(int employeeId, Preferences preferences)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            Employee employee = await context.Employees.FindAsync(employeeId);
            preferences.Employee = employee;

            await context.Preferences.AddAsync(preferences);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePreferences(int preferenceId, Preferences preferences)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            Preferences pref = await context.Preferences
                .Include(p => p.PreferenceDays)
                .Where(p => p.PreferencesId == preferenceId)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync();
            List<PreferenceDay> preferenceDays = new();

            for (int i = 0; i < preferences.PreferenceDays.Count; i++)
            {
                preferenceDays.Add(new() { FreeDayChosen = preferences.PreferenceDays[i].FreeDayChosen });
            }

            pref.FreeWorkingDays = preferences.FreeWorkingDays;
            pref.PreferenceDays = preferenceDays;
            pref.From = preferences.From;
            pref.To = preferences.To;
            pref.LeaveDays = preferences.LeaveDays;
            context.Entry(pref);
            await context.SaveChangesAsync();

            List<Preferences> prefs = await context.Preferences
                .Include(p => p.PreferenceDays).ToListAsync();
            for (int i = 0; i < prefs.Count; i++)
            {
                for(int j = 0; j < prefs[i].PreferenceDays.Count; j++)
                {
                    if (prefs[i].PreferenceDays[j].Preferences == null)
                        context.PreferenceDays.Remove(prefs[i].PreferenceDays[j]);
                }
            }
            await context.SaveChangesAsync();
        }

        public async Task<Preferences> GetPreferences(int employeeId, string internalPreferenceId)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();

            Preferences preferences = await context.Preferences
                .Where(p => p.InternalPreferenceId == internalPreferenceId)
                .Where(e => e.Employee.EmployeeId == employeeId)
                .Include(d => d.PreferenceDays)
                .Include(e => e.Employee)
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

        public async Task DeleteFromTo(string internalPreferenceId, int employeeId)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();

            Preferences preferences = await context.Preferences
                .Where(p => p.InternalPreferenceId == internalPreferenceId)
                .Where(e => e.Employee.EmployeeId == employeeId).FirstOrDefaultAsync();

            preferences.From = "";
            preferences.To = "";
            context.Entry(preferences);
            await context.SaveChangesAsync();
        }
    }
}
