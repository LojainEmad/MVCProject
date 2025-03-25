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
            builder.Services.AddControllersWithViews();
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
