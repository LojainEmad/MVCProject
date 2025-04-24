using IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Dto_s.Employees
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public int? Age { get; set; }
        public string? Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public string? Department { get; set; }

        #region Administrator


        public int CreatedBy { get; set; }  //who created the model(as department) , the user id

        public DateTime CreatedOn { get; set; }   //related to the system and developer and security

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }
        #endregion
    }
}
