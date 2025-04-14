using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Repositories._Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Employees
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        #region IEmployeeRepository
        ////WILL REMOVE THEM AS MAKE IGenericRepository to reduce the redundancy 
        //IEnumerable<Employee> GetAll(bool WithNoTracking = true);  //will not track anything //this function return things form type IEnumrable

        //Employee? GetById(int id);   //return specific department by its ID

        //int Add(Employee employee);   //return num of rows affected after that 

        //int Update(Employee employee);

        //int Delete(Employee employee);
        #endregion

        //here will add thig which is related to employee only specific 

    }
}
