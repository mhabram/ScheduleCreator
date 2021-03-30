using ScheduleCreator.Domain.Models;
using System;
using System.Collections.ObjectModel;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class CalendarDateDTO
    {
        public Collection<EmployeeDTO> Employees { get; set; }
        public DateTime Date { get; set; }
    }
}
