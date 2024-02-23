using Mataeem.Models;
using Mataeem.RequestHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mataeem.Data
{
    public class Seed
    {

        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> _roleManager,
            ILoggerFactory loggerFactory)
        {
            try
            {
                // Users & Role
                if (!userManager.Users.Any())
                {
                    // Create roles
                    var roles = new List<IdentityRole>
                    {
                        new IdentityRole { Name = RolesNames.IT },
                        new IdentityRole { Name = RolesNames.SUPERADMIN },
                        new IdentityRole { Name = RolesNames.ADMIN },
                        new IdentityRole { Name = RolesNames.DRIVER },
                        new IdentityRole { Name = RolesNames.CUSTOMER },
                    };

                    // Add roles to the context and save changes
                    foreach (var role in roles)
                    {
                        await _roleManager.CreateAsync(role);
                    }

                    await context.SaveChangesAsync();

                    // Create a IT
                    var IT = new AppUser
                    {
                        DisplayName = "IT",
                        UserName = "IT",
                        Email = "IT@manager.com"
                    };
                    // IT password
                    var createdItUser = await userManager.CreateAsync(IT, "Pa$$w0rd");

                    // Check if the user was successfully created before assigning roles
                    if (createdItUser.Succeeded)
                    {
                        // Find the ADMIN role
                        var role = await context.Roles
                            .Where(x => x.Name == RolesNames.IT)
                            .FirstOrDefaultAsync();

                        if (role != null)
                        {
                            // Add the user to the ADMIN role
                            await userManager.AddToRoleAsync(IT, role?.Name!);

                            // Save changes after adding the user to the role
                            await context.SaveChangesAsync();
                        }
                    }

                    // Create a Supper user
                    var supperUser = new AppUser
                    {
                        DisplayName = "Manager",
                        UserName = "Manager",
                        Email = "manager@manager.com"
                    };

                    // Supper user password
                    var createdSupperUser = await userManager.CreateAsync(supperUser, "Passw0rd");

                    if (createdSupperUser.Succeeded)
                    {
                        // Find the ADMIN role
                        var role = await context.Roles
                            .Where(x => x.Name == RolesNames.SUPERADMIN)
                            .FirstOrDefaultAsync();

                        if (role != null)
                        {
                            // Add the user to the ADMIN role
                            await userManager.AddToRoleAsync(supperUser, role?.Name!);

                            // Save changes after adding the user to the role
                            await context.SaveChangesAsync();
                        }
                    }
                }

            }
            catch (SystemException ex)
            {
                var logger = loggerFactory.CreateLogger<Seed>();
                logger.LogError(ex.Message);
            }
        }

    }
}
