using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {
        public string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand AddEmployeeCommand { get; set; }

        public EmployeeViewModel(IEmployeeService employeeService)
        {
            AddEmployeeCommand = new AddEmployeeCommand(this, employeeService);
        }
    }
}
