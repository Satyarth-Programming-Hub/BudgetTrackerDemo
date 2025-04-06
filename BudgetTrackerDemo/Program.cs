using BudgetTrackerDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Connection to MS SQL Server Database
            builder.Services.AddDbContext<BudgetTrackerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BudgetTrackerConnection")));

            var app = builder.Build();

            // Initialize the database with the "Uncategorized" category
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BudgetTrackerContext>();
                ApplicationDbInitializer.Initialize(dbContext);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
