using IKEA.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data.Configurations.DepartmentConfigurations
{
    //to add the configurations which related to the department, as fluent api
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
           builder.Property(D=>D.Id).UseIdentityColumn(10,10);  //Id is primary as conventional and we add the identity
            builder.Property(D => D.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(D => D.Code).HasColumnType("varchar(20)").IsRequired();

            //Development Usage

            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");    //add default value for the time when i create as developer

            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()"); //for every modify , it change 


            builder.HasMany(D=>D.Employees)
                .WithOne(E=>E.Department)
                .HasForeignKey(E=>E.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
