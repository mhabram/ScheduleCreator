using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.WPF.ApplicationLogic.Helpers
{
    public class ShiftCount
    {
        public Week CheckWeek(Employee tempEmployee, Employee currentEmployee, int weeksDict, char shiftChar, int workingDays = 0)
        {
            Week week = new();
            DayCount dayCount = new();
            List<DateTime> colleagueDays = new();
            List<DateTime> preferenceDays = dayCount.PreferenceDays(currentEmployee.Preferences);
            byte agentNumber = 0;

            if ((tempEmployee.Weeks != null) && (tempEmployee.Weeks.Count > 0))
                colleagueDays = dayCount.ColleagueDays(tempEmployee.Weeks.ElementAt(0));
            else
                colleagueDays = dayCount.ColleagueDays();


            if (currentEmployee.Weeks.Count > 0)
            {
                week = GetWeek(preferenceDays, colleagueDays, weeksDict, shiftChar, workingDays);
            }
            else
            {
                week = GetWeek(preferenceDays, colleagueDays, weeksDict, shiftChar, workingDays);
            }

            return week;
        }

        public Week GetWeek(List<DateTime> preferenceDays, List<DateTime> colleagueDays, int weeksDict, char shiftChar, int workingDays = 0)
        {
            Week week = new();
            ICollection<Day> days = new Collection<Day>();
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
                    if (shiftChar == 'N')
                    {
                        if ((preferenceDays[0] != dayToAdd) && (preferenceDays[1] != dayToAdd))
                        {
                            if ((colleagueDays[0] != dayToAdd) && (colleagueDays[1] != dayToAdd) && (colleagueDays[2] != dayToAdd) &&
                                (colleagueDays[3] != dayToAdd) && (colleagueDays[4] != dayToAdd)) // to be fixed while 2 or more people on the same shfit
                            {
                                days.Add(new Day()
                                {
                                    WorkingDay = dayToAdd,
                                    Shift = shiftChar
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
                                    Shift = shiftChar
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

        //public Week GetWeek(Employee employee, Employee currentEmployee, DateTime currentDate, DateTime startMonth, int weeksDict, int shift, char shiftChar, int workingDays = 0)
        //{
        //    DayCount dayCount = new();
        //    Week week = new();
        //    ICollection<Day> days = new Collection<Day>();
        //    List<DateTime> colleagueDays = new();
        //    List<DateTime> preferenceDays = dayCount.PreferenceDays(currentEmployee.Preferences);
        //    DateTime dayToAdd;

        //    if (employee.Weeks != null)
        //        colleagueDays = dayCount.ColleagueDays(employee.Weeks.ElementAt(0));
        //    else
        //        colleagueDays = dayCount.ColleagueDays();

        //    if (workingDays < 5)
        //    {
        //        for (int day = 0; day < DateTime.DaysInMonth(currentDate.Year, currentDate.Month); day++)
        //        {
        //            dayToAdd = startMonth.AddDays(day).Date; // Hours has to be deleted from this. After that it will be working.

        //            // Need to add condidtion that will not allow create shedule with (12.x) N and day after (13.x) D to be fixed in future.
        //            if (shiftChar == 'N')
        //            {
        //                if (weeksDict > day) // && (shift < 7)
        //                {
        //                    if ((preferenceDays[0] != dayToAdd) && (preferenceDays[1] != dayToAdd))
        //                    {
        //                        if ((colleagueDays[0] != dayToAdd) && (colleagueDays[1] != dayToAdd) && (colleagueDays[2] != dayToAdd) &&
        //                            (colleagueDays[3] != dayToAdd) && (colleagueDays[4] != dayToAdd))
        //                        {
        //                            days.Add(new Day()
        //                            {
        //                                WorkingDay = dayToAdd,
        //                                Shift = shiftChar
        //                            });
        //                            workingDays++;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (weeksDict > day) // && (shift < 14)
        //                {
        //                    if ((preferenceDays[0] != dayToAdd) && (preferenceDays[1] != dayToAdd))
        //                    {
        //                        if ((colleagueDays[0] != dayToAdd) && (colleagueDays[1] != dayToAdd) && (colleagueDays[2] != dayToAdd) &&
        //                            (colleagueDays[3] != dayToAdd) && (colleagueDays[4] != dayToAdd))
        //                        {
        //                            days.Add(new Day()
        //                            {
        //                                WorkingDay = dayToAdd,
        //                                Shift = shiftChar
        //                            });
        //                            workingDays++;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    week.WorkingDays = (byte)workingDays; // probably to delete from the database week.Days.Count does the same job
        //    week.Days = days;

        //    return week;
        //}
    }
}
