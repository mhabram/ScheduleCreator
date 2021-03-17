using ScheduleCreator.Domain.Models;
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

        public CreateScheduleCommand(CreateScheduleViewModel viewModel)
        {
            _viewModel = viewModel;
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
            IDictionary<int, int> weeksDict = Weeks(startMonth, currentDate);


            // ------------ Might be needed for shuffle employees while creating schedule.
            Random rand = new Random();
            List<int> listNumbers = new List<int>();
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

                for (int i = 1; i <= 5; i++)
                {
                    //-------------
                    int dayShift = 0;
                    int swingShift = 0;
                    int nightShift = 0;
                    //-------------

                    if (i == 1)
                    {
                        foreach (int val in listNumbers)
                        {
                            Employee emp = _viewModel.Employees.ElementAt(val - 1);
                            System.Windows.MessageBox.Show($"Free working days: {emp.Preferences.InternalPreferenceId}");

                            if (emp.Weeks != null)
                            {
                                foreach (Week w in emp.Weeks)
                                {
                                    System.Windows.MessageBox.Show($"Free working days: {w.Shift}");
                                }
                            }

                            // Implement creating schedule. TBD

                            if ((nightShift < 1) && (emp.Weeks.ElementAt(0).Shift == 'D'))
                            {
                                Week week = new Week(); // temporary, after repository creation can be removed and save immediatley to the database.
                                week = GetWeek(currentDate, startMonth, weeksDict.ElementAt(0).Value);
                                // week ready to save to database 
                                nightShift++;
                            }











                            //if (swingShift < 2)
                            //{
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
                int asd = _viewModel.Employees.Count();
                System.Windows.MessageBox.Show($"Employees: {asd}");
            }
        }

        private Week GetWeek(DateTime currentDate, DateTime startMonth, int weeksDict)
        {
            Week week = new Week();
            ICollection<Day> days = new Collection<Day>();
            byte workingDays = 0;

            for (int day = 0; day < DateTime.DaysInMonth(currentDate.Year, currentDate.Month); day++)
            {
                if ((weeksDict > day))
                {
                    days.Add(new Day() { WorkingDay = startMonth.AddDays(day) });
                    //days.Add(startMonth.AddDays(day));
                    workingDays++;
                }
            }

            week.WorkingDays = workingDays;
            week.Days = days;
            week.Shift = 'N';

            return week;
        }

        private IDictionary<int, int> Weeks(DateTime startMonth, DateTime currentDate)
        {
            IDictionary<int, int> weeks = new Dictionary<int, int>();
            DateTime checkWeek = startMonth;
            sbyte daysAfterMonday = 0;

            while (startMonth.DayOfWeek.ToString() != "Monday")
            {
                daysAfterMonday++;
                startMonth = startMonth.AddDays(-1);
            }

            weeks.Add(1, checkWeek.AddDays(6 - daysAfterMonday).Day);
            for (int i = 2; i <= 4; i++)
            {
                int x = weeks.ElementAt(i - 2).Value + 6;
                weeks.Add(i, checkWeek.AddDays(x).Day);
            }

            while (checkWeek.Day != DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
            {
                checkWeek = checkWeek.AddDays(1);
            }
            weeks.Add(5, checkWeek.Day);

            return weeks;
        }
    }
}
