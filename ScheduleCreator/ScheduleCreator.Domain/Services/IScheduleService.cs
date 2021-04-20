using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Services
{
    public interface IScheduleService
    {
        Task<bool> CreateSchedule(ObservableCollection<CalendarDateDTO> calendarDateDTO);
        Task<IList<Employee>> GetSchedule();
    }
}
