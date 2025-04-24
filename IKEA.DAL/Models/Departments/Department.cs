using IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Departments
{
    public class Department: ModelBase
    {
        //data annotation will be added in interface in views , dtos , in front end
        public string Name { get; set; } = null!;   //as not equal null

        public string Code { get; set; } = null!;

        public string? Description { get; set; }   //nullable , can be passed or not

        public DateOnly CreationDate { get; set; }    //this represent the date of create the department and this for user , for business role and endUser


        //Navigational property [Many]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }

}
