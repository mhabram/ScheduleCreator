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
    public class PreferenceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        //private DateTime _freeWorkingDay = DateTime.Now.AddMonths(1);
        //public DateTime FreeWorkingDay
        //{
        //    get
        //    {
        //        return _freeWorkingDay;
        //    }
        //    set
        //    {
        //        _freeWorkingDay = value;
        //        OnPropertyChanged("StartDate");
        //    }
        //}
        //private List<DateTime> _test;
        //public List<DateTime> Test
        //{
        //    get
        //    {
        //        return new List<DateTime> { DateTime.Now.AddMonths(1), DateTime.Now.AddMonths(1), DateTime.Now.AddMonths(1) };
        //    }
        //    set
        //    {
        //        _test.Add({ value });
        //        OnPropertyChanged("StartDate");
        //    }
        //}

        //private DateTime _freeWorkingDay = DateTime.Now.AddMonths(1);
        //public DateTime FreeWorkingDay
        //{
        //    get
        //    {
        //        return _freeWorkingDay;
        //    }
        //    set
        //    {
        //        _freeWorkingDay = value;
        //        OnPropertyChanged("StartDate");
        //    }
        //}

        // = new BindingList<DateTime>() { DateTime.Now.AddMonths(1), DateTime.Now.AddMonths(1), DateTime.Now.AddMonths(1) }
        //public BindingList<DateTime> _freeWorkingDays;
        //public BindingList<DateTime> FreeWorkingDays
        //{
        //    get
        //    {
        //        return _freeWorkingDays;
        //    }
        //    set
        //    {
        //        _freeWorkingDays = value;
        //    }
        //}

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
                OnPropertyChanged(nameof(DayOff1));
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
                OnPropertyChanged(nameof(DayOff1));
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged(string name)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //        handler(this, new PropertyChangedEventArgs(name));
        //}

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

        public PreferenceViewModel(IPreferenceService preferenceService, IEmployeeService employeeService, IDateService dateService)
        {
            AddPreferenceCommand = new AddPreferenceCommand(this, preferenceService, employeeService, dateService);
        }
    }
}
