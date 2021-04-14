using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.WeekRepository
{
    public interface IWeekRepository
    {
        Task<bool> SaveWeeks(ICollection<Week> weeks, string lastName);
    }
}
