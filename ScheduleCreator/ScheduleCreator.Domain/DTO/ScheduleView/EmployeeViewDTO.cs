using ScheduleCreator.Domain.DTO.Observable;
using ScheduleCreator.Domain.Helpers.Calendar;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class EmployeeViewDTO : ObservableObject
    {
        public string FullName { get; set; }
        public IList<DateTime> PreferenceDays { get; set; }
        private int _workingDays;
        public int WorkingDays
        {
            get { return _workingDays; }
            set
            {
                _workingDays = value;
                OnPropertyChanged(nameof(WorkingDays));
            }
        }

        public EmployeeViewDTO()
        {

        }

        public bool CheckEmployee(string fullName)
        {
            bool isCorrect = false;
            if (FullName == fullName)
                isCorrect = true;
            return isCorrect;
        }

        public int UpdateEmployeeView(PreferencesDTO preferences, ObservableCollection<CalendarDateDTO> calendarDateDTO)
        {
            CalendarHelper calendar = new();
            WorkingDays = calendar.WorkingDaysInMonth() - preferences.FreeWorkingDays;
            for (int i = 0; i < calendarDateDTO.Count; i++)
            {
                if (calendarDateDTO[i].UpdateEmployeeView(FullName))
                    WorkingDays--;
            }
            return WorkingDays;
        }
    }
}
