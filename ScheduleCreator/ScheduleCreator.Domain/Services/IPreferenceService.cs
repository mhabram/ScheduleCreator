using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Services
{
    public interface IPreferenceService
    {
        Task AddPreferences(int employeId, IList<PreferenceDay> preferenceDays, sbyte holidays);
        Task UpdatePreferences(int preferenceId, IList<PreferenceDay> preferenceDays, sbyte holidays);
        Task<Preferences> GetPreferences(int employeId);
    }
}
