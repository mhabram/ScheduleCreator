using ScheduleCreator.Domain.Helpers.Calendar;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class CreateScheduleCommand : AsyncCommandBase
    {
        private readonly CreateScheduleViewModel _viewModel;
        private readonly IEmployeeService _employeeService;

        private readonly CalendarHelper _calendarHelper = new();

        public CreateScheduleCommand(CreateScheduleViewModel viewModel, IEmployeeService employeeService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            //Employee tempEmployee = new Employee();
            //Random rand = new();
            //List<int> listNumbers = new();

            //IDictionary<int, int> weeksDict = _calendarHelper.Weeks();

            //tempEmployee.Weeks = new List<Week>();

            //// ------------ Might be needed for shuffle employees while creating schedule.
            //int number;

            //if (_viewModel.Employees != null)
            //{
            //    for (int i = 0; i < _viewModel.Employees.Count(); i++)
            //    {
            //        do
            //        {
            //            number = rand.Next(1, _viewModel.Employees.Count + 1);
            //        } while (listNumbers.Contains(number));
            //        listNumbers.Add(number);
            //    }
            //    // -------------

            //    for (byte i = 1; i <= 5; i++)
            //    {
            //        int dayShift = 0;
            //        int swingShift = 0;
            //        int nightShift = 0;

            //        if (i == 1) // ----- First week
            //        {
            //            foreach (int val in listNumbers) // looping shuffled employees
            //            {
            //                Employee employee = _viewModel.Employees.ElementAt(val - 1);
            //                Week week = new();
            //                Week tempWeek = new();
            //                int workingDays = 0;

            //                if (employee.Weeks.Count > 0)
            //                {
            //                    workingDays = employee.Weeks.ElementAt(0).Days.Count;
            //                }

            //                if (nightShift < (6 - _calendarHelper.DaysAfterMonday()))
            //                {
            //                    tempWeek = _calendarHelper.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, "Night", workingDays);
            //                    if (tempWeek.Days.Count > 0)
            //                    {
            //                        week = tempWeek;
            //                        tempEmployee.Weeks.Add(week);
            //                        nightShift += week.Days.Count;
            //                    }
            //                }
            //                else
            //                    tempEmployee.Weeks = new List<Week>();

            //                if (swingShift < (14 - (_calendarHelper.DaysAfterMonday() * 2))) //&& // Condition multiply depends on the users working on that shift (dayCount.DaysAfterMonday(startMonth) * 2)
            //                    //(tempEmployee.Weeks.ElementAt(0).Days.Count < (5 - workingDays))) // this contitions checking if employee has below 5 working days if yes, program will add to him other shift
            //                {
            //                    tempWeek = _calendarHelper.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, "Swing", workingDays);
            //                    if (tempWeek.Days.Count > 0)
            //                    {
            //                        if (week.Days != null)
            //                        {
            //                            foreach (Day d in tempWeek.Days)
            //                            {
            //                                week.Days.Add(d);
            //                            }
            //                        }
            //                        else
            //                            week = tempWeek;

            //                        tempEmployee.Weeks.Add(week);
            //                        swingShift += week.Days.Count;
            //                    }
            //                }
            //                else
            //                    tempEmployee.Weeks = new List<Week>();

            //                if (dayShift < ((14 - _calendarHelper.DaysAfterMonday() * 2)))
            //                {
            //                    tempWeek = _calendarHelper.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, "Swing", workingDays);
            //                    if (tempWeek.Days.Count > 0)
            //                    {
            //                        if (week.Days != null)
            //                        {
            //                            foreach (Day d in tempWeek.Days)
            //                            {
            //                                week.Days.Add(d);
            //                            }
            //                        }
            //                        else
            //                            week = tempWeek;

            //                        tempEmployee.Weeks.Add(week);
            //                        dayShift += week.Days.Count;
            //                    }
            //                }
            //                else
            //                    tempEmployee.Weeks = new List<Week>();

            //                if (week.Days.Count > 0)
            //                {
            //                    tempEmployee = await _employeeService.SetWeek(employee, week, i);
            //                }
            //                // feature below probably needs to be done.
            //                // it's creating 2 weeks id in the same week while it should be just one. 
            //            }
            //        }

            //        if ((i > 1) && (i < 5)) // counting from the start every time. need to fix it
            //        {
            //            tempEmployee.Weeks = new List<Week>();

            //            foreach (int val in listNumbers) // looping shuffled employees
            //            {
            //                Employee employee = _viewModel.Employees.ElementAt(val - 1);
            //                Week week = new();
            //                Week tempWeek = new();
            //                int workingDays = 0;

            //                if (employee.Weeks.Count > 0)
            //                {
            //                    workingDays = employee.Weeks.ElementAt(0).Days.Count;
            //                }

            //                if (nightShift < 6)
            //                {
            //                    tempWeek = _calendarHelper.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, "Night", workingDays);
            //                    if (tempWeek.Days.Count > 0)
            //                    {
            //                        week = tempWeek;
            //                        tempEmployee.Weeks.Add(week);
            //                        nightShift += week.Days.Count;
            //                    }
            //                }
            //                else
            //                    tempEmployee.Weeks = new List<Week>();

            //                if (swingShift < 14)
            //                {
            //                    tempWeek = _calendarHelper.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, "Swing", workingDays);
            //                    if (tempWeek.Days.Count > 0)
            //                    {
            //                        if (week.Days != null)
            //                        {
            //                            foreach (Day d in tempWeek.Days)
            //                            {
            //                                week.Days.Add(d);
            //                            }
            //                        }
            //                        else
            //                            week = tempWeek;

            //                        tempEmployee.Weeks.Add(week);
            //                        swingShift += week.Days.Count;
            //                    }
            //                }
            //                else
            //                    tempEmployee.Weeks = new List<Week>();

            //                if (dayShift < 14)
            //                {
            //                    tempWeek = _calendarHelper.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, "Swing", workingDays);
            //                    if (tempWeek.Days.Count > 0)
            //                    {
            //                        if (week.Days != null)
            //                        {
            //                            foreach (Day d in tempWeek.Days)
            //                            {
            //                                week.Days.Add(d);
            //                            }
            //                        }
            //                        else
            //                            week = tempWeek;

            //                        tempEmployee.Weeks.Add(week);
            //                        dayShift += week.Days.Count;
            //                    }
            //                }
            //                else
            //                    tempEmployee.Weeks = new List<Week>();

            //                if (week.Days.Count > 0)
            //                {
            //                    tempEmployee = await _employeeService.SetWeek(employee, week, i);
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    System.Windows.MessageBox.Show("There are no employees yet.");
            //}
        }
    }
}
