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
        public EmployeeViewModel(IEmployeeService employeeService)
        {
            AddEmployeeCommand = new AddEmployeeCommand(this, employeeService);
            SuccessMessageViewModel = new MessageViewModel();
        }

        public string _name;
        public string Name
        {
            get
            { return _name; }
            set
            {
                _name = value;
            }
        }

        public string _lastName;
        public string LastName
        {
            get
            { return _lastName; }
            set
            {
                _lastName = value;
            }
        }

        public MessageViewModel SuccessMessageViewModel { get; }
        public string SuccessMessage
        {
            set => SuccessMessageViewModel.Message = value;
        }

        public ICommand AddEmployeeCommand { get; set; }
    }
}
