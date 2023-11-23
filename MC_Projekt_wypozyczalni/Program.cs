using MC_Projekt_wypozyczalni.Data;
using MC_Projekt_wypozyczalni.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using NuGet.Protocol.Core.Types;
using System;
using MC_Projekt_wypozyczalni.Areas.Admin.Controllers;
using MC_Projekt_wypozyczalni.Models;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("wypozyczalnia"));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddTransient<IRepositoryService<Vehicle>, RepositoryService<Vehicle>>();
        builder.Services.AddTransient<IRepositoryService<VehicleType>, RepositoryService<VehicleType>>();
        builder.Services.AddTransient<IRepositoryService<Reservation>, RepositoryService<Reservation>>();
        builder.Services.AddTransient<IRepositoryService<LoaningPoint>, RepositoryService<LoaningPoint>>();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddValidatorsFromAssemblyContaining<ReservationValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<LoaningPointValidator>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
        });

        app.MapRazorPages();


        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "Operator", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // creating users for debug purposes
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string email = "admin@admin.com";
            string password = "Admin!23";

            if(await userManager.FindByNameAsync(email) == null)
            {
                var user = new IdentityUser();
                user.UserName = email;
                user.Email = email;
                user.EmailConfirmed = true;

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, "Admin");
                await userManager.AddToRoleAsync(user, "User");
            }

            string email2 = "operator@operator.com";
            string password2 = "Operator!23";

            if(await userManager.FindByNameAsync(email2) == null)
            {
                var user = new IdentityUser();
                user.UserName = email2;
                user.Email = email2;
                user.EmailConfirmed = true;

                await userManager.CreateAsync(user, password2);

                await userManager.AddToRoleAsync(user, "Operator");
                await userManager.AddToRoleAsync(user, "User");
            }
        }

        app.Run();

    }

}

