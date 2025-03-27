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
        public IActionResult Index()  //main page
        {

            return View();
        }
    }
}
