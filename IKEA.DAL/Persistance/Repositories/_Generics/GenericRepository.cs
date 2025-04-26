using IKEA.DAL.Models;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories._Generics
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private readonly ApplicationDbContext dbContext;//property for   ApplicationDbContext , dbContext this property can call through the functions below as this context is ready 

        public GenericRepository(ApplicationDbContext context) //ASK FOR Generate object of Context
        {
            dbContext = context;
        }

        //IEnumrebe ->In-Memory Collection
        //IQuerable ->Database Collection

        #region Old Approach
        //public IEnumerable<T> GetAll(bool WithNoTracking = true)
        //{

        //    if (WithNoTracking)

        //        return dbContext.Set<T>().Where(D => D.IsDeletd == false).AsNoTracking().ToList();   //normal and for Soft Deleted


        //    return dbContext.Set<T>().Where(D => D.IsDeletd == false).ToList();

        //} 
        #endregion
        public IQueryable<T> GetAll(bool WithNoTracking = true)
        {

            if (WithNoTracking)

                return dbContext.Set<T>().AsNoTracking();   //normal and for Soft Deleted


            return dbContext.Set<T>();

        }

        public async T? GetById(int id)
        {
            //using Find is better approach 

            var item =await dbContext.Set<T>().FindAsync(id);    //search by id first locally , if not find then search remote 
            return item;
        }

        public void Add(T item)
        {
            dbContext.Set<T>().Add(item);
            //return dbContext.SaveChanges();
        }

        public void Update(T item)
        {
            dbContext.Set<T>().Update(item);
            //return dbContext.SaveChanges();
        }

        public void Delete(T item)
        {

            //This is Soft delete (Delete Front of the User only) not from the database
            item.IsDeletd = true;
            dbContext.Set<T>().Update(item);
            //return dbContext.SaveChanges();
        }
    }
}
