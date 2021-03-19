using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    public class Week
    {
        [Key]
        public int WeekId { get; set; }
        [Required]
        public byte WorkingDays { get; set; }
        [Required]
        public string InternalWeekId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        public virtual ICollection<Day> Days { get; set; }
    }
}
