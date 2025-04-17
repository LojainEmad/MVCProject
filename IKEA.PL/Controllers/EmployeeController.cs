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

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = employeeServices.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);
        }
        #endregion


        #region Update

        [HttpGet]   //Get:   /Department/Edit/10
        public IActionResult Edit(int? id)
        {
            //will make mapping to convert from departmentDetailsDto to UpdatedDepartmentDto , to limit the things which the user can edit and update 
            if (id is null)
                return BadRequest();

            var Employee = employeeServices.GetEmployeeById(id.Value);
            if (Employee is null)
                return NotFound();

            var MappedEmployee = new UpdatedEmployeeDto()
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                HiringDate = Employee.HiringDate,
                Salary = Employee.Salary,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                Gender = Employee.Gender,
                EmployeeType = Employee.EmployeeType,   
                IsActive = Employee.IsActive,   
            };

            return View(MappedEmployee);
        }

        [HttpPost]
        public IActionResult Edit(UpdatedEmployeeDto employeeDto)
        {

            //check the validation for the model if exist 
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }
            var Message = String.Empty;
            try
            {
                var Result = employeeServices.UpdateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Employee is Not Updated";
            }
            catch (Exception ex)
            {
                //1.log Exceptions through kestral
                logger.LogError(ex, ex.Message);

                //2.Set Message

                Message = environment.IsDevelopment() ? ex.Message : "An Error has been occured during Update the Employee!";

            }

            ModelState.AddModelError(string.Empty, Message);
            return View(employeeDto);
        }
        #endregion

    }
}
