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
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> AddEmployee(string name, string lastName)
        {
            Employee employee = new Employee
            {
                Name = name.ToLower(),
                LastName = lastName.ToLower()
            };

            return await _employeeRepository.AddEmployee(employee);
        }

        public async Task<int> GetEmployee(string lastName)
        {
            int employeId = await _employeeRepository.GetEmployee(lastName.ToLower());

            return employeId;
        }
    }
}
