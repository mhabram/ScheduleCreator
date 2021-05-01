using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Repositories.PreferenceRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IPreferenceRepository _preferenceRepository;
        private string internalPreferenceId = String.Concat(DateTime.Now.AddMonths(1).Year.ToString(),
            DateTime.Now.AddMonths(1).Month.ToString());

        public PreferenceService(IPreferenceRepository preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }

        public async Task AddPreferences(int employeId, IList<PreferenceDay> preferenceDays, sbyte holidays)
        {
            Preferences preferences = new Preferences
            {
                FreeWorkingDays = holidays,
                InternalPreferenceId = internalPreferenceId,
                PreferenceDays = preferenceDays
            };
            
            await _preferenceRepository.AddPreferences(employeId, preferences);
        }

        public async Task UpdatePreferences(int preferenceId, IList<PreferenceDay> preferences, sbyte holidays)
        {
            await _preferenceRepository.UpdatePreferences(preferenceId, preferences, holidays);
        }

        public async Task<Preferences> GetPreferences(int employeId)
        {
            return await _preferenceRepository.GetPreferences(employeId, internalPreferenceId);
        }
    }
}
