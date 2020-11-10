using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework;
using ScheduleCreator.EntityFramework.Services;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataService<Employee> employesService = new GenericDataService<Employee>(new ScheduleCreatorDbContextFactory());
            //Console.WriteLine(employesService.GetAll().Result);
            employesService.Create(
                new Employee { Name = "Mateusz", LastName = "Test", Shift = 1, FreeWorkingDays = 10 }
                ).Wait();
        }
    }
}
