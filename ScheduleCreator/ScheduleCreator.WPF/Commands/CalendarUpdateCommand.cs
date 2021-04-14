using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class CalendarUpdateCommand : ICommand
    {
        private readonly ScheduleViewModel _viewModel;
        private readonly IScheduleService _scheduleService;

        public CalendarUpdateCommand(ScheduleViewModel viewModel, IScheduleService scheduleService)
        {
            _viewModel = viewModel;
            _scheduleService = scheduleService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            bool isSaved = await _scheduleService.CreateSchedule(_viewModel.CalendarDates);
            if (isSaved)
                MessageBox.Show($"Schedule has been saved to the database.");
            else
                MessageBox.Show($"Schedule has not been saved to the database.");
        }
    }
}
