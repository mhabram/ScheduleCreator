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

        public async Task<ICollection<Employee>> GetSchedule(string internalId)
        {
            ICollection<Employee> employees = new Collection<Employee>();

            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            
            employees = await context.Employees
                .Include(w => w.Weeks.Where(i => i.InternalWeekId.Contains(internalId)))
                .ThenInclude(s => s.Days)
                .Include(p => p.Preferences)
                .ThenInclude(d => d.Dates)
                .Where(i => i.Preferences.InternalPreferenceId == internalId)
                .ToListAsync();

            return employees;
        }
    }
}
