using IKEA.BLL.Common.Services.Attachments;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using IKEA.DAL.Models.Identity;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using IKEA.DAL.Persistance.UnitOfWork;
using IKEA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IKEA.PL
{
    public class Program
    {
        //Entry point run kestral consoleApp
        public static void Main(string[] args)
        {
            #region Cofigure Services
            // Add services to the container.
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();    //object =>Department =>services =>Repository => Context => options  --------> this happen per Request 

            //Dependency Injection , when i create context , will inject in it the options
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });

            //Allow DI => USERManager SignInManager RoleManager
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options)=>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;  //#$%
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

            }).AddEntityFrameworkStores<ApplicationDbContext>();

            #region Old 
            ////----------------------------------------
            ////Old , addScoped => make me create an obj from class i want Per Request . 
            ////this work generics , when i ask for obj from ApplicationDbContext , this is created at a time when i need per request 

            //builder.Services.AddScoped<ApplicationDbContext>();

            ////this for create DbContextOptions (which has the connection) for the my context (ApplicationDbContext)
            //builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>((service) =>
            //{
            //    var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //    optionBuilder.UseSqlServer("Server=. , Database=IKEA_G02 ; trusted_Connection =true ; TrustServerCertificate = true");

            //    var options = optionBuilder.Options;    //cas the configurations which is as connectionBuilder

            //    return options;
            //});
            #endregion

            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); //when any thing require thing from IDepartmentRepository (which is Services ) , send to it thing from type DepartmentRepository

            //-------------------------------

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();   //here told CLR , when thing want from type  IDepartmentServices , pass to it thing from type DepartmentServices

            builder.Services.AddScoped<IAttachmentServices , AttachmentServices>();
            builder.Services.AddAutoMapper(M => M.AddProfile(typeof(MappingProfile)));
              


            #endregion

            #region Configure Pipelines (MiddleWares)
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");  //if this not exist , will go to Shared/Error
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();  //to redirect from http to https
            app.UseStaticFiles();  //related to files, images

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
