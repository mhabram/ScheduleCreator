using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Services
{
    public interface IPreferenceService
    {
        Task AddPreferences(int employeId, IList<PreferenceDay> preferenceDays, DateTime from, DateTime to, sbyte holidays);
        Task UpdatePreferences(int employeeId, IList<PreferenceDay> preferenceDays, DateTime from, DateTime to, sbyte holidays);
        Task<Preferences> GetPreferences(int employeId);
        Task<List<PreferencesDTO>> GetPreferences();
    }
}
