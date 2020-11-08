using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    public class Employee : GenericModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Shift { get; set; }
        public int WorkingDays { get; set; }
    }
}
