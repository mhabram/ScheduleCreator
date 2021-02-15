using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    [Table("Employee")]
    public class EmployeeModel : GenericModel
    {
        public EmployeeModel()
        {
            Schedules = new HashSet<ScheduleModel>();
        }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string LastName { get; set; }

        [ForeignKey("Id")]
        public virtual PreferencesModel Preferences { get; set; }
        [ForeignKey("Id")]
        public virtual ICollection<ScheduleModel> Schedules { get; set; }
    }
}
