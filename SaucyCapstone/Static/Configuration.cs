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

        //await db.SeedDataNeededForSession();
        //await db.SeedDataNeededForEnrollments();
    }
    private static async Task AddUserToRole(this UserManager<ApplicationUser> userManager, string username, string role)
    {
        var _user = await userManager.FindByNameAsync(username);
        if (_user is not null) await userManager.AddToRoleAsync(_user, Roles.Admin);
    }

    //private static async Task SeedDataNeededForSession(this ApplicationDbContext db)
    //{
    //    var alreadyExists = await db.Schools.Where(s => s.SchoolName == "Test School").FirstOrDefaultAsync() == null;
    //    if (alreadyExists)
    //    {
    //        var school = new School
    //        {
    //            SchoolName = "Test School"
    //        };

    //        await db.AddAsync(school);

    //        //var subject = new Subject
    //        //{
    //        //    School = school,
    //        //    SubjectName = "Maths"
    //        //};

    //        //await db.AddAsync(subject);

    //        await db.AddAsync(new Term
    //        {
    //            StartDate = DateTime.Now,
    //            EndDate = DateTime.Now.AddMonths(3),
    //            TermName = "Test Term",
    //            IsActive = true
    //        });

    //        var course = new Course
    //        {
    //            //Subject = subject,
    //            CourseName = "Test Course"
    //        };

    //        await db.AddAsync(course);


    //        //guardians
    //        var guardian1 = new Guardian
    //        {
    //            FirstName = "guardian1",
    //            LastName = "guardian1",
    //            ContactInfo = "555-555-5555",
    //            Relation = "father"
    //        };
    //        await db.AddAsync(guardian1);
    //        var guardian2 = new Guardian
    //        {
    //            FirstName = "guardian2",
    //            LastName = "guardian2",
    //            ContactInfo = "777-777-777",
    //            Relation = "mother"
    //        };
    //        await db.AddAsync(guardian2);

    //        //students
    //        var student1 = new Student
    //        {
    //            FirstName = "student1",
    //            LastName = "student1",
    //            Phone = "123-123-1234",
    //            ImageUrl = "\\images\\stock-profile-pic.jpg",
    //            Determination = Student.DeterminationLevel.High,
    //            Status = Student.StudentStatus.OpenApplication,
    //            DateOfBirth = DateTime.MinValue,
    //            AcceptedDate = DateTime.MinValue,
    //            LastModifiedDate = DateTime.Now,
    //            IsActive = false,
    //            Address = "86 Shadow Brook Street",
    //            Village = "Plattsburgh",
    //            Latitude = "",
    //            Longitude = "",
    //            AnnualIncome = 12345,
    //            SchoolLevel = 10,
    //            FoodAssistance = true,
    //            ChappaAssistance = false,
    //        };
    //        await db.AddAsync(student1);
    //        var student2 = new Student
    //        {
    //            FirstName = "student2",
    //            LastName = "student2",
    //            Phone = "456-456-4567",
    //            ImageUrl = "\\images\\stock-profile-pic.jpg",
    //            Determination = Student.DeterminationLevel.Low,
    //            Status = Student.StudentStatus.OpenApplication,
    //            DateOfBirth = DateTime.MinValue,
    //            AcceptedDate = DateTime.MinValue,
    //            LastModifiedDate = DateTime.Now,
    //            IsActive = false,
    //            Address = "265 Lawrence St.",
    //            Village = "Barberton",
    //            Latitude = "",
    //            Longitude = "",
    //            AnnualIncome = 12345,
    //            SchoolLevel = 7,
    //            FoodAssistance = false,
    //            ChappaAssistance = true,
    //        };
    //        await db.AddAsync(student2);

    //        //student-guardians
    //        var studentguardian1 = new StudentGuardian
    //        {
    //            Student = student1,
    //            Guardian = guardian1,
    //        };
    //        await db.AddAsync(studentguardian1);
    //        var studentguardian2 = new StudentGuardian
    //        {
    //            Student = student1,
    //            Guardian = guardian2,
    //        };
    //        await db.AddAsync(studentguardian2);
    //        var studentguardian3 = new StudentGuardian
    //        {
    //            Student = student2,
    //            Guardian = guardian1,
    //        };
    //        await db.AddAsync(studentguardian3);

    //        await db.SaveChangesAsync();
    //    }
    //} 
    //    private static async Task SeedDataNeededForEnrollments(this ApplicationDbContext db)
    //    {
    //        var alreadyExists = await db.Enrollments.Where(s => s.EnrollmentId == 1).FirstOrDefaultAsync() == null;
    //        if (alreadyExists)
    //        {
    //        var school = new School
    //        {
    //            SchoolName = "Bowani"
    //        };

    //        await db.AddAsync(school);

          

    //        var term = new Term
    //        {
    //            StartDate = DateTime.Now,
    //            EndDate = DateTime.Now.AddMonths(3),
    //            TermName = "Summer",
    //            IsActive = true
    //        };

    //        await db.AddAsync(term);
          

    //        var course = new Course
    //        {
    //            Subject = "Computer",
    //            CourseName = "2"
    //        };

    //        await db.AddAsync(course);
    //        var session = new Session
    //        {
    //            Course = course,
    //            Term = term,
    //            DayofWeek = "Monday",
    //            isActive = true,
    //            StartTime = "10:00AM",
    //            EndTime = "12:00PM"
    //        };
    //        await db.AddAsync(session);
    //        var student = new Student
    //        {
    //            FirstName = "Bob",
    //            LastName = "Doe",
    //            Phone = "555-555-5555",
    //            DateOfBirth = DateTime.Now,
    //            AcceptedDate = DateTime.Now,
    //            LastModifiedDate = DateTime.Now,
    //            IsActive = true,
    //            Status = Student.StudentStatus.Active,
    //            SchoolLevel = '8',
    //            FoodAssistance = false,
    //            ChappaAssistance = true,
    //            Determination = Student.DeterminationLevel.High,
    //            AnnualIncome = 1333,
    //            ImageUrl = "~/StudentPictures/obama.jpg",
    //            NotesAndAbout = "Notes about Bob"
    //        };
    //        await db.AddAsync(student);
    //        var Enrollment = new Enrollment
    //        {
    //            Student = student,
    //            Session = session
    //        };
    //        await db.AddAsync(Enrollment);
    //        await db.SaveChangesAsync();
    //    }
    //    }
}
