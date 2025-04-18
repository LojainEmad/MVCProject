using IKEA.BLL.Dto_s.Departments;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.PL.ViewModel;
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

        #region Services - DI
        private readonly IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _departmentServices, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            logger = _logger;
            this.environment = environment;
        } 
        #endregion

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
        [ValidateAntiForgeryToken]   //enable the browser to send the token which i use , so Create method no one can use it except the token which the server send 
        //يعني مش هيخليني اعمل كرييت الا من عند الويب سايت عشان وقتها هتتعمل توكين غير كدة لا 
        public IActionResult Create(DepartmentVM departmentVM)
        {
            //ServerSide Validation
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var Message = string.Empty;


            try
            {
                var departmentDto = new CreatedDepartmentDto()
                {
                    Name= departmentVM.Name,
                    Code= departmentVM.Code,
                    CreationDate= departmentVM.CreationDate,
                    Description= departmentVM.Description,
                };
                var Result = departmentServices.CreateDepartment(departmentDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Department is not Created";
            }
            catch (Exception ex)
            {
                //1-Log Exception Kestral (not appear for enduser)
                logger.LogError(ex, ex.Message);

                //2-Set Default Message User
                if (environment.IsDevelopment())
                
                    Message = ex.Message;
                
                else
                
                    Message = "An Error affects at the Creation Operator";

            }
            ModelState.AddModelError(string.Empty, Message);
            return View(departmentVM);


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

            var MappedDepartment = new DepartmentVM()
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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentVM departmentVM)
        {

            //check the validation for the model if exist 
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var Message =String.Empty;
            try
            {
                var departmentDto = new UpdatedDepartmentDto()
                {
                    Id = departmentVM.Id,
                    Name = departmentVM.Name,
                    Code = departmentVM.Code,
                    CreationDate = departmentVM.CreationDate,
                    Description = departmentVM.Description,
                };
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
            return View(departmentVM);
        }
        #endregion

        #region Delete (HARD DELETE) ->Already delete from database
        [HttpGet]          //to take id first throw the bottun
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var Department = departmentServices.GetDepartmentById(id.Value);

            if (Department is null)
                return NotFound();
            return View(Department);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (int DeptId)
        {
            var Message=String.Empty;
            try
            {
                var IsDeleted = departmentServices.DeleteDepartment(DeptId);    
                if(IsDeleted)
                    return RedirectToAction(nameof(Index));
                Message = "Department is Not Deleted";
            }
            catch(Exception ex)
            {

                //1.log Exceptions through kestral
                logger.LogError(ex, ex.Message);

                //2.Set Message

                Message = environment.IsDevelopment() ? ex.Message : "An Error has been occured during delete the Department!";

            }
            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new {id= DeptId });
        }


        #endregion

    }
}
