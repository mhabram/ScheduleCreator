using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.Domain.Services
{
    interface ICreateScheduleService
    {
        Task<CreateSchedule> AddSchedule();
    }
}
