using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PROG6212_POE_CMCS.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PROG6212_POE_CMCS
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Lecturer", "ProgramCoordinator" };

            // Create roles if they don't exist
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create Admin user if not exists
            var adminUser = await userManager.FindByEmailAsync("admin@123.com");
            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = "admin@123.com",
                    Email = "admin@123.com",
                    FullName = "Admin User"
                };
                var result = await userManager.CreateAsync(newAdmin, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                    Console.WriteLine("Admin user created and assigned to Admin role.");
                }
                else
                {
                    Console.WriteLine("Error creating Admin user.");
                }
            }

            // Create Lecturer user if not exists
            var lecturerUser = await userManager.FindByEmailAsync("lecturer@123.com");
            if (lecturerUser == null)
            {
                var newLecturer = new ApplicationUser
                {
                    UserName = "lecturer@123.com",
                    Email = "lecturer@123.com",
                    FullName = "Lecturer User"
                };
                var result = await userManager.CreateAsync(newLecturer, "Lecturer123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newLecturer, "Lecturer");
                    Console.WriteLine("Lecturer user created and assigned to Lecturer role.");
                }
                else
                {
                    Console.WriteLine("Error creating Lecturer user.");
                }
            }

            // Create Program Coordinator user if not exists
            var coordinatorUser = await userManager.FindByEmailAsync("coordinator@123.com");
            if (coordinatorUser == null)
            {
                var newCoordinator = new ApplicationUser
                {
                    UserName = "coordinator@123.com",
                    Email = "coordinator@123.com",
                    FullName = "Program Coordinator User"
                };
                var result = await userManager.CreateAsync(newCoordinator, "Coordinator123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newCoordinator, "ProgramCoordinator");
                    Console.WriteLine("Program Coordinator user created and assigned to ProgramCoordinator role.");
                }
                else
                {
                    Console.WriteLine("Error creating Program Coordinator user.");
                }
            }
        }
    }
}
