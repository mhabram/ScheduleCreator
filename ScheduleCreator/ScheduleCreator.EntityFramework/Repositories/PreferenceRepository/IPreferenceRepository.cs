using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.PreferenceRepository
{
    public interface IPreferenceRepository
    {
        Task AddPreferences(int employeId, Preferences preferences);
        Task UpdatePreferences(int preferenceId, IList<PreferenceDay> preferenceDays, sbyte holidays);
        Task<Preferences> GetPreferences(int employeId, string internalPreferenceId);
    }
}
