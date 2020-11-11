using ScheduleCreator.WPF.State.Navigators;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static ScheduleCreator.WPF.State.Navigators.INavigator;

namespace ScheduleCreator.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch(viewType)
                {
                    case ViewType.Conditions:
                        _navigator.CurrentViewModel = new ConditionsViewModel();
                        break;
                    case ViewType.CreateSchedule:
                        _navigator.CurrentViewModel = new CreateScheduleViewModel();
                        break;
                    case ViewType.Employee:
                        _navigator.CurrentViewModel = new EmployeeViewModel();
                        break;
                    case ViewType.Help:
                        _navigator.CurrentViewModel = new HelpViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
