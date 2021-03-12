using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class RemoveEmployeesCommand : ICommand
    {
        private readonly CreateScheduleViewModel _viewModel;

        public RemoveEmployeesCommand(CreateScheduleViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_viewModel.SelectedEmployee != null)
                _viewModel.Employees.Remove(_viewModel.SelectedEmployee);
        }
    }
}
