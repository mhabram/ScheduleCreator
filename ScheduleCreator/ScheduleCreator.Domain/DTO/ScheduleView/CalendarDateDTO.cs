using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class CalendarDateDTO
    {
        public int Id { get; set; }
        public ObservableCollection<EmployeeDTO> Employees { get; set; }
        public DateTime Date { get; set; }

        public bool IsWeekend()
        {
            bool isWeekend = false;

            switch (Date.DayOfWeek.ToString())
            {
                case "Saturday":
                    isWeekend = true;
                    break;
                case "Sunday":
                    isWeekend = true;
                    break;
                default:
                    break;
            }

            return isWeekend;
        }

        public bool UpdateEmployeeView(string fullName)
        {
            bool isAssigned = false;
            for (int i = 0; i < Employees.Count; i++)
            {
                if ((Employees[i].FullName == fullName) && (Employees[i].IsAssigned()))
                    isAssigned = true;
            }
            return isAssigned;
        }
    }
}
