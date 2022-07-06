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

        await db.SeedData();
    }
    private static async Task AddUserToRole(this UserManager<ApplicationUser> userManager, string username, string role)
    {
        var _user = await userManager.FindByNameAsync(username);
        if (_user is not null) await userManager.AddToRoleAsync(_user, Roles.Admin);
    }

    private static async Task SeedDataNeededForSession(this ApplicationDbContext db)
    {

        var alreadyExists = await db.Terms.Where(s => s.TermId == 1).FirstOrDefaultAsync() == null;
        if (alreadyExists)
        {
            //faculty-member
            var faculty = new FacultyMember
            {
                FirstName = "Jo",
                LastName = "Mama",
                IsAdmin = true,
                IsInstructor = true,
                IsRater = true,
                IsSocialWorker = true
            };
            var faculty2 = new FacultyMember
            {
                FirstName = "Jimmy",
                LastName = "Mama",
                IsAdmin = false,
                IsInstructor = true,
                IsRater = false,
                IsSocialWorker = false,
            };
            await db.AddAsync(faculty);
            await db.AddAsync(faculty2);

            //terms
            var term = new Term
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                TermName = "Test Term",
                IsActive = true
            };
            var term2 = new Term
            {
                StartDate = DateTime.Now.AddMonths(3),
                EndDate = DateTime.Now.AddMonths(6),
                TermName = "Test Term2",
                IsActive = false
            };
            await db.AddAsync(term);
            await db.AddAsync(term2);

            //course
            var course = new Course
            {
                CourseName = "E101",
                SchoolName = "Bowani",
                Subject = "English",
                Term = term,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course2 = new Course
            {
                CourseName = "C101",
                SchoolName = "Bowani",
                Subject = "Computer",
                Term = term,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course3 = new Course
            {
                CourseName = "History-10th",
                SchoolName = "Public",
                Subject = "History",
                Term = term2,
                Instructor = faculty,
                Sessions = new List<Session>()
            };
            await db.AddAsync(course);
            await db.AddAsync(course2);
            await db.AddAsync(course3);

            //guardians
            var guardian1 = new Guardian
            {
                FirstName = "guardian1",
                LastName = "guardian1",
                ContactInfo = "555-555-5555",
                Relation = "father"
            };
            var guardian2 = new Guardian
            {
                FirstName = "guardian2",
                LastName = "guardian2",
                ContactInfo = "777-777-777",
                Relation = "mother"
            };
            await db.AddAsync(guardian1);
            await db.AddAsync(guardian2);

            //students
            var student1 = new Student
            {
                FirstName = "student1",
                LastName = "student1",
                Phone = "123-123-1234",
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.High,
                Status = Student.StudentStatus.OpenApplication,
                DateOfBirth = DateTime.MinValue,
                AcceptedDate = DateTime.MinValue,
                LastModifiedDate = DateTime.Now,
                IsActive = false,
                Address = "86 Shadow Brook Street",
                Village = "Plattsburgh",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 12345,
                SchoolLevel = 10,
                FoodAssistance = true,
                ChappaAssistance = false,
            };
            var student2 = new Student
            {
                FirstName = "student2",
                LastName = "student2",
                Phone = "456-456-4567",
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.Low,
                Status = Student.StudentStatus.OpenApplication,
                DateOfBirth = DateTime.MinValue,
                AcceptedDate = DateTime.MinValue,
                LastModifiedDate = DateTime.Now,
                IsActive = false,
                Address = "265 Lawrence St.",
                Village = "Barberton",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 12345,
                SchoolLevel = 7,
                FoodAssistance = false,
                ChappaAssistance = true,
            };
            await db.AddAsync(student1);
            await db.AddAsync(student2);

            //student-guardians
            var studentGuardians = new List<StudentGuardian>{
                new StudentGuardian
                {
                    Student = student1,
                    Guardian = guardian1,
                },
                new StudentGuardian
                {
                    Student = student1,
                    Guardian = guardian2,
                },
                new StudentGuardian
                {
                    Student = student2,
                    Guardian = guardian1,
                }
            };
            await db.AddRangeAsync(studentGuardians);
            await db.SaveChangesAsync();

            //sessions
            var session = new Session
            {
                DayofWeek = "Monday",
                StartTime = "10:00 AM",
                EndTime = "11:00 AM",
                IsActive = true,
                Course = course
            };
            var session2 = new Session
            {
                DayofWeek = "Tuesday",
                StartTime = "9:30 AM",
                EndTime = "10:30 AM",
                IsActive = true,
                Course = course
            };
            var session3 = new Session
            {
                DayofWeek = "Thursday",
                StartTime = "10:00 AM",
                EndTime = "11:00 AM",
                IsActive = true,
                Course = course2
            };
            await db.AddAsync(session);
            await db.AddAsync(session2);
            await db.AddAsync(session3);

            var enrollment = new Enrollment
            {
                Student = student1,
                Course = course,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Ongoing,
                FinalGrade = 0
            };
            var enrollment2 = new Enrollment
            {
                Student = student1,
                Course = course2,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 85
            };
            await db.AddAsync(enrollment);
            await db.AddAsync(enrollment2);

            //attendance
            var attendance = new Attendance
            {
                Status = Attendance.AttendanceStatus.OnTime,
                Date = DateTime.Now,
                Session = session
            };
            var attendance2 = new Attendance
            {
                Status = Attendance.AttendanceStatus.OnTime,
                Date = DateTime.Now,
                Session = session
            };
            var attendance3 = new Attendance
            {
                Status = Attendance.AttendanceStatus.OnTime,
                Date = DateTime.Now,
                Session = session2
            };
            await db.AddAsync(attendance);
            await db.AddAsync(attendance2);
            await db.AddAsync(attendance3);

            //assessment (homework)
            var assessment = new Assessment
            {
                Score = 88,
                DueDate = DateTime.Now.AddDays(14),
                MaxScore = 100
            };
            var assessment2 = new Assessment
            {
                Score = 77,
                DueDate = DateTime.Now.AddDays(7),
                MaxScore = 100
            };
            var assessment3 = new Assessment
            {
                Score = 66,
                DueDate = DateTime.Now.AddDays(3),
                MaxScore = 100
            };
            await db.AddAsync(assessment);
            await db.AddAsync(assessment2);
            await db.AddAsync(assessment3);


            //sessionAssessment
            var sessionassessment = new SessionAssessment
            {
                Assessment = assessment,
                Session = session
            };
            var sessionassessment2 = new SessionAssessment
            {
                Assessment = assessment2,
                Session = session
            };
            var sessionassessment3 = new SessionAssessment
            {
                Assessment = assessment3,
                Session = session
            };
            var sessionassessment4 = new SessionAssessment
            {
                Assessment = assessment3,
                Session = session2
            };
            var sessionassessment5 = new SessionAssessment
            {
                Assessment = assessment3,
                Session = session2
            };
            await db.AddAsync(sessionassessment);
            await db.AddAsync(sessionassessment2);
            await db.AddAsync(sessionassessment3);
            await db.AddAsync(sessionassessment4);
            await db.AddAsync(sessionassessment5);



            await db.SaveChangesAsync();

        }
    }
}
