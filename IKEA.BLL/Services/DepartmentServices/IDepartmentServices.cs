//using IKEA.DAL.Models.Departments;
using IKEA.BLL.Dto_s.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    //has signatures for the methods which will be used through controller in view ->help in maintainability and Separation of Concepts(SOC)
    public interface IDepartmentServices
    {
        //has some services as: getOneDepartment , AddDepartment , ....
        //we donot interact with the Department module 
        //DTO ->(Data Transfer Object ) this represent the model in the BLL , has the things which is useful only for endUser , make concept SOC .

        Task<IEnumerable<DepartmentDto>> GetAllDepartments();
        Task<DepartmentDetailsDto>? GetDepartmentById(int id);


        //each model is different in the properties which have
        Task<int> CreateDepartment(CreatedDepartmentDto departmentDto);

        Task<int> UpdateDepartment(UpdatedDepartmentDto departmentDto);
        Task<bool> DeleteDepartment(int id);

    }
}
