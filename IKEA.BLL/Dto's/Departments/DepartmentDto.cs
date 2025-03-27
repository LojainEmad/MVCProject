﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Dto_s.Departments
{
    //take only the things which will be displayed for user for the department 
    public class DepartmentDto
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;   

        public string Code { get; set; } = null!;

        [Display(Name="Date Of Creation")]
        public DateOnly CreationDate { get; set; }    
    }
}
