using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Services
{
    public interface IWeekService
    {
        Task<bool> AddWeeks(string fullName, ICollection<Day> days);
    }
}
