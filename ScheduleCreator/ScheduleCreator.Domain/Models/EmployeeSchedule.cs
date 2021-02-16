using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    public class EmployeeSchedule
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
