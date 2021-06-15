using ScheduleCreator.Domain.DTO.PreferenceView;
using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Services
{
    public interface IPreferenceService
    {
        Task AddPreferences(int employeeId, ObservableCollection<DateTimeWrapper> preferenceDays,
            DateTime? from, DateTime? to, int leaveDays, sbyte holidays);
        Task UpdatePreferences(int employeeId, ObservableCollection<DateTimeWrapper> preferenceDays,
            DateTime? from, DateTime? to, int leaveDays, sbyte holidays);
        Task<Preferences> GetPreferences(int employeeId);
        Task<List<PreferencesDTO>> GetPreferences();
    }
}
