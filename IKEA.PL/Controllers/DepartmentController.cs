using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //There are 2 relations between Services and Controller
        //-> Inheritance :  DepartmentController is a Controller
        //->Composition: DepartmentController has a IDepartmentService
        //->
        //Service => Departments
        //Will Cal the Services of the Department

        private IDepartmentServices departmentServices;

        public DepartmentController(IDepartmentServices _departmentServices)
        {
            departmentServices = _departmentServices;
        }

        #region Index
        public IActionResult Index()  //main page
        {
            //view will return data of model
            var Departments =departmentServices.GetAllDepartments();
            return View(Departments);   //view of model
        }
        #endregion

    }
}
