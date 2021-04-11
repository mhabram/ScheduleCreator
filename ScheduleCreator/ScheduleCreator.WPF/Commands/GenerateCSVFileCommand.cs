using ScheduleCreator.Domain.GenerateToExcel;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class GenerateCSVFileCommand : ICommand
    {
        private readonly ScheduleViewModel _viewModel;
        private readonly GenerateToCSV _generateToCSV = new GenerateToCSV();

        public GenerateCSVFileCommand(ScheduleViewModel viewModel)
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
            _generateToCSV.ToCSV();
        }
    }
}
