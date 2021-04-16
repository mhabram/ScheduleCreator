using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.ScheduleRepository
{
    public interface IScheduleRepository
    {
        Task<ICollection<Employee>> GetSchedule(string internalId);
    }
}
