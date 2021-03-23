using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ApplicationLogic.Helpers;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class CreateScheduleCommand : ICommand
    {
        private readonly CreateScheduleViewModel _viewModel;
        private readonly IEmployeeService _employeeService;

        public CreateScheduleCommand(CreateScheduleViewModel viewModel, IEmployeeService employeeService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public async void Execute(object parameter)
        {
            Employee tempEmployee = new Employee();
            ShiftCount shiftCount = new();
            DayCount dayCount = new();
            Random rand = new();
            List<int> listNumbers = new();

            IDictionary<int, int> weeksDict = dayCount.Weeks(); // splitting month into a 5 weeks. Feature fix - make a 4 weeks for 28 day Feb.

            tempEmployee.Weeks = new List<Week>();

            // ------------ Might be needed for shuffle employees while creating schedule.
            int number;

            if (_viewModel.Employees != null)
            {
                for (int i = 0; i < _viewModel.Employees.Count(); i++)
                {
                    do
                    {
                        number = rand.Next(1, _viewModel.Employees.Count + 1);
                    } while (listNumbers.Contains(number));
                    listNumbers.Add(number);
                }
                // -------------

                for (byte i = 1; i <= 5; i++)
                {
                    int dayShift = 0;
                    int swingShift = 0;
                    int nightShift = 0;

                    if (i == 1) // ----- First week
                    {
                        foreach (int val in listNumbers) // looping shuffled employees
                        {
                            Employee employee = _viewModel.Employees.ElementAt(val - 1);
                            Week week = new();
                            Week tempWeek = new();
                            int workingDays = 0;

                            if (employee.Weeks.Count > 0)
                            {
                                workingDays = employee.Weeks.ElementAt(0).Days.Count;
                            }

                            if (nightShift < (6 - dayCount.DaysAfterMonday()))
                            {
                                tempWeek = shiftCount.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, 'N', workingDays);
                                if (tempWeek.Days.Count > 0)
                                {
                                    week = tempWeek;
                                    tempEmployee.Weeks.Add(week);
                                    nightShift += week.Days.Count;
                                }
                            }
                            else
                                tempEmployee.Weeks = new List<Week>();

                            if (swingShift < (14 - (dayCount.DaysAfterMonday() * 2))) //&& // Condition multiply depends on the users working on that shift (dayCount.DaysAfterMonday(startMonth) * 2)
                                //(tempEmployee.Weeks.ElementAt(0).Days.Count < (5 - workingDays))) // this contitions checking if employee has below 5 working days if yes, program will add to him other shift
                            {
                                tempWeek = shiftCount.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, 'S', workingDays);
                                if (tempWeek.Days.Count > 0)
                                {
                                    if (week.Days != null)
                                    {
                                        foreach (Day d in tempWeek.Days)
                                        {
                                            week.Days.Add(d);
                                        }
                                    }
                                    else
                                        week = tempWeek;

                                    tempEmployee.Weeks.Add(week);
                                    swingShift += week.Days.Count;
                                }
                            }
                            else
                                tempEmployee.Weeks = new List<Week>();

                            if (dayShift < ((14 - dayCount.DaysAfterMonday() * 2)))
                            {
                                tempWeek = shiftCount.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, 'S', workingDays);
                                if (tempWeek.Days.Count > 0)
                                {
                                    if (week.Days != null)
                                    {
                                        foreach (Day d in tempWeek.Days)
                                        {
                                            week.Days.Add(d);
                                        }
                                    }
                                    else
                                        week = tempWeek;

                                    tempEmployee.Weeks.Add(week);
                                    dayShift += week.Days.Count;
                                }
                            }
                            else
                                tempEmployee.Weeks = new List<Week>();

                            if (week.Days.Count > 0)
                            {
                                tempEmployee = await _employeeService.SetWeek(employee, week, i);
                            }
                            // feature below probably needs to be done.
                            // it's creating 2 weeks id in the same week while it should be just one. 
                        }
                    }

                    if ((i > 1) && (i < 5)) // counting from the start every time. need to fix it
                    {
                        tempEmployee.Weeks = new List<Week>();

                        foreach (int val in listNumbers) // looping shuffled employees
                        {
                            Employee employee = _viewModel.Employees.ElementAt(val - 1);
                            Week week = new();
                            Week tempWeek = new();
                            int workingDays = 0;

                            if (employee.Weeks.Count > 0)
                            {
                                workingDays = employee.Weeks.ElementAt(0).Days.Count;
                            }

                            if (nightShift < 6)
                            {
                                tempWeek = shiftCount.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, 'N', workingDays);
                                if (tempWeek.Days.Count > 0)
                                {
                                    week = tempWeek;
                                    tempEmployee.Weeks.Add(week);
                                    nightShift += week.Days.Count;
                                }
                            }
                            else
                                tempEmployee.Weeks = new List<Week>();

                            if (swingShift < 14)
                            {
                                tempWeek = shiftCount.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, 'S', workingDays);
                                if (tempWeek.Days.Count > 0)
                                {
                                    if (week.Days != null)
                                    {
                                        foreach (Day d in tempWeek.Days)
                                        {
                                            week.Days.Add(d);
                                        }
                                    }
                                    else
                                        week = tempWeek;

                                    tempEmployee.Weeks.Add(week);
                                    swingShift += week.Days.Count;
                                }
                            }
                            else
                                tempEmployee.Weeks = new List<Week>();

                            if (dayShift < 14)
                            {
                                tempWeek = shiftCount.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, 'S', workingDays);
                                if (tempWeek.Days.Count > 0)
                                {
                                    if (week.Days != null)
                                    {
                                        foreach (Day d in tempWeek.Days)
                                        {
                                            week.Days.Add(d);
                                        }
                                    }
                                    else
                                        week = tempWeek;

                                    tempEmployee.Weeks.Add(week);
                                    dayShift += week.Days.Count;
                                }
                            }
                            else
                                tempEmployee.Weeks = new List<Week>();

                            if (week.Days.Count > 0)
                            {
                                tempEmployee = await _employeeService.SetWeek(employee, week, i);
                            }
                        }
                    }

                    //if (i == 5)
                    //{
                    //    tempEmployee = null;

                    //    foreach (int val in listNumbers) // looping shuffled employees
                    //    {
                    //        Employee employee = _viewModel.Employees.ElementAt(val - 1);
                    //        int workingDays = 0;
                    //        if (employee.Weeks.Count > 0)
                    //        {
                    //            workingDays = employee.Weeks.ElementAt(0).Days.Count;
                    //        }

                    //        if (nightShift < 6) // to be fixed, need to cunt how many days left to the end of month
                    //        {
                    //            Week week = shiftCount.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, 'N', workingDays);

                    //            if (week.Days.Count > 0)
                    //            {
                    //                tempEmployee = await _employeeService.SetWeek(employee, week, i);
                    //                nightShift += tempEmployee.Weeks.ElementAt(0).Days.Count;
                    //            }
                    //        }
                    //        else
                    //            tempEmployee = null;

                    //        if (swingShift < 14)
                    //        {
                    //            Week week = shiftCount.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, 'S', workingDays);

                    //            if (week.Days.Count > 0)
                    //            {
                    //                tempEmployee = await _employeeService.SetWeek(employee, week, i);
                    //                swingShift += tempEmployee.Weeks.ElementAt(0).Days.Count;
                    //            }
                    //        }
                    //        else
                    //            tempEmployee = null;

                    //        if (dayShift < 14)
                    //        {
                    //            Week week = shiftCount.CheckWeek(tempEmployee, employee, weeksDict.ElementAt(i - 1).Value, 'D', workingDays);

                    //            if (week.Days.Count > 0)
                    //            {
                    //                tempEmployee = await _employeeService.SetWeek(employee, week, i);
                    //                dayShift += tempEmployee.Weeks.ElementAt(0).Days.Count;
                    //            }
                    //        }
                    //        else
                    //            tempEmployee = null;
                    //    }
                    //}
                }
            }
            else
            {
                System.Windows.MessageBox.Show("There are no employees yet.");
            }
        }
    }
}
