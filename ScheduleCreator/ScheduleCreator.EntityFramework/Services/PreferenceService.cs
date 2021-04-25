using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Repositories.EmployeeRepositories;
using ScheduleCreator.EntityFramework.Repositories.PreferenceRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IPreferenceRepository _preferenceRepository;

        public PreferenceService(IPreferenceRepository preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }

        public async Task AddPreferences(int employeId, IList<PreferenceDay> preferenceDays, sbyte holidays)
        {
            DateTime CurrentDate = DateTime.Now.AddMonths(1); // Adding 1 to cause we are creating schedule for the next month
            DateTime StartMonth = CurrentDate.AddDays(-CurrentDate.Day + 1); // Making the next month starting counting from 1
            int DaysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            string internalPreferenceId = String.Concat(StartMonth.Year.ToString(), StartMonth.Month.ToString());

            //sbyte WeekDays = holidays; // this should be moved to GetPreferences().
            //int Day = DaysInMonth - 1;
            //for(sbyte i = 1; i <= DaysInMonth; i++)
            //{
            //    string DayName = StartMonth.AddDays(-Day).DayOfWeek.ToString();
            //    if ((DayName == "Saturday") || (DayName == "Sunday"))
            //        WeekDays += 1;

            //    Day -= 1;
            //} 
            //sbyte FreeWorkingDays = (sbyte)(DaysInMonth - WeekDays);

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
            DateTime StartMonth = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day + 1);
            string internalPreferenceId = String.Concat(StartMonth.Year.ToString(), StartMonth.Month.ToString());

            return await _preferenceRepository.GetPreferences(employeId, internalPreferenceId);
        }
    }
}
