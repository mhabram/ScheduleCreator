using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Repositories.ScheduleRepository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IWeekService _weekService;
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IWeekService weekService, IScheduleRepository scheduleRepository)
        {
            _weekService = weekService;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<IList<Employee>> GetSchedule()
        {
            IList<Employee> employees = new List<Employee>();
            string internalId = String.Concat(DateTime.Now.AddMonths(1).Year.ToString(), DateTime.Now.AddMonths(1).Month.ToString());
            
            employees =  await _scheduleRepository.GetSchedule(internalId);

            return employees;
        }

        public async Task<bool> CreateSchedule(ObservableCollection<CalendarDateDTO> calendarDateDTO)
        {
            Dictionary<string, ICollection<Day>> employeeFullNameWorkingDays = new Dictionary<string, ICollection<Day>>();
            EmployeeDTO employeeDTO;
            bool isSaved = false;
            string shift = "";
            bool isWorking = false;

            for (int i = 0; i < calendarDateDTO.Count; i++)
            {
                for (int j = 0; j < calendarDateDTO[i].Employees.Count; j++)
                {
                    employeeDTO = calendarDateDTO[i].Employees[j];

                    if ((employeeDTO.Shift == null) || (employeeDTO.Shift == ""))
                        shift = "Free";
                    else
                        shift = employeeDTO.Shift;

                    if (employeeDTO.Day || employeeDTO.Swing || employeeDTO.Night)
                        isWorking = true;

                    if (employeeFullNameWorkingDays.ContainsKey(employeeDTO.FullName))
                    {

                        employeeFullNameWorkingDays[employeeDTO.FullName].Add(
                            new Day()
                            {
                                Shift = shift,
                                IsWorking = isWorking,
                                WorkingDay = calendarDateDTO[i].Date
                            });
                    }
                    else
                    {
                        employeeFullNameWorkingDays.Add(employeeDTO.FullName,
                            new List<Day>() {
                                new Day()
                                {
                                    Shift = shift,
                                    IsWorking = isWorking,
                                    WorkingDay = calendarDateDTO[i].Date
                                }
                            });
                    }
                }
            }

            foreach (var key in employeeFullNameWorkingDays)
            {
                isSaved = await _weekService.AddWeeks(key.Key, key.Value);
            }

            //foreach (CalendarDateDTO calendarDTO in calendarDateDTO)
            //{
            //    foreach (EmployeeDTO employeeDTO in calendarDTO.Employees)
            //    {
            //        if ((employeeDTO.Shift == null) || (employeeDTO.Shift == ""))
            //            shift = "Free";
            //        else
            //            shift = employeeDTO.Shift;

            //        if (employeeDTO.Day || employeeDTO.Swing || employeeDTO.Night)
            //            isWorking = true;

            //        if (employeeFullNameWorkingDays.ContainsKey(employeeDTO.FullName))
            //        {

            //            employeeFullNameWorkingDays[employeeDTO.FullName].Add(
            //                new Day()
            //                {
            //                    Shift = shift,
            //                    IsWorking = isWorking,
            //                    //IsWorking = employeeDTO.IsWorking,
            //                    WorkingDay = employeeDTO.Date
            //                });
            //        }
            //        else
            //        {
            //            employeeFullNameWorkingDays.Add(employeeDTO.FullName,
            //                new List<Day>() {
            //                    new Day()
            //                    {
            //                        Shift = shift,
            //                        IsWorking = isWorking,
            //                        //IsWorking = employeeDTO.IsWorking,
            //                        WorkingDay = employeeDTO.Date
            //                    }
            //                });
            //        }
            //    }
            //}

            //foreach (var key in employeeFullNameWorkingDays)
            //{
            //    isSaved = await _weekService.AddWeeks(key.Key, key.Value);
            //}

            return isSaved;
        }
    }
}
