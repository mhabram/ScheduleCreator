using ScheduleCreator.Domain.Models;
using System;
using System.Collections.ObjectModel;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class CalendarDateDTO
    {
        public ObservableCollection<EmployeeDTO> Employees { get; set; }
        public DateTime Date { get; set; }
    }
}
