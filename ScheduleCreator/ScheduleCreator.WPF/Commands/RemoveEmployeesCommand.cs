using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class RemoveEmployeesCommand : AsyncCommandBase
    {
        private readonly CreateScheduleViewModel _viewModel;

        public RemoveEmployeesCommand(CreateScheduleViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedEmployee != null)
                _viewModel.Employees.Remove(_viewModel.SelectedEmployee);
        }
    }
}
