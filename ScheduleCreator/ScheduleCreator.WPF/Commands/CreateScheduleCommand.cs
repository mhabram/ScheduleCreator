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

            DateTime currentDate = DateTime.Now.AddMonths(1);
            DateTime startMonth = currentDate.AddDays(-currentDate.Day + 1);

            Employee tempEmployee = new();
            Random rand = new();
            List<int> listNumbers = new();

            IDictionary<int, int> weeksDict = Weeks(startMonth, currentDate); // splitting month into a 5 weeks. Feature fix - make a 4 weeks for 28 day Feb.

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
                    //-------------
                    int dayShift = 0;
                    int swingShift = 0;
                    int nightShift = 0;
                    //-------------

                    if (i == 1) // ----- First week
                    {
                        foreach (int val in listNumbers) // looping shuffled employees
                        {
                            Employee employee = _viewModel.Employees.ElementAt(val - 1);
                            int workingDays = 0;
                            if (employee.Weeks.Count > 0)
                            {
                                workingDays = employee.Weeks.ElementAt(0).Days.Count;
                            }
                            

                            if (nightShift < (7 - DaysAfterMonday(startMonth)))
                            {
                                if (employee.Weeks.Count > 0) // second condition checks if employee already had working.
                                {
                                    Week week = GetWeek(tempEmployee, employee, currentDate, startMonth, weeksDict.ElementAt(0).Value, nightShift, 'N', workingDays);
                                    tempEmployee = await _employeeService.SetWeek(employee, week, i);
                                    nightShift += week.Days.Count;
                                }
                                else if (employee.Weeks.Count < 1)
                                {
                                    Week week = GetWeek(tempEmployee, employee, currentDate, startMonth, weeksDict.ElementAt(0).Value, nightShift, 'N', workingDays);
                                    tempEmployee = await _employeeService.SetWeek(employee, week, i);
                                    nightShift += week.Days.Count;
                                }
                            }

                            if (swingShift < (7 - DaysAfterMonday(startMonth)))
                            {
                                if (employee.Weeks.Count > 0)
                                {
                                    Week week = GetWeek(tempEmployee, employee, currentDate, startMonth, weeksDict.ElementAt(0).Value, swingShift, 'S', workingDays);
                                    tempEmployee = await _employeeService.SetWeek(employee, week, i);
                                    swingShift += week.Days.Count;
                                }
                                else
                                {
                                    Week week = GetWeek(tempEmployee, employee, currentDate, startMonth, weeksDict.ElementAt(0).Value, swingShift, 'S', workingDays);
                                    tempEmployee = await _employeeService.SetWeek(employee, week, i);
                                    swingShift += week.Days.Count;
                                }
                            }

                            //if (swingShift < 2)
                            //{
    //if ((emp.Weeks.ElementAt(0).Days.Last().Shift == 'D') && (emp.Weeks.ElementAt(0).Days.Last().WorkingDay.AddDays(1) < startMonth.AddDays(temp))) // This is not needed while adding to N shift.
                                //{
                                //}
                            //    Console.WriteLine("Swing");
                            //    swingShift++;
                            //}

                            //if (dayShift < 2)
                            //{
                            //    Console.WriteLine("Day");
                            //    dayShift++;
                            //}
                        }
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Employees");
            }
        }

        private byte DaysAfterMonday(DateTime startMonth)
        {
            byte daysAfterMonday = 0;

            while (startMonth.DayOfWeek.ToString() != "Monday")
            {
                daysAfterMonday++;
                startMonth = startMonth.AddDays(-1);
            }
            
            return daysAfterMonday;
        }

        private Week GetWeek(Employee employee, Employee currentEmployee, DateTime currentDate, DateTime startMonth, int weeksDict, int shift, char shiftChar, int workingDays = 0)
        {
            DayCount dayCount = new();
            Week week = new();
            ICollection<Day> days = new Collection<Day>();
            List<DateTime> colleagueDays = new();
            List<DateTime> preferenceDays = dayCount.PreferenceDays(currentEmployee.Preferences);
            DateTime dayToAdd;

            if (employee.Weeks != null)
                colleagueDays = dayCount.ColleagueDays(employee.Weeks.ElementAt(0));
            else
                colleagueDays = dayCount.ColleagueDays();

            if (workingDays < 5)
            {
                for (int day = 0; day < DateTime.DaysInMonth(currentDate.Year, currentDate.Month); day++)
                {
                    dayToAdd = startMonth.AddDays(day).Date; // Hours has to be deleted from this. After that it will be working.

                    if ((weeksDict > day) && (shift < 7))
                    {
                        if ((preferenceDays[0] != dayToAdd) && (preferenceDays[1] != dayToAdd))
                        {
                            if ((colleagueDays[0] != dayToAdd) && (colleagueDays[1] != dayToAdd) && (colleagueDays[2] != dayToAdd) &&
                                (colleagueDays[3] != dayToAdd) && (colleagueDays[4] != dayToAdd))
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

            week.WorkingDays = (byte)workingDays; // probably to delete from the database week.Days.Count does the same job
            week.Days = days;

            return week;
        }

        private IDictionary<int, int> Weeks(DateTime startMonth, DateTime currentDate)
        {
            IDictionary<int, int> weeks = new Dictionary<int, int>();
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
    }
}
