using ScheduleCreator.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.ScheduleRepository
{
    public interface IScheduleRepository
    {
        Task<IList<Employee>> GetSchedule(string internalId);
        Task<bool> AddEmployeeScheduleDays(string lastName, List<Day> days);
    }
}
