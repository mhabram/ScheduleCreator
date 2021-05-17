using ScheduleCreator.Domain.DTO.Observable;
using ScheduleCreator.Domain.Helpers.Calendar;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;

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

        public void SetWorkingDays()
        {

        }

        public int GetWorkingDays(string fullName)
        {
            int workingDays = -1;

            if (FullName == fullName)
                workingDays = WorkingDays;

            return workingDays;
        }

        public void SetStartingWorkingDays(PreferencesDTO preferences)
        {
            CalendarHelper calendarHelper = new();
            WorkingDays = calendarHelper.WorkingDaysInMonth(preferences.FreeWorkingDays);
        }
    }
}
