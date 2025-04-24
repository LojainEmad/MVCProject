using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.UnitOfWork
{
    public interface IUnitOfWork //: IDisposable   //to call the function of dispose without call it 
    {
        public IDepartmentRepository DepartmentRepository { get;  }

        public IEmployeeRepository EmployeeRepository { get;  }

        int Complete();

        //void Dispose();       //use it if we make manual connection


    }
}
