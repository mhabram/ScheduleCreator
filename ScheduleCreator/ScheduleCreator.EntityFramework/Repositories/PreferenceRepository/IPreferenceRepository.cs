using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.PreferenceRepository
{
    public interface IPreferenceRepository
    {
        Task AddPreferences(int employeeId, Preferences preferences);
        Task UpdatePreferences(int preferenceId, Preferences preferences);
        Task<Preferences> GetPreferences(int employeeId, string internalPreferenceId);
        Task<List<Preferences>> GetPreferences(string internalPreferenceId);
        Task DeleteFromTo(string internalPreferenceId, int employeeId);
    }
}
