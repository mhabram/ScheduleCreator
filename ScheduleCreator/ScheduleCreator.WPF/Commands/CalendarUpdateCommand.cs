using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class CalendarUpdateCommand : ICommand
    {
        private readonly ScheduleViewModel _viewModel;

        public CalendarUpdateCommand(ScheduleViewModel viewModel)
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
            System.Windows.MessageBox.Show(_viewModel.SelectedCalendarDate.Count.ToString());
            System.Windows.MessageBox.Show(_viewModel.SelectedEmployee.ToString());
            System.Windows.MessageBox.Show(_viewModel.SelectedDate.ToString());
        }
    }
}
