using ScheduleCreator.Domain.Helpers.Calendar;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Repositories.WeekRepository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Services
{
    public class WeekService : IWeekService
    {
        private readonly IWeekRepository _weekRepository;
        private readonly CalendarHelper _calendarHelper = new ();

        public WeekService(IWeekRepository weekRepository)
        {
            _weekRepository = weekRepository;
        }

        public async Task<bool> AddWeeks(string fullName, ICollection<Day> days)
        {
            IDictionary<int, int> keyEndOfWeek = _calendarHelper.Weeks();
            IDictionary<int, ICollection<Day>> dictionaryOfWeeks = new Dictionary<int, ICollection<Day>>();
            ICollection<Week> weeks = new Collection<Week>();
            Week week;

            string lastName = fullName.Split()[1];
            DateTime currentDate = DateTime.Now.AddMonths(1);
            int startMonth = currentDate.AddDays(-DateTime.Now.AddMonths(1).Day + 1).Day;
            int startWeek;
            int endWeek;

            foreach (var key in keyEndOfWeek)
            {
                startWeek = startMonth + key.Value - 7;
                endWeek = startMonth + key.Value - 1;

                if (key.Key == 1)
                    startWeek = startMonth;

                if (key.Key == keyEndOfWeek.Count)
                    startWeek = startMonth + keyEndOfWeek.ElementAt(keyEndOfWeek.Count - 2).Value;

                foreach (Day d in days)
                {
                    
                    if ((startWeek <= d.WorkingDay.Day) && (endWeek >= d.WorkingDay.Day))
                    {
                        if (dictionaryOfWeeks.ContainsKey(key.Key))
                            dictionaryOfWeeks[key.Key].Add(d);
                        else
                            dictionaryOfWeeks.Add(key.Key, new Collection<Day> { d });
                    }
                }
            }


            foreach (var key in dictionaryOfWeeks)
            {
                week = new Week() { Days = new Collection<Day>() };
                string internalWeekId = String.Concat(currentDate.Year.ToString(), currentDate.Month.ToString(), key.Key);

                foreach (Day d in key.Value)
                {
                    week.Days.Add(d);
                    week.InternalWeekId = internalWeekId;
                    weeks.Add(week);
                }
            }

            return await _weekRepository.SaveWeeks(weeks, lastName.ToLower());
        }
    }
}
