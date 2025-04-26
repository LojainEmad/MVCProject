using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public interface IEmployeeServices
    {


        Task <IEnumerable<EmployeeDto>> GetAllEmployees(string search);
        Task <EmployeeDetailsDto>? GetEmployeeById(int id);


        //each model is different in the properties which have
        Task <int> CreateEmployee(CreatedEmployeeDto employeeDto);

        Task <int> UpdateEmployee(UpdatedEmployeeDto employeeDto);
        Task <bool> DeleteEmployee(int id);

    }
}
