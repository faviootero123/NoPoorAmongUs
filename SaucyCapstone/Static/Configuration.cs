using Microsoft.AspNetCore.Identity;
using SaucyCapstone.Constants;

namespace SaucyCapstone.Static
{
    public static class ConfigurationStaticMethods
    {
        public static async Task SeedDataAsync(this IServiceProvider provider)
        {
            //Seed Roles
            string[] roleNames = { Roles.Admin, Roles.Student, Roles.Instructor, Roles.SocialWorker };
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            
            foreach (var roleName in roleNames)
            {
                IdentityResult roleResult;
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            ///Seed users
            var users = new List<IdentityUser>()
            {
                new IdentityUser
                {

                }
            };

            IdentityResult userResult;
            foreach (var user in users)
            {
                var userExists  = await userManager.FindByNameAsync(user.UserName);
                if(userExists is not null) {
                    userResult = await userManager.CreateAsync(user);
                }
                    
            }
            IdentityResult addToRoleResult;
            //how to add role to a user
            var _user = await userManager.FindByNameAsync("username");
            if (_user is not null) addToRoleResult = await userManager.AddToRoleAsync(_user, Roles.Admin);
        }
    }
}
