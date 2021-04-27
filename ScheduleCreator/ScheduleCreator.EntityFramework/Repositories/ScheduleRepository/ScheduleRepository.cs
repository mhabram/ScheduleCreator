using Microsoft.EntityFrameworkCore;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
            Employee employee = await context.Employees.SingleOrDefaultAsync(e => e.LastName == lastName.ToLower());
            employee.Days = days;
            await context.SaveChangesAsync();
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
