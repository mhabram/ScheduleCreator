using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.WPF.ApplicationLogic.Helpers
{
    public class DayCount
    {
        public byte DaysAfterMonday()
        {
            DateTime currentDate = DateTime.Now.AddMonths(1);
            DateTime startMonth = currentDate.AddDays(-currentDate.Day + 1);

            byte daysAfterMonday = 0;

            while (startMonth.DayOfWeek.ToString() != "Monday")
            {
                daysAfterMonday++;
                startMonth = startMonth.AddDays(-1);
            }

            return daysAfterMonday;
        }

        public sbyte WorkingDaysInMonth(sbyte freeDays)
        {
            DateTime currentDate = DateTime.Now.AddMonths(1);
            DateTime startMonth = currentDate.AddDays(-currentDate.Day + 1);
            sbyte workingDays = 0;

            for (int i = 0; i < DateTime.DaysInMonth(currentDate.Year, currentDate.Month); i++)
            {
                if ((startMonth.AddDays(i).DayOfWeek.ToString() != "Sunday") && (startMonth.AddDays(i).DayOfWeek.ToString() != "Saturday"))
                    workingDays++;
            }

            return workingDays;
        }

        public IDictionary<int, int> Weeks()
        {
            IDictionary<int, int> weeks = new Dictionary<int, int>();
            DateTime currentDate = DateTime.Now.AddMonths(1);
            DateTime startMonth = currentDate.AddDays(-currentDate.Day + 1);
            DateTime checkWeek = startMonth;

            sbyte daysAfterMonday = 0;

            // Week 1
            while (startMonth.DayOfWeek.ToString() != "Monday")
            {
                daysAfterMonday++;
                startMonth = startMonth.AddDays(-1);
            }

            // Weeks 2 - 4
            weeks.Add(1, checkWeek.AddDays(6 - daysAfterMonday).Day);
            for (byte i = 2; i <= 4; i++)
            {
                int x = (weeks.ElementAt(i - 2).Value + 6);
                weeks.Add(i, checkWeek.AddDays(x).Day);
            }

            // Week 5
            while (checkWeek.Day != DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
            {
                checkWeek = checkWeek.AddDays(1);
            }
            weeks.Add(5, checkWeek.Day);

            return weeks;
        }


        public List<DateTime> ColleagueDays(Week weeks = null)
        {
            List<DateTime> colleagueDays = new();

            if (weeks != null)
            {
                foreach (Day day in weeks.Days)
                {
                    colleagueDays.Add(day.WorkingDay.Date);
                }
                if (colleagueDays.Count < 7)
                {
                    for (int i = colleagueDays.Count; i < 7; i++)
                    {
                        colleagueDays.Add(DateTime.Now.AddMonths(-3).Date);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    colleagueDays.Add(DateTime.Now.AddMonths(-3).Date);
                }
            }

            return colleagueDays;
        }

        public List<DateTime> PreferenceDays(Preferences preferences)
        {
            List<DateTime> preferenceDays = new();

            foreach (Date day in preferences.Dates)
            {
                preferenceDays.Add(day.FreeDayChosen.Date);
            }

            return preferenceDays;
        }
    }
}
