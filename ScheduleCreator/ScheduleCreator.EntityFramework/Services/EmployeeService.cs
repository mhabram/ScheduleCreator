using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Repositories.EmployeeRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeRepository;

        public EmployeeService(IEmployeeRepository employeRepository)
        {
            _employeRepository = employeRepository;
        }

        public Task<Employee> AddEmployee(string name, string lastName)
        {
            Employee employee = new Employee
            {
                Name = name,
                LastName = lastName
            };

            return _employeRepository.AddEmployee(employee);
        }
    }
}
