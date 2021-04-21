using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Repositories.DateRepository;
using System;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Services
{
    public class PreferenceDayService : IPreferenceDayService
    {
        private readonly IPreferenceDayRepository _preferenceDayRepository;

        public PreferenceDayService(IPreferenceDayRepository preferenceDayRepository)
        {
            _preferenceDayRepository = preferenceDayRepository;
        }

        public async Task<PreferenceDay> AddPreferenceDay(DateTime dateTime, int preferencesId)
        {
            PreferenceDay preferenceDay = new()
            {
                FreeDayChosen = dateTime
            };

            return await _preferenceDayRepository.AddPreferenceDay(preferenceDay, preferencesId);
        }
    }
}
