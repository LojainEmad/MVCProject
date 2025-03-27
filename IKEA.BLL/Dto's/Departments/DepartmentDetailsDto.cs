using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Dto_s.Departments
{
    public class DepartmentDetailsDto  
    {

        #region Administrator  
        //has the part which is related to the administrator , any one admin will access them  , which is in modelBase
        public int Id { get; set; }

        public bool IsDeletd { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }
        #endregion


        //related to DepartmentModule
        public string Name { get; set; } = null!;  

        public string Code { get; set; } = null!;

        public string? Description { get; set; }  

        public DateOnly CreationDate { get; set; }   

    }
}
