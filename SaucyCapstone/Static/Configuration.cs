using Microsoft.AspNetCore.Identity;

namespace SaucyCapstone.Static
{
    public static class ConfigurationStaticMethods
    {
        public static async Task SeedDataAsync(this IServiceProvider provider)
        {
            string[] roleNames = { "Admin", "Student", "Instructor" };
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            foreach (var roleName in roleNames)
            {
                IdentityResult roleResult;
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
