using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories._Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Employees
{ 
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        #region OLD APPROACH WITH NO GENERICS 
        //public class EmployeeRepository : IEmployeeRepository
        //{
            //private readonly ApplicationDbContext dbContext;//property for   ApplicationDbContext , dbContext this property can call through the functions below as this context is ready 

            //public EmployeeRepository(ApplicationDbContext context) //ASK FOR Generate object of Context
            //{
            //    dbContext = context;
            //}
            //public IEnumerable<Employee> GetAll(bool WithNoTracking = true)
            //{
            //    if (WithNoTracking)

            //        return dbContext.Employees.Where(D => D.IsDeletd == false).AsNoTracking().ToList();   //normal and for Soft Deleted


            //    return dbContext.Employees.Where(D => D.IsDeletd == false).ToList();

            //}

            //public Employee? GetById(int id)
            //{
            //    //using Find is better approach 

            //    var Employee = dbContext.Employees.Find(id);    //search by id first locally , if not find then search remote 
            //    return Employee;
            //}

            //public int Add(Employee employee)
            //{
            //    dbContext.Employees.Add(employee);
            //    return dbContext.SaveChanges();
            //}

            //public int Update(Employee employee)
            //{
            //    dbContext.Employees.Update(employee);
            //    return dbContext.SaveChanges();
            //}

            //public int Delete(Employee employee)
            //{

            //    //This is Soft delete (Delete Front of the User only) not from the database
            //    employee.IsDeletd = true;
            //    dbContext.Employees.Update(employee);
            //    return dbContext.SaveChanges();
            //}
            //}
            #endregion

        private readonly ApplicationDbContext dbContext;//property for   ApplicationDbContext , dbContext this property can call through the functions below as this context is ready 

        public EmployeeRepository(ApplicationDbContext context) : base(context) //ASK FOR Generate object of Context
        {
            dbContext = context;
        }


    }
}
