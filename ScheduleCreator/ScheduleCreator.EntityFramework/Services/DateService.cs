using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Repositories.DateRepository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Services
{
    public class DateService : IDateService
    {
        private readonly IDateRepository _dateRepository;

        public DateService(IDateRepository dateRepository)
        {
            _dateRepository = dateRepository;
        }

        public async Task<Date> AddDate(DateTime dateTime, int preferencesId)
        {
            Date date = new Date()
            {
                FreeDayChosen = dateTime
            };

            return await _dateRepository.AddDate(date, preferencesId);
        }
    }
}
