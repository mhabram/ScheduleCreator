using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class EmployeeDTO
    {
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public bool IsWorking { get; set; }
        public string Shift { get; set; }
        public ICollection<DateTime> PreferenceDays { get; set; }
    }
}
