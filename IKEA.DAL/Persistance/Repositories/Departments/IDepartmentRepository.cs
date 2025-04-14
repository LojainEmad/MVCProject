using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories._Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
    //GetAll GetById Add Update Delete
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        #region IDepartmentRepository
        ////WILL REMOVE THEM AS MAKE IGenericRepository to reduce the redundancy 
        //IEnumerable<Department> GetAll(bool WithNoTracking = true);  //will not track anything //this function return things form type IEnumrable

        //Department? GetById(int id);   //return specific department by its ID

        //int Add(Department department);   //return num of rows affected after that 

        //int Update(Department department);

        //int Delete(Department department);
        #endregion

        //here will add thig which is related to department only specific 


    }
}
