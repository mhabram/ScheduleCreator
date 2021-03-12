using ScheduleCreator.Domain.Models;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
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
            int test = _viewModel.Employees.Count();
            System.Windows.MessageBox.Show($"Employees: {test}");
        }
    }
}
