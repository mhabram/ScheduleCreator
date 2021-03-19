using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
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
            IDictionary<int, int> weeksDict = Weeks(startMonth, currentDate); // splitting month into a 5 weeks. Feature fix - make a 4 weeks for 28 day Feb.

            // ------------ Might be needed for shuffle employees while creating schedule.
            Random rand = new();
            List<int> listNumbers = new();
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
                            Employee emp = _viewModel.Employees.ElementAt(val - 1);
                            byte temp = 0; // day adding to the DateTime
                            byte workingDays = 0;
                            if (emp.Weeks.Count > 0)
                            {
                                workingDays = (byte)emp.Weeks.ElementAt(0).Days.Count;
                            }

                            if (nightShift < 1)
                            {
                                if (emp.Weeks.Count >= 1) // second condition checks if employee already had working.
                                {
                                    Week week = new();
                                    week = GetWeek(currentDate, startMonth, weeksDict.ElementAt(0).Value, workingDays);
                                    await _employeeService.SetWeek(emp, week, i);
                                    nightShift++;
                                    temp++;
                                }
                                else if (emp.Weeks.Count < 1)
                                {
                                    Week week = new();
                                    week = GetWeek(currentDate, startMonth, weeksDict.ElementAt(0).Value, workingDays);
                                    await _employeeService.SetWeek(emp, week, i);
                                    nightShift++;
                                    temp++;
                                }
                            }

                            //if (swingShift < 2)
                            //{
                                //if ((emp.Weeks.ElementAt(0).Days.Last().Shift == 'D') &&
                                //    (emp.Weeks.ElementAt(0).Days.Last().WorkingDay.AddDays(1) < startMonth.AddDays(temp))) // This is not needed while adding to N shift.
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

        private Week GetWeek(DateTime currentDate, DateTime startMonth, int weeksDict, byte workingDays = 0)
        {
            Week week = new Week();
            ICollection<Day> days = new Collection<Day>();

            if (workingDays <= 5)
            {
                for (int day = 0; day < DateTime.DaysInMonth(currentDate.Year, currentDate.Month); day++)
                {
                    if (weeksDict > day)
                    {
                        days.Add(new Day() { WorkingDay = startMonth.AddDays(day), Shift = 'N' });
                        workingDays++; // probably to delete week.Days.Count does the same job
                    }
                }
            }

            week.WorkingDays = workingDays;
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
            for (int i = 2; i <= 4; i++)
            {
                int x = weeks.ElementAt(i - 2).Value + 6;
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
