using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;
using System.Text.Json;
using static Data.Student;

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

    private static async Task SeedData(this ApplicationDbContext db)
    { 

        var alreadyExists = await db.Terms.Where(s => s.TermId == 1).FirstOrDefaultAsync() == null;
        if (alreadyExists)
        {
            //school
            var school = new School
            {
                SchoolName = "Bowani"
            };
            var school2 = new School
            {
                SchoolName = "Weber"
            };
            await db.AddAsync(school);
            await db.AddAsync(school2);

            //subject
            var subject = new Subject
            {
                SubjectName = "English"
            };
            var subject2 = new Subject
            {
                SubjectName = "IT"
            };
            await db.AddAsync(subject);
            await db.AddAsync(subject2);


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
                TermName = "Summer",
                IsActive = true
            };
            var term2 = new Term
            {
                StartDate = DateTime.Now.AddMonths(3),
                EndDate = DateTime.Now.AddMonths(6),
                TermName = "Spring",
                IsActive = false
            };
            await db.AddAsync(term);
            await db.AddAsync(term2);

            //course
            var course = new Course
            {
                School = school,
                CourseLevel = 1,
                Subject = subject,
                Term = term,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course4 = new Course
            {
                School = school,
                CourseLevel = 2,
                Subject = subject,
                Term = term,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course5 = new Course
            {
                School = school,
                CourseLevel = 3,
                Subject = subject,
                Term = term,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course2 = new Course
            {
                School = school,
                CourseLevel = 2,
                Subject = subject2,
                Term = term2,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course6 = new Course
            {
                School = school,
                CourseLevel = 3,
                Subject = subject2,
                Term = term2,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course3 = new Course
            {
                School = school2,
                CourseLevel = 1,
                Subject = subject2,
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

            //applicant

            var students = new List<Student>
            {
                new Student
                {
                    FirstName = "Karl",
                    LastName = "Guy",
                     EnglishLevel = 1,
                ITLevel = 1,
                    DateOfBirth = new DateTime(1990, 7, 7),
                    AcceptedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    IsActive = false,
                    Status = StudentStatus.Applicant,
                    AppStatus = ApplicationStatus.Open,
                    SchoolLevel = '1',
                    FoodAssistance = false,
                    ChappaAssistance = true,
                    Determination = DeterminationLevel.Middle
                },
            };
            db.AddRange(students);
            db.SaveChanges();

            var student1 = new Student
            {
                FirstName = "Jane",
                LastName = "Doe",
                Phone = "123-123-1234",
                EnglishLevel = 1,
                ITLevel = 1,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.High,
                AppStatus = Student.ApplicationStatus.Open,
                Status = Student.StudentStatus.Applicant,
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
                FirstName = "Ashley",
                LastName = "Smith",
                Phone = "456-456-4567",
                EnglishLevel = 3,
                ITLevel = 3,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.Low,
                AppStatus = Student.ApplicationStatus.Approved,
                Status = Student.StudentStatus.Active,
                DateOfBirth = DateTime.MinValue,
                AcceptedDate = DateTime.MinValue,
                LastModifiedDate = DateTime.Now,
                IsActive = true,
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

            //students
            var student3 = new Student
            {
                FirstName = "Hailey",
                LastName = "Jones",
                Phone = "123-123-1234",
                EnglishLevel = 2,
                ITLevel = 2,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.High,
                Status = Student.StudentStatus.Active,
                DateOfBirth = DateTime.MinValue,
                AcceptedDate = DateTime.MinValue,
                LastModifiedDate = DateTime.Now,
                IsActive = true,
                Address = "86 Shadow Brook Street",
                Village = "Plattsburgh",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 12345,
                SchoolLevel = 10,
                FoodAssistance = true,
                ChappaAssistance = false,
            };
            var student4 = new Student
            {
                FirstName = "Tiffany",
                LastName = "Bates",
                EnglishLevel = 1,
                ITLevel = 1,
                Phone = "456-456-4567",
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.Low,
                Status = Student.StudentStatus.Active,
                DateOfBirth = DateTime.MinValue,
                AcceptedDate = DateTime.MinValue,
                LastModifiedDate = DateTime.Now,
                IsActive = true,
                Address = "265 Lawrence St.",
                Village = "Barberton",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 12345,
                SchoolLevel = 7,
                FoodAssistance = false,
                ChappaAssistance = true,
            };
            await db.AddAsync(student3);
            await db.AddAsync(student4);

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
                StartTime = DateTime.MinValue,
                EndTime = DateTime.MinValue.AddHours(9),
                IsActive = true,
                Course = course
            };
            var session2 = new Session
            {
                DayofWeek = "Tuesday",
                StartTime = DateTime.MinValue,
                EndTime = DateTime.MinValue.AddHours(9),
                IsActive = true,
                Course = course
            };
            var session3 = new Session
            {
                DayofWeek = "Thursday",
                StartTime = DateTime.MinValue,
                EndTime = DateTime.MinValue.AddHours(9),
                IsActive = true,
                Course = course2
            };
            await db.AddAsync(session);
            await db.AddAsync(session2);
            await db.AddAsync(session3);

            //grade
            var grade = new Grade
            {
                AssessmentGrade = "A+",
                BeginningRange = 1,
                EndingRange = 1
            };
            var grade2 = new Grade
            {
                AssessmentGrade = "B+",
                BeginningRange = 1,
                EndingRange = 1
            };
            await db.AddAsync(grade);
            await db.AddAsync(grade2);


            //enrollment
            var enrollment = new Enrollment
            {
                Student = student1,
                Session = session,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Ongoing,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment2 = new Enrollment
            {
                Student = student2,
                Session = session,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 85,
                Grade = grade2
            };
            await db.AddAsync(enrollment);
            await db.AddAsync(enrollment2);

            ////attendance
            //var attendance = new Attendance
            //{
            //    Status = Attendance.AttendanceStatus.OnTime,
            //    Date = DateTime.Now,
            //    Session = session
            //};
            //var attendance2 = new Attendance
            //{
            //    Status = Attendance.AttendanceStatus.OnTime,
            //    Date = DateTime.Now,
            //    Session = session
            //};
            //var attendance3 = new Attendance
            //{
            //    Status = Attendance.AttendanceStatus.OnTime,
            //    Date = DateTime.Now,
            //    Session = session
            //};
            //await db.AddAsync(attendance);
            //await db.AddAsync(attendance2);
            //await db.AddAsync(attendance3);

            //Session Dates

            //assessment (homework)
            var assessment = new Assessment
            {
                Score = 88,
                Session = session,
                DueDate = DateTime.Now.AddDays(14),
                MaxScore = 100,
                Grade = grade,
                Title = "Homework 1",
                Description = "Homework 1 Description"
            };
            var assessment2 = new Assessment
            {
                Score = 77,
                Session = session,
                DueDate = DateTime.Now.AddDays(7),
                MaxScore = 100,
                Grade = grade,
                Title = "Homework 2",
                Description = "Homework 2 Description"
            };
            var assessment3 = new Assessment
            {
                Score = 66,
                Session = session,
                DueDate = DateTime.Now.AddDays(3),
                MaxScore = 100,
                Grade = grade2,
                Title = "Homework 3",
                Description = "Homework 3 Description"
            };
            await db.AddAsync(assessment);
            await db.AddAsync(assessment2);
            await db.AddAsync(assessment3);

            //criterion
            var criteria = new List<Criterion> {
                new Criterion
                {
                    Description = "Academics",
                    Weight = .25m
                },
                new Criterion
                {
                    Description = "Age",
                    Weight = .05m
                },
                new Criterion
                {
                    Description = "Distance",
                    Weight = .05m
                },
                new Criterion
                {
                    Description = "Family support and interest",
                    Weight = .4m
                },
                new Criterion
                {
                    Description = "Finanaces",
                    Weight = .25m
                }
            };
            await db.AddRangeAsync(criteria);
            await db.SaveChangesAsync();



            //rating


            //accesstype
            var accesstype = new AccessType
            {
                Accesss = AccessType.Type.Rater
            };
            await db.AddAsync(accesstype);

            //note
            var note = new Note
            {
                Student = student1,
                FacultyMember = faculty,
                Topic = "this is the note topic",
                Content = "this is the note content",
                isPrivate = false,
                CreatedDate = DateTime.Now,
                EditedDate = DateTime.Now,
                NoteType = accesstype
            };
            var note2 = new Note
            {
                Student = student2,
                FacultyMember = faculty,
                Topic = "this is the note topic",
                Content = "this is the note content",
                isPrivate = false,
                CreatedDate = DateTime.Now,
                EditedDate = DateTime.Now,
                NoteType = accesstype
            };
            await db.AddAsync(note);
            await db.AddAsync(note2);

            //student doc
            var studentdoc = new StudentDoc
            {
                Student = student1,
                AccessType = accesstype,
                Name = "document name",
                Description = "document description",
                Path = "path goes here",
                Extension = "extension goes here",
                FileGUID = "",
                UploadDate = DateTime.Now
            };
            await db.AddAsync(studentdoc);
            var studentdoc2 = new StudentDoc
            {
                Student = student3,
                AccessType = accesstype,
                Name = "document name",
                Description = "document description",
                Path = "path goes here",
                FileGUID = "",
                Extension = "extension goes here",
                UploadDate = DateTime.Now
            };
            await db.AddAsync(studentdoc2);


            await db.SaveChangesAsync();

        }
    }
}