using IKEA.BLL.Dto_s.Departments;
using IKEA.DAL.Models.Departments;
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

        //when thing call Services , this is (Controller) which is in PL 
        public DepartmentServices(IDepartmentRepository _repository)  //inject in the Services thing of the repository 
        {
            Repository = _repository;

        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            //Mapping =>then make mapping from department to departmentDto
            //Manual Mapping 
            var Departments = Repository.GetAll().Select(dept => new DepartmentDto()
            {
                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate,

            }).ToList();

            return Departments;

            #region Using foreach
            ////Mapping =>then make mapping from department to departmentDto
            ////Manual Mapping 
            //List<DepartmentDto> departmentDtos = new List<DepartmentDto>();
            //foreach (var dept in Departments)
            //{
            //    DepartmentDto departmentDto = new DepartmentDto()
            //    {
            //        Id = dept.Id,
            //        Name = dept.Name,
            //        Code = dept.Code,
            //        CreationDate = dept.CreationDate,


            //    };
            //    departmentDtos.Add(departmentDto);
            //}
            //return departmentDtos;
            #endregion

        }

        public DepartmentDetailsDto? GetDepartmentById(int id)         //make it nullable as if it is null not get error 
         {
           var Department = Repository.GetById(id);
            if(Department is not null)
                return new DepartmentDetailsDto()
                {
                    Id = Department.Id,
                    Name = Department.Name,
                    Code = Department.Code,
                    Description=Department.Description,
                    CreationDate = Department.CreationDate,
                    IsDeletd = Department.IsDeletd,
                    LastModifiedBy = Department.LastModifiedBy,
                    LastModifiedOn = Department.LastModifiedOn,
                    CreatedBy = Department.CreatedBy,
                    CreatedOn = Department.CreatedOn,

                };
            return null;

            
        }

        public int CreateDepartment(CreatedDepartmentDto departmentDto)   //here we enter CreatedDepartmentDto as Department
        {
            var CreatedDepartment = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,                //as simple indocated , but after that there is a actual one
                CreatedOn = DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            return Repository.Add(CreatedDepartment);
        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var UpdatedDepartment = new Department()
            {
                //created by and created on will not change
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,

            };
            return Repository.Update(UpdatedDepartment);
        }

        public bool DeleteDepartment(int id)
        {
            var department =Repository.GetById(id);
            //int result=0;
            if (department is not null)
                return Repository.Delete(department)>0;   //will return num of roes effected
            else
                return false;
        }



        //Implementation of Services , but before that -> Repository is Ready first 


    }
}
