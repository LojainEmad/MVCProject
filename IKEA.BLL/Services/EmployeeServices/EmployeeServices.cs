using IKEA.BLL.Dto_s.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeServices:IEmployeeServices
    {
        private readonly IEmployeeRepository repository;
        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            repository=employeeRepository;


        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return repository.GetAll().Where(E => E.IsDeletd == false).Select(E => new EmployeeDto()
            {

                Id=E.Id,
                Name=E.Name,
                Age=E.Age,
                Salary=E.Salary,
                IsActive=E.IsActive,
                Email=E.Email,
                Gender = E.Gender,
                EmployeeType =E.EmployeeType,
            }).ToList();

        }


        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee= repository.GetById(id);
            if (employee is not null)
            {
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    LastModifiedBy =employee.LastModifiedBy,
                    CreatedBy =employee.CreatedBy,
                    LastModifiedOn =employee.LastModifiedOn,
                    CreatedOn =employee.CreatedOn,  
                };

            }
            return null; 
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var Employee = new Employee()
            {
                Name= employeeDto.Name,
                Age= employeeDto.Age,
                Address= employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate= employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy=1,
                LastModifiedBy=1,
                LastModifiedOn =DateTime.Now,
                CreatedOn = DateTime.Now,

            };

            return repository.Add(Employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var Employee = new Employee()
            {
                Id= employeeDto.Id, 
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,

            };

            return repository.Update(Employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = repository.GetById(id);
            //int result=0;
            if (employee is not null)
                return repository.Delete(employee) > 0;   //will return num of roes effected
            else
                return false;
        }


    }
}
