using IKEA.BLL.Dto_s.Departments;
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

        private readonly IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _departmentServices , ILogger<DepartmentController> _logger ,IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            logger = _logger;
            this.environment = environment;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()  //main page
        {
            //view will return data of model
            var Departments =departmentServices.GetAllDepartments();
            return View(Departments);   //view of model
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id )
        {
            if (id is null)
                return BadRequest();

            var department = departmentServices.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            //ServerSide Validation
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }
            var Message = string.Empty;


            try
            {
                var Result = departmentServices.CreateDepartment(departmentDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    Message = "Department is not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto);
                }
            }
            catch (Exception ex)
            {
                //1-Log Exception Kestral (not appear for enduser)
                logger.LogError(ex, ex.Message);

                //2-Set Default Message User
                if (environment.IsDevelopment())
                {
                    Message = ex.Message;
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto);
                }
                else
                {
                    Message = "An Error affects at the Creation Operator";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto);
                }

            }


            //return View();


        }
        #endregion

        #region Update

        [HttpGet]   //Get:   /Department/Edit/10
        public IActionResult Edit(int? id)
        {
            //will make mapping to convert from departmentDetailsDto to UpdatedDepartmentDto , to limit the things which the user can edit and update 
            if (id is null)
                return BadRequest();

            var Department = departmentServices.GetDepartmentById(id.Value);
            if(Department is null)
                return NotFound();

            var MappedDepartment = new UpdatedDepartmentDto()
            {
                Id = Department.Id,
                Name = Department.Name,
                Code = Department.Code,
                Description = Department.Description,
                CreationDate = Department.CreationDate,
            };

            return View(MappedDepartment);
        }

        [HttpPost]
        public IActionResult Edit(UpdatedDepartmentDto departmentDto)
        {

            //check the validation for the model if exist 
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }
            var Message =String.Empty;
            try
            {
                var Result =departmentServices.UpdateDepartment(departmentDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Department is Not Updated";
            }
            catch (Exception ex) 
            {
            //1.log Exceptions through kestral
            logger.LogError(ex,ex.Message);

            //2.Set Message

                Message =environment.IsDevelopment() ? ex.Message : "An Error has been occured during Update the Department!" ;
            
            }

            ModelState.AddModelError(string.Empty, Message);    
            return View(departmentDto);
        }
        #endregion

    }
}
