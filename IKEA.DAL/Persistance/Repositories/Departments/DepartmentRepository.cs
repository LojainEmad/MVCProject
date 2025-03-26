using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {//Services will call these Repositories 
        //Repository -> is depend on inject thing of type Context -> Context is depend on inject thing of options , S o the options is passed through mthod AddDbContext which in program.cs

        //will implement these functions based on the Database Context ,So I now need to holde obj form this Context 


        //readOnly - encapsulated
        private readonly ApplicationDbContext dbContext;//property for   ApplicationDbContext , dbContext this property can call through the functions below as this context is ready 

        public DepartmentRepository(ApplicationDbContext context) //ASK FOR Generate object of Context
        {
            dbContext = context;
        }
        public IEnumerable<Department> GetAll(bool WithNoTracking = true)
        {
            if(WithNoTracking)
                return dbContext.Departments.AsNoTracking().ToList();

            return dbContext.Departments.ToList();

        }

        public Department? GetById(int id)
        {
            //using Find is better approach 

            var Department = dbContext.Departments.Find(id);    //search by id first locally , if not find then search remote 
            return Department;
            //-----------------

            //var Department = dbContext.Departments.Local.FirstOrDefault(D => D.Id == id); //firt search Locally 
            //if(Department == null)  // this mean department is not in local 
            //    Department= dbContext.Departments.FirstOrDefault(D => D.Id == id);  //search remote if not in local 
            //return Department;
        }

        public int Add(Department department)
        {
            dbContext.Departments.Add(department);
            return dbContext.SaveChanges();
        }

        public int Update(Department department)
        {
            dbContext.Departments.Update(department);
            return dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            dbContext.Departments.Remove(department);
            return dbContext.SaveChanges();
        }


      
    }
}
