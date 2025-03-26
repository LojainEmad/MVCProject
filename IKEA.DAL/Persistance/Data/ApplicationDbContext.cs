using IKEA.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data
{
    internal class ApplicationDbContext: DbContext
    {
        //to apply configurations made using fluent api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());           //assembly is mean the current project   //search for any ine how implement the IEntityTypeConfiguration  , to can implement the configurations
        }
        public DbSet<Department> Departments { get; set; }   //this we the table be ready


    }
}
