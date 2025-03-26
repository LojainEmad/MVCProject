using IKEA.DAL.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    public class DepartmentServices:IDepartmentServices
        
    {   //CLR Make all these things , i donot create the obj by myself , this done by CLR , and i determine its lifecycle ,,,,, make one obj per request (one obj for repository , one for services , one for options , ..
        //Controller => Services => Repositories => Context => Options
        //Controller => call Services => services depend on inject thing of (Repository) = > repository depend on inject thing of (Context) => context depend on inject thing of (Options)

        //Repository

        //the interface is reference and wait for obj , any obj implemet the repository as department repository , oracle repository ,...
        private IDepartmentRepository Repository;    //develop against concrete class not true if i change the services  , so develop against interface is true 

        //------------------------------------------------

        //when thing call Services , which is (Controller) which is in PL 
        public DepartmentServices(DepartmentRepository _repository)  //inject in the Services thing of the repository 
        {
            Repository = _repository;

        }

        //Implementation of Services , but before that Repository is Ready first 
    }
}
