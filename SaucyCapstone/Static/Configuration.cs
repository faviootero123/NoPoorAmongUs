using Data;
using Microsoft.AspNetCore.Identity;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;

namespace SaucyCapstone.Static;

public static class ConfigurationStaticMethods
{
    public static async Task SeedDataAsync(this IServiceProvider provider)
    {
        // Allows us to access scoped services from the DI Container such as DbContext
        using var scope = provider.CreateAsyncScope();
        //Seed Roles
        string[] roleNames = { Roles.Admin, Roles.Student, Roles.Instructor, Roles.SocialWorker };
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // Ensure the database is there and the current migrations are applied
        await db.Database.EnsureCreatedAsync();
        // Add the roles 
        foreach (var roleName in roleNames)
        {
            IdentityResult roleResult;
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new ApplicationRole(roleName));
            }
        }

        /// Add Users 
        var users = new Dictionary<ApplicationUser, string>()
        {
           {
            new ApplicationUser
                {
                    Email = "Admin@odetopeaches.com",
                },
                "SuperUserDo@!"
            }
        };

        IdentityResult userResult;
        foreach (var user in users)
        {
            var userExists = await userManager.FindByEmailAsync(user.Key.Email);
            if (userExists is null)
            {
                userResult = await userManager.CreateAsync(user.Key, user.Value);
            }

        }
        IdentityResult addToRoleResult;
        // Apply role to user 
        var _user = await userManager.FindByNameAsync("Admin@odetopeaches.com");
        if (_user is not null) addToRoleResult = await userManager.AddToRoleAsync(_user, Roles.Admin);

        //Seed data for other tables 
    }
}
