using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;
using System.Text.Json;

namespace SaucyCapstone.Static;

public static class ConfigurationStaticMethods
{
    public static async Task SeedDataAsync(this IServiceProvider provider)
    {
        // Allows us to access scoped services from the DI Container such as DbContext
        using var scope = provider.CreateAsyncScope();
        //Seed Roles
        string[] roleNames = { Roles.Admin, Roles.Instructor, Roles.SocialWorker };
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // Ensure the database is there and the current migrations are applied
        await db.Database.EnsureCreatedAsync();
        // Remove Existing roles
        var currentRoles = await roleManager.Roles.ToListAsync();

        foreach (var role in currentRoles)
        {
            await roleManager.DeleteAsync(role);
        }
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

        /// Add Users That you want to seed
        var users = new Dictionary<ApplicationUser, string>()
        {
           {
              new ApplicationUser
              {
                UserName ="AdminUser@odetopeaches.com",
                Email = "AdminUser@odetopeaches.com" ,
                EmailConfirmed = true,
              },
              "SuperUserDo@123!"
           },
           {
               new ApplicationUser
               {
                    UserName ="InstructorUser@odetopeaches.com",
                    Email = "InstructorUser@odetopeaches.com" ,
                    EmailConfirmed = true,
               },
               "InstructorUserDo@123!"
           }
        };

        IdentityResult userResult;
        foreach (var user in users)
        {
            var userExists = await userManager.FindByEmailAsync(user.Key.Email);
            if (userExists is not null) await userManager.DeleteAsync(userExists);
            userResult = await userManager.CreateAsync(user.Key, user.Value);
        }

        // Apply role to user 
        await userManager.AddUserToRole("AdminUser@odetopeaches.com", Roles.Admin);
        await userManager.AddUserToRole("InstructorUser@odetopeaches.com", Roles.Instructor);
        //Seed data for other tables 
        await db.SeedDataNeededForSession();
    }
    private static async Task AddUserToRole(this UserManager<ApplicationUser> userManager, string username, string role)
    {
        var _user = await userManager.FindByNameAsync(username);
        if (_user is not null) await userManager.AddToRoleAsync(_user, Roles.Admin);
    }

    private static async Task SeedDataNeededForSession(this ApplicationDbContext db)
    {
        var alreadyExists = await db.Schools.Where(s => s.SchoolName == "Test School").FirstOrDefaultAsync() == null;
        if (alreadyExists)
        {
            var school = new School
            {
                SchoolName = "Test School"
            };

            await db.AddAsync(school);

            var course = new Course
            {
                School = school,
                SubjectName = "Test Subject",
                CourseName = "Test Course"
            };

            await db.AddAsync(course);

            await db.AddAsync(new Term
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                TermName = "Test Term",
                IsActive = true
            });

            await db.SaveChangesAsync();
        }
    }
}
