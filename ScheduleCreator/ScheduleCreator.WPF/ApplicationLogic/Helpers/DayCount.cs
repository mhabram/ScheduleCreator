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
