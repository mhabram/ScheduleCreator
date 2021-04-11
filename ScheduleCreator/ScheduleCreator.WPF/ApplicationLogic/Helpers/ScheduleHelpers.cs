using System;
using System.Collections.ObjectModel;

namespace ScheduleCreator.WPF.ApplicationLogic.Helpers
{
    public class ScheduleHelpers
    {
        public Collection<DateTime> CalendarDate()
        {
            Collection<DateTime> calendarDate = new Collection<DateTime>();


            DateTime currentDate = DateTime.Now.AddMonths(1);
            DateTime startMonth = currentDate.AddDays(-currentDate.Day + 1);

            for(int i = 0; i < DateTime.DaysInMonth(currentDate.Year, currentDate.Month); i++)
            {
                calendarDate.Add(startMonth.AddDays(i));
            }

            return calendarDate;
        }
    }
}
