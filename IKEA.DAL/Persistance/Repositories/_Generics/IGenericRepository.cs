using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories._Generics
{
    public interface IGenericRepository<T> where T :ModelBase
    {
        //IEnumerable<T> GetAll(bool WithNoTracking = true);  //will not track anything //this function return things form type IEnumrable
        IQueryable<T> GetAll(bool WithNoTracking = true);  

        T? GetById(int id);   //return specific department by its ID

        int Add(T entity);   //return num of rows affected after that 

        int Update(T entity);

        int Delete(T entity);

    }
}
