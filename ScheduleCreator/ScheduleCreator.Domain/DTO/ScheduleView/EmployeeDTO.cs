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

        public EmployeeDTO(){ }
        public EmployeeDTO(int calendarId, int employeeId, string fullName, string shift = "")
        {
            CalendarDateDTOId = calendarId;
            EmployeeId = employeeId;
            FullName = fullName;
            Day = false;
            Swing = false;
            Night = false;

            if (shift == "Free")
                Shift = "";
            else
                Shift = shift;

            switch (shift)
            {
                case "Day":
                    Day = true;
                    break;
                case "Swing":
                    Swing = true;
                    break;
                case "Night":
                    Night = true;
                    break;
                default:
                    break;
            }
        }

        public void CorrectShift()
        {
            switch (Shift)
            {
                case "Day":
                    Swing = false;
                    Night = false;
                    break;
                case "Swing":
                    Day = false;
                    Night = false;
                    break;
                case "Night":
                    Day = false;
                    Swing = false;
                    break;
                default:
                    break;
            }
        }

        public void UpdateEmployee(string shift, bool isWorking)
        {
            if (isWorking)
                Shift = shift;
            if ((Shift == shift) && !isWorking)
                Shift = "";
        }

        public void UpdateEmployeeView(ObservableCollection<CalendarDateDTO> calendarDateDTO,
            ObservableCollection<EmployeeViewDTO> employeeViewDTO,
            IList<PreferencesDTO> preferences)
        {
            string lastName = FullName.Split()[1];
            PreferencesDTO pref = preferences.Where(e => e.LastName == lastName.ToLower()).FirstOrDefault();

            for (int i = 0; i < employeeViewDTO.Count; i++)
            {
                employeeViewDTO[i].SetStartingWorkingDays(pref);
                for (int j = 0; j < calendarDateDTO.Count; j++)
                {
                    calendarDateDTO[j].UpdateEmployeeView(employeeViewDTO[i]);
                }
            }
        }

        public bool IsPreferenceDay(ObservableCollection<CalendarDateDTO> calendarDates, IList<PreferencesDTO> preferences)
        {
            string lastName = FullName.Split()[1];
            PreferencesDTO pref = preferences.Where(e => e.LastName == lastName.ToLower()).FirstOrDefault();
            return pref.PreferenceDays.Any(d => d.FreeDayChosen.Day == calendarDates.ElementAt(CalendarDateDTOId).Date.Day);
        }

        public int GetWorkingDays(ObservableCollection<EmployeeViewDTO> employeeViewDTO)
        {
            int workingDays = 0;

            for (int i = 0; i < employeeViewDTO.Count; i++)
            {
                workingDays = employeeViewDTO[i].GetWorkingDays(FullName);
                if (workingDays >= 0)
                    break;
            }

            return workingDays;
        }

        public bool IsAssigned()
        {
            bool isAssigned = false;

            if (Day)
                isAssigned = true;
            if (Swing)
                isAssigned = true;
            if (Night)
                isAssigned = true;

            return isAssigned;
        }
    }
}
