using ScheduleCreator.Domain.DTO.PreferenceView;
using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Repositories.PreferenceRepository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IPreferenceRepository _preferenceRepository;
        private string internalPreferenceId = String.Concat(DateTime.Now.AddMonths(1).Year.ToString(),
            DateTime.Now.AddMonths(1).Month.ToString());


        private CultureInfo Culture = new CultureInfo("en-NZ", true);

        public PreferenceService(IPreferenceRepository preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }

        public async Task AddPreferences(int employeeId, ObservableCollection<DateTimeWrapper> preferenceDays,
            DateTime? from, DateTime? to, int leaveDays, sbyte holidays)
        {
            IList<PreferenceDay> preferenceDateDays = new List<PreferenceDay>();
            Preferences preferences = new();
            DateTime fromDate = new();
            DateTime toDate = new();

            for (int i = 0; i < preferenceDays.Count; i++)
            {
                if (preferenceDays[i].Value != null)
                {
                    preferenceDateDays.Add(new PreferenceDay() { FreeDayChosen = (DateTime)preferenceDays[i].Value });
                }
            }

            if ((from != null) || (to != null))
            {
                fromDate = (DateTime)from;
                toDate = (DateTime)to;

                preferences = new()
                {
                    FreeWorkingDays = holidays,
                    From = fromDate.ToString(Culture),
                    To = toDate.ToString(Culture),
                    LeaveDays = leaveDays,
                    InternalPreferenceId = internalPreferenceId,
                    PreferenceDays = preferenceDateDays
                };
            }
            else
            {
                preferences = new()
                {
                    FreeWorkingDays = holidays,
                    LeaveDays = leaveDays,
                    InternalPreferenceId = internalPreferenceId,
                    PreferenceDays = preferenceDateDays
                };
            }

            await _preferenceRepository.AddPreferences(employeeId, preferences);
        }

        public async Task UpdatePreferences(int employeeId, ObservableCollection<DateTimeWrapper> preferenceDays,
            DateTime? from, DateTime? to, int leaveDays, sbyte holidays)
        {
            Preferences pref =  await GetPreferences(employeeId);
            IList<PreferenceDay> preferenceDateDays = new List<PreferenceDay>();
            Preferences preferences = new();
            DateTime fromDate = new();
            DateTime toDate = new();

            for (int i = 0; i < preferenceDays.Count; i++)
            {
                if (preferenceDays[i].Value != null)
                {
                    preferenceDateDays.Add(new PreferenceDay() { FreeDayChosen = (DateTime)preferenceDays[i].Value });
                }
            }

            if ((from != null) || (to != null))
            {
                fromDate = (DateTime)from;
                toDate = (DateTime)to;

                preferences = new()
                {
                    FreeWorkingDays = holidays,
                    From = fromDate.ToString(Culture),
                    To = toDate.ToString(Culture),
                    LeaveDays = leaveDays,
                    InternalPreferenceId = internalPreferenceId,
                    PreferenceDays = preferenceDateDays
                };
            }
            else
            {
                await _preferenceRepository.DeleteFromTo(internalPreferenceId, employeeId);

                preferences = new()
                {
                    FreeWorkingDays = holidays,
                    From = "",
                    To = "",
                    LeaveDays = leaveDays,
                    InternalPreferenceId = internalPreferenceId,
                    PreferenceDays = preferenceDateDays
                };
            }

            await _preferenceRepository.UpdatePreferences(pref.PreferencesId, preferences);
        }

        public async Task<Preferences> GetPreferences(int employeeId)
        {
            Preferences preferences = await _preferenceRepository.GetPreferences(employeeId, internalPreferenceId);
            if (preferences.PreferenceDays.Count > 1)
                preferences.PreferenceDays = preferences.PreferenceDays.OrderBy(x => x.FreeDayChosen).ToList();
            return preferences;
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
