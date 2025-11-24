using Business.Services.Abstract;
using Business.Services.Concrate;
using BusinessLayer;
using BusinessLayer.Services;
using DataAccess;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrate;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RBCProjectMVC.Services;
using System;


namespace RBCProjectMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllersWithViews();

            builder.Services.AddDataAccessConfiguration(builder.Configuration);
            builder.Services.AddBusinessConfiguration(builder.Configuration);
            builder.Services.AddScoped<IFileEnvironment, WebFileEnvironment>();

            var app = builder.Build();

    
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseSwagger(); 
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RBCProject API V1");
                c.RoutePrefix = "swagger"; // https://localhost:44314/swagger
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
