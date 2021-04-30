using ScheduleCreator.Domain.DTO.Observable;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class EmployeeDTO : ObservableObject
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        private bool _day;
        public bool Day
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged(nameof(Day));
            }
        }
        private bool _swing;
        public bool Swing
        {
            get { return _swing; }
            set
            {
                _swing = value;
                OnPropertyChanged(nameof(Swing));
            }
        }
        private bool _night;
        public bool Night
        {
            get { return _night; }
            set
            {
                _night = value;
                OnPropertyChanged(nameof(Night));
            }
        }
        private string _shift;
        public string Shift
        {
            get { return _shift; }
            set
            {
                _shift = value;
                OnPropertyChanged(nameof(Shift));
            }
        }
        public int CalendarDateDTOId{ get; set; }

        public void UpdateEmployeeView(ObservableCollection<EmployeeViewDTO> employeeViewDTO)
        {
            bool isWorking = false;

            if (Day || Swing || Night)
                isWorking = true;

            for (int i = 0; i < employeeViewDTO.Count; i++)
            {
                if ((FullName == employeeViewDTO[i].FullName) && isWorking)
                {
                    employeeViewDTO[i].WorkingDays--;
                    break;
                }

                if ((FullName == employeeViewDTO[i].FullName) && !isWorking)
                {
                    employeeViewDTO[i].WorkingDays++;
                    break;
                }
            }
        }

        public bool IsPreferenceDay(ObservableCollection<CalendarDateDTO> calendarDates, IList<Preferences> preferences)
        {
            string lastName = FullName.Split()[1];
            Preferences pref = preferences.Where(e => e.Employee.LastName == lastName).FirstOrDefault();
            return pref.PreferenceDays.Any(d => d.FreeDayChosen.Day == calendarDates.ElementAt(CalendarDateDTOId).Date.Day);
        }

        public int GetWorkingDays(ObservableCollection<EmployeeViewDTO> employeeViewDTO)
        {
            int workingDays = 0;

            for (int i = 0; i < employeeViewDTO.Count; i++)
            {
                workingDays = employeeViewDTO[i].GetWorkingDays(FullName);
                if (workingDays > 0)
                    break;
            }

            return workingDays;
        }
    }
}
