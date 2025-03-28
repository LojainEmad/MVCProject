using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data.Configurations.EmployeeConfigurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();

            builder.Property(E => E.Address).HasColumnType("varchar(100)");

            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

            //we want to represent the gender as string ot num as in enum
            //delegate 
            //when store in database as string
            //and when retrieve it , will be retrieved as enum

            builder.Property(e => e.Gender).HasConversion
                (
                (gender) => gender.ToString(),   //when stored as string
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender)           //when retrieved as enum nums

                );

            builder.Property(e => e.EmployeeType).HasConversion
                (
                (Type) => Type.ToString(),   //when stored as string
                (Type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), Type)           //when retrieved as enum nums

                );


            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");    //add default value for the time when i create as developer

            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()"); //for every modify , it change 



        }
    }
}
