using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string LastName { get; set; }

        [ForeignKey("PreferencesId")]
        public virtual Preferences Preferences { get; set; }
        public virtual ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }
    }
}
