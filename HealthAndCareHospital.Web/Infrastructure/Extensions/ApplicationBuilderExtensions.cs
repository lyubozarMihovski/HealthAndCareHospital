namespace HealthAndCareHospital.Common.Infrastructure.Extensions
{
    using HealthAndCareHospital.Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<HealthAndCareHospitalDbContext>().Database.Migrate();

               var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
               var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
               
               Task
                   .Run(async () =>
                   {
                       var roleName = WebConstants.AdministratorRole;
               
                       var roleExists = await roleManager.RoleExistsAsync(roleName);
               
                       if (!roleExists)
                       {
                           await roleManager.CreateAsync(new IdentityRole
                           {
                               Name = roleName
                           });
               
                           await roleManager.CreateAsync(new IdentityRole
                           {
                               Name = WebConstants.DoctorRole
                           });
                       }
               
                       var adminEmail = "admin@admin.bg";
                       var doctorEmail = "doctor@doctor.bg";
               
                       var adminUserExists = await userManager.FindByNameAsync(adminEmail);
                       var doctorUserExists = await userManager.FindByNameAsync(doctorEmail);
               
                       if (doctorUserExists == null)
                       {
                           doctorUserExists = new User
                           {
                               Email = doctorEmail,
                               UserName = doctorEmail
                           };
               
                           await userManager.CreateAsync(doctorUserExists, "Doctor@2");
                           await userManager.AddToRoleAsync(doctorUserExists, WebConstants.DoctorRole);
                       }
                       if (adminUserExists == null)
                       {
                           adminUserExists = new User
                           {
                               Email = adminEmail,
                               UserName = adminEmail
                           };
               
                           await userManager.CreateAsync(adminUserExists, "Admin@2");
                           await userManager.AddToRoleAsync(adminUserExists, roleName);
                       }
                   })
               .Wait();
            }

            return app;
        }
    }
}
