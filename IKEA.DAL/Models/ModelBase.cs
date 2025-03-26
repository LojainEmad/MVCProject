using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models
{
    public class ModelBase
    {
        //these attributes are common with modules 

        public int Id { get; set; }

        public bool IsDeletd { get; set; }  //Soft Delete , make the user delete only on interface , but still is recorded in database

        public int CreatedBy { get; set; }  //who created the model(as department) , the user id

        public DateTime CreatedOn { get; set; }   //related to the system and developer and security

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }



    }
}
