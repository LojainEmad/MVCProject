using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Dto_s.Employees;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services - DI
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]  //Employee/Index
        public IActionResult Index()
        {
            var Employees = employeeServices.GetAllEmployees();

            return View(Employees);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto EmployeeDto)
        {
            //ServerSide Validation
            if (!ModelState.IsValid)
            {
                return View(EmployeeDto);
            }
            var Message = string.Empty;


            try
            {
                var Result = employeeServices.CreateEmployee(EmployeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    Message = "Employee is not Created";

                }
            }
            catch (Exception ex)
            {
                //1-Log Exception Kestral (not appear for enduser)
                logger.LogError(ex, ex.Message);

                //2-Set Default Message User
                if (environment.IsDevelopment())
                    Message = ex.Message;
                else
                {
                    Message = "An Error affects at the Creation Operator";

                }

            }
            ModelState.AddModelError(string.Empty, Message);
            return View(EmployeeDto);


            //return View();


        }
        #endregion

    }
}
