using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ApplicationLogic.Helpers;
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
    class GetCalendarEmployeeDetailsCommand : ICommand
    {
        private readonly ScheduleViewModel _viewModel;
        private readonly IEmployeeService _employeeService;

        public GetCalendarEmployeeDetailsCommand(ScheduleViewModel viewModel, IEmployeeService employeeService)
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
            ICollection<DateTime> calendar = ScheduleHelpers.CalendarDate();
            Collection<Employee> employees = await _employeeService.GetDetails();
            Collection<EmployeeDTO> tempEmployees;

            foreach (DateTime d in calendar)
            {
                tempEmployees = new();
                foreach (Employee e in employees)
                {
                    string fullName = String.Concat(e.Name, " ", e.LastName);
                    tempEmployees.Add(new EmployeeDTO { FullName = fullName, WorkingDays = 0, Date = d, IsWorking = false });
                }
                _viewModel.CalendarDates.Add(new CalendarDateDTO { Employees = tempEmployees, Date = d });
            }

            foreach (Employee e in employees)
            {
                string fullName = String.Concat(e.Name, " ", e.LastName);
                _viewModel.Employees.Add(new EmployeeDTO { FullName = fullName, WorkingDays = 0 });
            }
        }
    }
}
