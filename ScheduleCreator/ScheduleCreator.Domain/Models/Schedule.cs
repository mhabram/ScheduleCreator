using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    public class Schedule : GenericModel
    {
        public int Month { get; set; }
        public int WorkingDays { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
