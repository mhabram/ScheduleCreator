using ScheduleCreator.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.ScheduleRepository
{
    public interface IScheduleRepository
    {
        Task<IList<Employee>> GetSchedule(string internalId);
        Task AddEmployeeScheduleDays(string lastName, List<Day> days);
        Task UpdateEmployeeScheduleDays(string lastName, List<Day> days, string internalId);
        Task<Employee> GetEmployeeSchedule(int id, string internalId);
    }
}
