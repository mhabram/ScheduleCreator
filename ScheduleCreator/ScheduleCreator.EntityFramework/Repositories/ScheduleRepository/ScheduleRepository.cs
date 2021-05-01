using Microsoft.EntityFrameworkCore;
using ScheduleCreator.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.ScheduleRepository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ScheduleCreatorDbContextFactory _contextFactory;

        public ScheduleRepository(ScheduleCreatorDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddEmployeeScheduleDays(string lastName, List<Day> days)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            Employee employee = await context.Employees.SingleOrDefaultAsync(e => e.LastName == lastName);
            employee.Days = days;
            await context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeScheduleDays(string lastName, List<Day> days, string internalId)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            Employee employee= await context.Employees
                .Include(d => d.Days.Where(m => m.MonthId == internalId))
                .FirstOrDefaultAsync(e => e.LastName == lastName);

            for (int i = 0; i < days.Count; i++)
            {
                employee.Days[i].IsWorking = days[i].IsWorking;
                employee.Days[i].Shift = days[i].Shift;
            }

            context.Entry(employee);
            await context.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeSchedule(int id, string internalId)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();

            return await context.Employees
                .Where(e => e.EmployeeId == id)
                .Include(d => d.Days.Where(m => m.MonthId == internalId))
                .FirstOrDefaultAsync();
        }

        public async Task<IList<Employee>> GetSchedule(string internalId)
        {
            IList<Employee> employees = new List<Employee>();

            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();

            employees = await context.Employees
                .Include(d => d.Days.Where(m => m.MonthId == internalId))
                .Include(p => p.Preferences)
                .ThenInclude(d => d.PreferenceDays)
                .Where(i => i.Preferences.InternalPreferenceId == internalId)
                .ToListAsync();

            return employees;
        }
    }
}
