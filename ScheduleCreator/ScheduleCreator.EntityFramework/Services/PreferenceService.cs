using ScheduleCreator.Domain.DTO.ScheduleView;
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

        public async Task UpdatePreferences(int employeeId, IList<PreferenceDay> preferences, sbyte holidays)
        {
            Preferences pref =  await GetPreferences(employeeId);
            await _preferenceRepository.UpdatePreferences(pref.PreferencesId, preferences, holidays);
        }

        public async Task<Preferences> GetPreferences(int employeId)
        {
            return await _preferenceRepository.GetPreferences(employeId, internalPreferenceId);
        }

        public async Task<List<PreferencesDTO>> GetPreferences()
        {
            List<PreferencesDTO> preferencesDTO = new();
            List<Preferences> preferences = await _preferenceRepository.GetPreferences(internalPreferenceId);

            for (int i = 0; i < preferences.Count; i++)
            {
                preferencesDTO.Add(new PreferencesDTO(preferences[i]));
            }

            return preferencesDTO;
        }
    }
}
