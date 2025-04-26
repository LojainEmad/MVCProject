using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>   //IdentityUser IdentityRole string
    {
        //Dependency Injection
        //exp : Department require => Context => Options , as  ask clr to generate options for my contexet
         
        //this is empty constructor make chaining on base , to the base can call the last version of onConfiguring function which has optionsBuilder 

        //inject optionsBuilder

        //options has the conncetion string
        //Ask CLR Generate Options for My Context
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {
            

        }
        #region old
        ////this on the Entity Frame work
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=. , Database=IKEA_G02 ; trusted_Connection =true ; TrustServerCertificate = true");
        //}
        #endregion

        //to apply configurations made using fluent api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());           //assembly is mean the current project   //search for any ine how implement the IEntityTypeConfiguration  , to can implement the configurations
        }
        public DbSet<Department> Departments { get; set; }   //this we the table be ready
        public DbSet<Employee> Employees { get; set; }

        //public DbSet<IdentityUser> Users {  get; set; }

    }
}
