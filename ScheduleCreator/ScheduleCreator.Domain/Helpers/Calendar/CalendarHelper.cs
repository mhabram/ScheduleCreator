using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Helpers.Calendar
{
    public class CalendarHelper
    {
        public Collection<DateTime> CalendarDate()
        {
            Collection<DateTime> calendarDate = new Collection<DateTime>();


            DateTime currentDate = DateTime.Now.AddMonths(1);
            DateTime startMonth = currentDate.AddDays(-currentDate.Day + 1);

            for (int i = 0; i < DateTime.DaysInMonth(currentDate.Year, currentDate.Month); i++)
            {
                calendarDate.Add(startMonth.AddDays(i));
            }

            return calendarDate;
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
            DateTime findLastMonday = startMonth;

            // numberOfMondays starting with 1 because the first monday is not counted in loop.
            sbyte numberOfMondays = 1;
            sbyte daysToNewMonth = 0;

            // This looks for number of mondays except the first monday of the month
            for (int i = 1; i <= DateTime.DaysInMonth(currentDate.Year, currentDate.Month); i++)
            {
                if (startMonth.AddDays(i).DayOfWeek.ToString() == "Monday")
                    numberOfMondays++;
            }

            // Week 1
            while (findLastMonday.DayOfWeek.ToString() != "Monday") // this is final loop, no need to change anything
            {
                daysToNewMonth++;
                findLastMonday = findLastMonday.AddDays(-1);
            }
            weeks.Add(1, startMonth.AddDays(6 - daysToNewMonth).Day);

            // Rest of the weeks
            for (sbyte i = 2; i <= numberOfMondays; i++)
            {
                if (numberOfMondays != i)
                    weeks.Add(i, startMonth.AddDays(weeks.ElementAt(i-2).Value + 6).Day);

                if (numberOfMondays == i)
                {
                    int numberOfDaysAfterLastSunday = DateTime.DaysInMonth(currentDate.Year, currentDate.Month) - weeks.ElementAt(i - 2).Value;
                    weeks.Add(i, startMonth.AddDays(weeks.ElementAt(i - 2).Value + numberOfDaysAfterLastSunday - 1).Day);
                }

            }
            return weeks;
        }

        //--------------------------------------------------------------
        public byte DaysAfterMonday() // need to check if needs this.
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

        public List<DateTime> ColleagueDays(Week weeks = null) // need to check if needs this methood.
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

        public List<DateTime> PreferenceDays(Preferences preferences) // need to check if this is usefull.
        {
            List<DateTime> preferenceDays = new();

            foreach (Date day in preferences.Dates)
            {
                preferenceDays.Add(day.FreeDayChosen.Date);
            }

            return preferenceDays;
        }


        public Week CheckWeek(Employee tempEmployee, Employee currentEmployee, int weeksDict, string shift, int workingDays = 0) // need to check if needed
        {
            Week week = new();
            List<DateTime> colleagueDays = new();
            List<DateTime> preferenceDays = PreferenceDays(currentEmployee.Preferences);

            if ((tempEmployee.Weeks != null) && (tempEmployee.Weeks.Count > 0))
                colleagueDays = ColleagueDays(tempEmployee.Weeks.ElementAt(0));
            else
                colleagueDays = ColleagueDays();


            if (currentEmployee.Weeks.Count > 0)
            {
                week = GetWeek(preferenceDays, colleagueDays, weeksDict, shift, workingDays);
            }
            else
            {
                week = GetWeek(preferenceDays, colleagueDays, weeksDict, shift, workingDays);
            }

            return week;
        }

        public Week GetWeek(List<DateTime> preferenceDays, List<DateTime> colleagueDays, int weeksDict, string shift, int workingDays = 0) // need to check if needed
        {
            Week week = new();
            IList<Day> days = new List<Day>();
            DateTime currentDate = DateTime.Now.AddMonths(1);
            DateTime startMonth = currentDate.AddDays(-currentDate.Day + 1);
            DateTime dayToAdd;
            int d = 0;

            if ((weeksDict - 7) > 0)
                d = (weeksDict - 7);

            for (int day = d; day < weeksDict; day++)
            {
                if (workingDays < 5) // probably need to change it for 4 instead of 5
                {
                    dayToAdd = startMonth.AddDays(day).Date; // Hours has to be deleted from this. After that it will be working.

                    // Need to add condidtion that will not allow create shedule with (12.x) N and day after (13.x) D to be fixed in future. !!!
                    if (shift == "Night")
                    {
                        if ((preferenceDays[0] != dayToAdd) && (preferenceDays[1] != dayToAdd))
                        {
                            if ((colleagueDays[0] != dayToAdd) && (colleagueDays[1] != dayToAdd) && (colleagueDays[2] != dayToAdd) &&
                                (colleagueDays[3] != dayToAdd) && (colleagueDays[4] != dayToAdd)) // to be fixed while 2 or more people on the same shfit
                            {
                                days.Add(new Day()
                                {
                                    WorkingDay = dayToAdd,
                                    Shift = shift
                                });
                                workingDays++;
                            }
                        }
                    }
                    else
                    {
                        if ((preferenceDays[0] != dayToAdd) && (preferenceDays[1] != dayToAdd))
                        {
                            if ((colleagueDays[0] != dayToAdd) && (colleagueDays[1] != dayToAdd) && (colleagueDays[2] != dayToAdd) &&
                                (colleagueDays[3] != dayToAdd) && (colleagueDays[4] != dayToAdd)) // this has to be changed
                            {
                                days.Add(new Day()
                                {
                                    WorkingDay = dayToAdd,
                                    Shift = shift
                                });
                                workingDays++;
                            }
                        }
                    }
                }
            }

            week.Days = days;

            return week;
        }
    }
}
