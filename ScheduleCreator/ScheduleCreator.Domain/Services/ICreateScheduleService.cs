using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Services
{
    interface ICreateScheduleService
    {
        Task<Schedule> AddSchedule();
    }
}
