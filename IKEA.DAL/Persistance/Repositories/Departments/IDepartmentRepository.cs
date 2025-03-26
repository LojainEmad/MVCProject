using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
    //GetAll GetById Add Update Delete
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool WithNoTracking = true);  //will not track anything //this function return things form type IEnumrable

        Department? GetById(int id);   //return specific department by its ID

        int Add(Department department);   //return num of rows affected after that 

        int Update(Department department); 

        int Delete(Department department);  

    }
}
