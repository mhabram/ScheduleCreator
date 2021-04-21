using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class PreferenceViewModel : ViewModelBase
    {
        public PreferenceViewModel(IPreferenceService preferenceService,
            IEmployeeService employeeService,
            IPreferenceDayService dateService)
        {
            AddPreferenceCommand = new AddPreferenceCommand(this, preferenceService, employeeService, dateService);
        }

        private DateTime _dayOff1 = DateTime.Now.AddMonths(1);
        public DateTime DayOff1
        {
            get
            {
                return _dayOff1;
            }
            set
            {
                _dayOff1 = value;
                OnPropertyChanged(nameof(DayOff1));
            }
        }

        private DateTime _dayOff2 = DateTime.Now.AddMonths(1);
        public DateTime DayOff2
        {
            get
            {
                return _dayOff2;
            }
            set
            {
                _dayOff2 = value;
                OnPropertyChanged(nameof(DayOff2));
            }
        }

        private DateTime _dayOff3 = DateTime.Now.AddMonths(1);
        public DateTime DayOff3
        {
            get
            {
                return _dayOff3;
            }
            set
            {
                _dayOff3 = value;
                OnPropertyChanged(nameof(DayOff3));
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
                OnPropertyChanged(nameof(LastName));
            }
        }

        public sbyte _holidays;
        public sbyte Holidays
        {
            get
            {
                return _holidays;
            }
            set
            {
                _holidays = value;
                OnPropertyChanged(nameof(Holidays));
            }
        }

        public ICommand AddPreferenceCommand { get; set; }
    }
}
