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

        public Task<EmployeeModel> AddEmployee(string name, string lastName)
        {
            EmployeeModel employee = new EmployeeModel
            {
                Name = name,
                LastName = lastName
            };

            return _employeRepository.AddEmployee(employee);
        }
    }
}
