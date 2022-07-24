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
            ///////////school\\\\\\\\\\\
            var school = new School
            {
                SchoolName = "Bowani"
            };
            var school2 = new School
            {
                SchoolName = "Weber"
            };
            var school3 = new School
            {
                SchoolName = "Public School"
            };
            await db.AddAsync(school);
            await db.AddAsync(school2);
            await db.AddAsync(school3);

            ///////////subject\\\\\\\\\\\
            var subject = new Subject
            {
                SubjectName = "English"
            };
            var subject2 = new Subject
            {
                SubjectName = "IT"
            };
            var subject3 = new Subject
            {
                SubjectName = "Public"
            };
            await db.AddAsync(subject);
            await db.AddAsync(subject2);
            await db.AddAsync(subject3);


            ///////////faculty-member\\\\\\\\\\\
            var faculty = new FacultyMember
            {
                FirstName = "John",
                LastName = "Doe",
                IsAdmin = false,
                IsInstructor = true,
                IsRater = true,
                IsSocialWorker = true
            };
            var faculty2 = new FacultyMember
            {
                FirstName = "Adam",
                LastName = "Smith",
                IsAdmin = false,
                IsInstructor = true,
                IsRater = false,
                IsSocialWorker = false,
            };
            var faculty3 = new FacultyMember
            {
                FirstName = "Public",
                LastName = "Teacher",
                IsAdmin = false,
                IsInstructor = false,
                IsRater = false,
                IsSocialWorker = false,
            };
            await db.AddAsync(faculty);
            await db.AddAsync(faculty2);
            await db.AddAsync(faculty3);

            ///////////terms\\\\\\\\\\\\
            var term = new Term
            {
                StartDate = DateTime.Now.AddMonths(-6),
                EndDate = DateTime.Now.AddMonths(-3),
                TermName = "Spring22",
                IsActive = false
            };
            var term2 = new Term
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(3),
                TermName = "Summer22",
                IsActive = true
            };
            await db.AddAsync(term);
            await db.AddAsync(term2);

            ////////////course\\\\\\\\\\\\
            //courses for inactive term
            var course = new Course
            {
                School = school,
                CourseLevel = 1,
                Subject = subject,
                Term = term,
                Instructor = faculty,
                Sessions = new List<Session>()
            };
            var course2 = new Course
            {
                School = school,
                CourseLevel = 2,
                Subject = subject,
                Term = term,
                Instructor = faculty,
                Sessions = new List<Session>()
            };
            var course3 = new Course
            {
                School = school,
                CourseLevel = 3,
                Subject = subject,
                Term = term,
                Instructor = faculty,
                Sessions = new List<Session>()
            };
            var course4 = new Course
            {
                School = school,
                CourseLevel = 1,
                Subject = subject2,
                Term = term,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course5 = new Course
            {
                School = school,
                CourseLevel = 2,
                Subject = subject2,
                Term = term,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course6 = new Course
            {
                School = school,
                CourseLevel = 3,
                Subject = subject2,
                Term = term,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course7 = new Course
            {
                School = school3,
                CourseLevel = 0,
                Subject = subject3,
                Term = term,
                Instructor = faculty3,
                Sessions = new List<Session>()
            };

            //courses for active term
            var course8 = new Course
            {
                School = school,
                CourseLevel = 1,
                Subject = subject,
                Term = term2,
                Instructor = faculty,
                Sessions = new List<Session>()
            };
            var course9 = new Course
            {
                School = school,
                CourseLevel = 2,
                Subject = subject,
                Term = term2,
                Instructor = faculty,
                Sessions = new List<Session>()
            };
            var course10 = new Course
            {
                School = school,
                CourseLevel = 3,
                Subject = subject,
                Term = term2,
                Instructor = faculty,
                Sessions = new List<Session>()
            };
            var course11 = new Course
            {
                School = school,
                CourseLevel = 1,
                Subject = subject2,
                Term = term2,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course12 = new Course
            {
                School = school,
                CourseLevel = 2,
                Subject = subject2,
                Term = term2,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course13 = new Course
            {
                School = school,
                CourseLevel = 3,
                Subject = subject2,
                Term = term2,
                Instructor = faculty2,
                Sessions = new List<Session>()
            };
            var course14 = new Course
            {
                School = school3,
                CourseLevel = 0,
                Subject = subject3,
                Term = term2,
                Instructor = faculty3,
                Sessions = new List<Session>()
            };
            await db.AddAsync(course);
            await db.AddAsync(course2);
            await db.AddAsync(course3);
            await db.AddAsync(course4);
            await db.AddAsync(course5);
            await db.AddAsync(course6);
            await db.AddAsync(course7);
            await db.AddAsync(course8);
            await db.AddAsync(course9);
            await db.AddAsync(course10);
            await db.AddAsync(course11);
            await db.AddAsync(course12);
            await db.AddAsync(course13);
            await db.AddAsync(course14);


            ////////////guardians\\\\\\\\\\\\
            var guardian1 = new Guardian
            {
                FirstName = "John",
                LastName = "Smith",
                ContactInfo = "555-555-5555",
                Relation = "father"
            };
            var guardian2 = new Guardian
            {
                FirstName = "Jane",
                LastName = "Brown",
                ContactInfo = "777-777-777",
                Relation = "mother"
            };
            await db.AddAsync(guardian1);
            await db.AddAsync(guardian2);

            ////////////applicant & student\\\\\\\\\\\\
            //applicant
            var applicant1 = new Student
            {
                FirstName = "Jane",
                LastName = "Doe",
                Phone = "111-111-1111",
                EnglishLevel = 1,
                ITLevel = 1,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.Middle,
                AppStatus = Student.ApplicationStatus.Open,
                Status = Student.StudentStatus.Applicant,
                DateOfBirth = DateTime.Now.AddYears(-12),
                AcceptedDate = null,
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
            var applicant2 = new Student
            {
                FirstName = "Haley",
                LastName = "Rogers",
                Phone = "222-222-2222",
                EnglishLevel = 1,
                ITLevel = 1,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.High,
                AppStatus = Student.ApplicationStatus.Open,
                Status = Student.StudentStatus.Applicant,
                DateOfBirth = DateTime.Now.AddYears(-10),
                AcceptedDate = null,
                LastModifiedDate = DateTime.Now,
                IsActive = false,
                Address = "4914 Randolf Street",
                Village = "Walpole",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 23456,
                SchoolLevel = 10,
                FoodAssistance = true,
                ChappaAssistance = true,
            };
            var applicant3 = new Student
            {
                FirstName = "Dawn",
                LastName = "Robles",
                Phone = "333-333-3333",
                EnglishLevel = 1,
                ITLevel = 1,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.Low,
                AppStatus = Student.ApplicationStatus.Open,
                Status = Student.StudentStatus.Applicant,
                DateOfBirth = DateTime.Now.AddYears(-8),
                AcceptedDate = null,
                LastModifiedDate = DateTime.Now,
                IsActive = false,
                Address = "1836 Hart Ridge Road",
                Village = "Grayling",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 34567,
                SchoolLevel = 10,
                FoodAssistance = false,
                ChappaAssistance = false,
            };
            var applicant4 = new Student
            {
                FirstName = "Mandy",
                LastName = "Tucker",
                Phone = "444-444-4444",
                EnglishLevel = 1,
                ITLevel = 1,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.AboveLow,
                AppStatus = Student.ApplicationStatus.Open,
                Status = Student.StudentStatus.Applicant,
                DateOfBirth = DateTime.Now.AddYears(-17),
                AcceptedDate = null,
                LastModifiedDate = DateTime.Now,
                IsActive = false,
                Address = "4576 Murry Street",
                Village = "Highland Mills",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 45678,
                SchoolLevel = 10,
                FoodAssistance = true,
                ChappaAssistance = false,
            };
            var applicant5 = new Student
            {
                FirstName = "Tina",
                LastName = "Cobb",
                Phone = "555-555-5555",
                EnglishLevel = 1,
                ITLevel = 1,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.High,
                AppStatus = Student.ApplicationStatus.Open,
                Status = Student.StudentStatus.Applicant,
                DateOfBirth = DateTime.Now.AddYears(-20),
                AcceptedDate = null,
                LastModifiedDate = DateTime.Now,
                IsActive = false,
                Address = "2323 Camden Place",
                Village = "Huntingdon Valley",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 56789,
                SchoolLevel = 10,
                FoodAssistance = false,
                ChappaAssistance = true,
            };
            await db.AddAsync(applicant1);
            await db.AddAsync(applicant2);
            await db.AddAsync(applicant3);
            await db.AddAsync(applicant4);
            await db.AddAsync(applicant5);

            //students
            var student1 = new Student
            {
                FirstName = "Teresa",
                LastName = "Willis",
                Phone = "666-666-6666",
                EnglishLevel = 1,
                ITLevel = 1,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.High,
                AppStatus = Student.ApplicationStatus.Approved,
                Status = Student.StudentStatus.Active,
                DateOfBirth = DateTime.Now.AddYears(-15),
                AcceptedDate = DateTime.Now.AddYears(-1),
                LastModifiedDate = DateTime.Now,
                IsActive = true,
                Address = "4713 Glen Falls Road",
                Village = "Grayling",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 23333,
                SchoolLevel = 10,
                FoodAssistance = true,
                ChappaAssistance = false,
            };
            var student2 = new Student
            {
                FirstName = "Ashley",
                LastName = "Smith",
                Phone = "777-777-7777",
                EnglishLevel = 3,
                ITLevel = 3,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.Low,
                AppStatus = Student.ApplicationStatus.Approved,
                Status = Student.StudentStatus.Active,
                DateOfBirth = DateTime.Now.AddYears(-14),
                AcceptedDate = DateTime.Now.AddYears(-3),
                LastModifiedDate = DateTime.Now,
                IsActive = true,
                Address = "265 Lawrence St.",
                Village = "Barberton",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 43333,
                SchoolLevel = 7,
                FoodAssistance = false,
                ChappaAssistance = true,
            };
            var student3 = new Student
            {
                FirstName = "Hailey",
                LastName = "Jones",
                Phone = "888-888-8888",
                EnglishLevel = 2,
                ITLevel = 2,
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.High,
                AppStatus = Student.ApplicationStatus.Approved,
                Status = Student.StudentStatus.Active,
                DateOfBirth = DateTime.Now.AddYears(-15),
                AcceptedDate = DateTime.Now.AddYears(-2),
                LastModifiedDate = DateTime.Now,
                IsActive = true,
                Address = "4957 Aplha Avenue",
                Village = "Marshall",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 33433,
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
                Phone = "999-999-9999",
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.Low,
                AppStatus = Student.ApplicationStatus.Approved,
                Status = Student.StudentStatus.Active,
                DateOfBirth = DateTime.Now.AddYears(-15),
                AcceptedDate = DateTime.Now.AddYears(-1),
                LastModifiedDate = DateTime.Now,
                IsActive = true,
                Address = "3320 Charla Lane",
                Village = "Marshall",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 32121,
                SchoolLevel = 7,
                FoodAssistance = false,
                ChappaAssistance = true,
            };
            var student5 = new Student
            {
                FirstName = "Enrollment",
                LastName = "Tester",
                EnglishLevel = 1,
                ITLevel = 1,
                Phone = "999-999-9999",
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.Low,
                AppStatus = Student.ApplicationStatus.Approved,
                Status = Student.StudentStatus.Active,
                DateOfBirth = DateTime.Now.AddYears(-15),
                AcceptedDate = DateTime.Now.AddYears(-1),
                LastModifiedDate = DateTime.Now,
                IsActive = true,
                Address = "Foobar Street",
                Village = "",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 32121,
                SchoolLevel = 7,
                FoodAssistance = false,
                ChappaAssistance = true,
            };
            await db.AddAsync(student1);
            await db.AddAsync(student2);
            await db.AddAsync(student3);
            await db.AddAsync(student4);
            await db.AddAsync(student5);

            ////////////student-guardians\\\\\\\\\\\\
            //every applicant/student should have at least one, so have more
            var studentGuardians = new List<StudentGuardian>{
                new StudentGuardian
                {
                    Student = applicant1,
                    Guardian = guardian1,
                },
                new StudentGuardian
                {
                    Student = applicant2,
                    Guardian = guardian1,
                },
                new StudentGuardian
                {
                    Student = applicant3,
                    Guardian = guardian2,
                },
                new StudentGuardian
                {
                    Student = applicant4,
                    Guardian = guardian2,
                },
                new StudentGuardian
                {
                    Student = applicant5,
                    Guardian = guardian2,
                },
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
                    Guardian = guardian2,
                },
                new StudentGuardian
                {
                    Student = student3,
                    Guardian = guardian2,
                },
                new StudentGuardian
                {
                    Student = student4,
                    Guardian = guardian2,
                },
                new StudentGuardian
                {
                    Student = student5,
                    Guardian = guardian2,
                }
            };
            await db.AddRangeAsync(studentGuardians);
            await db.SaveChangesAsync();

            ////////////sessions\\\\\\\\\\\\
            //course 1 sessions (current term)
            var session = new Session
            {
                DayofWeek = "Monday",
                StartTime = DateTime.MinValue.AddHours(9),
                EndTime = DateTime.MinValue.AddHours(10),
                IsActive = true,
                Course = course8
            };
            var session2 = new Session
            {
                DayofWeek = "Monday",
                StartTime = DateTime.MinValue.AddHours(17),
                EndTime = DateTime.MinValue.AddHours(18),
                IsActive = true,
                Course = course8
            };
            var session3 = new Session
            {
                DayofWeek = "Wednesday",
                StartTime = DateTime.MinValue.AddHours(9),
                EndTime = DateTime.MinValue.AddHours(10),
                IsActive = true,
                Course = course8
            };
            var session4 = new Session
            {
                DayofWeek = "Wednesday",
                StartTime = DateTime.MinValue.AddHours(17),
                EndTime = DateTime.MinValue.AddHours(18),
                IsActive = true,
                Course = course8
            };
            var session5 = new Session
            {
                DayofWeek = "Friday",
                StartTime = DateTime.MinValue.AddHours(9),
                EndTime = DateTime.MinValue.AddHours(10),
                IsActive = true,
                Course = course8
            };

            //course 2 sessions (current term)
            var session6 = new Session
            {
                DayofWeek = "Monday",
                StartTime = DateTime.MinValue.AddHours(11),
                EndTime = DateTime.MinValue.AddHours(12),
                IsActive = true,
                Course = course9
            };
            var session7 = new Session
            {
                DayofWeek = "Monday",
                StartTime = DateTime.MinValue.AddHours(18),
                EndTime = DateTime.MinValue.AddHours(19),
                IsActive = true,
                Course = course9
            };
            var session8 = new Session
            {
                DayofWeek = "Wednesday",
                StartTime = DateTime.MinValue.AddHours(11),
                EndTime = DateTime.MinValue.AddHours(12),
                IsActive = true,
                Course = course9
            };
            var session9 = new Session
            {
                DayofWeek = "Wednesday",
                StartTime = DateTime.MinValue.AddHours(18),
                EndTime = DateTime.MinValue.AddHours(19),
                IsActive = true,
                Course = course9
            };
            var session10 = new Session
            {
                DayofWeek = "Friday",
                StartTime = DateTime.MinValue.AddHours(11),
                EndTime = DateTime.MinValue.AddHours(12),
                IsActive = true,
                Course = course9
            };

            //course 3 sessions
            var session11 = new Session
            {
                DayofWeek = "Monday",
                StartTime = DateTime.MinValue.AddHours(13),
                EndTime = DateTime.MinValue.AddHours(14),
                IsActive = true,
                Course = course10
            }; var session12 = new Session
            {
                DayofWeek = "Monday",
                StartTime = DateTime.MinValue.AddHours(19),
                EndTime = DateTime.MinValue.AddHours(20),
                IsActive = true,
                Course = course10
            };
            var session13 = new Session
            {
                DayofWeek = "Wednesday",
                StartTime = DateTime.MinValue.AddHours(13),
                EndTime = DateTime.MinValue.AddHours(14),
                IsActive = true,
                Course = course10
            }; 
            var session14 = new Session
            {
                DayofWeek = "Wednesday",
                StartTime = DateTime.MinValue.AddHours(19),
                EndTime = DateTime.MinValue.AddHours(20),
                IsActive = true,
                Course = course10
            };
            var session15 = new Session
            {
                DayofWeek = "Monday",
                StartTime = DateTime.MinValue.AddHours(13),
                EndTime = DateTime.MinValue.AddHours(14),
                IsActive = true,
                Course = course10
            };

            //courses 3-6 only have one session per seeded
            //(theoretically our 6 main courses should have 4-6 sessions per
            var session16 = new Session
            {
                DayofWeek = "Monday",
                StartTime = DateTime.MinValue.AddHours(14),
                EndTime = DateTime.MinValue.AddHours(15),
                IsActive = true,
                Course = course10
            };
            var session17 = new Session
            {
                DayofWeek = "Monday",
                StartTime = DateTime.MinValue.AddHours(7),
                EndTime = DateTime.MinValue.AddHours(8),
                IsActive = true,
                Course = course11
            };
            var session18 = new Session
            {
                DayofWeek = "Wednesday",
                StartTime = DateTime.MinValue.AddHours(15),
                EndTime = DateTime.MinValue.AddHours(18),
                IsActive = true,
                Course = course12
            };
            var session19 = new Session
            {
                DayofWeek = "Wednesday",
                StartTime = DateTime.MinValue.AddHours(7),
                EndTime = DateTime.MinValue.AddHours(8),
                IsActive = true,
                Course = course13
            };

            //session for class in non active term
            var session20 = new Session
            {
                DayofWeek = "Friday",
                StartTime = DateTime.MinValue.AddHours(15),
                EndTime = DateTime.MinValue.AddHours(18),
                IsActive = true,
                Course = course
            };

            //public school sessions to be able to block out the schedule
            var session21 = new Session
            {
                DayofWeek = "Friday",
                StartTime = DateTime.MinValue.AddHours(9),
                EndTime = DateTime.MinValue.AddHours(11),
                IsActive = true,
                Course = course14
            };

            await db.AddAsync(session);
            await db.AddAsync(session2);
            await db.AddAsync(session3);
            await db.AddAsync(session4);
            await db.AddAsync(session5);
            await db.AddAsync(session6);
            await db.AddAsync(session7);
            await db.AddAsync(session8);
            await db.AddAsync(session9);
            await db.AddAsync(session10);
            await db.AddAsync(session11);
            await db.AddAsync(session12);
            await db.AddAsync(session13);
            await db.AddAsync(session14);
            await db.AddAsync(session15);
            await db.AddAsync(session16);
            await db.AddAsync(session17);
            await db.AddAsync(session18);
            await db.AddAsync(session19);
            await db.AddAsync(session20);
            await db.AddAsync(session21);


            ////////////grade\\\\\\\\\\\\
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


            ////////////enrollment\\\\\\\\\\\\
            var enrollment = new Enrollment
            {
                Student = student5,
                Session = session,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Ongoing,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment2 = new Enrollment
            {
                Student = student5,
                Session = session2,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment3 = new Enrollment
            {
                Student = student5,
                Session = session3,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment4 = new Enrollment
            {
                Student = student5,
                Session = session4,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment5 = new Enrollment
            {
                Student = student5,
                Session = session5,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment6 = new Enrollment
            {
                Student = student5,
                Session = session6,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment7 = new Enrollment
            {
                Student = student5,
                Session = session7,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment8 = new Enrollment
            {
                Student = student5,
                Session = session8,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment9 = new Enrollment
            {
                Student = student5,
                Session = session9,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment10 = new Enrollment
            {
                Student = student5,
                Session = session10,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment11 = new Enrollment
            {
                Student = student5,
                Session = session11,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment12 = new Enrollment
            {
                Student = student5,
                Session = session12,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment13 = new Enrollment
            {
                Student = student5,
                Session = session13,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment14 = new Enrollment
            {
                Student = student5,
                Session = session14,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment15 = new Enrollment
            {
                Student = student5,
                Session = session15,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment16 = new Enrollment
            {
                Student = student5,
                Session = session16,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment17 = new Enrollment
            {
                Student = student5,
                Session = session17,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment18 = new Enrollment
            {
                Student = student5,
                Session = session18,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment19 = new Enrollment
            {
                Student = student5,
                Session = session19,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            var enrollment21 = new Enrollment
            {
                Student = student5,
                Session = session21,
                EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed,
                FinalGrade = 0,
                Grade = grade
            };
            await db.AddAsync(enrollment);
            await db.AddAsync(enrollment2);
            await db.AddAsync(enrollment3);
            await db.AddAsync(enrollment4);
            await db.AddAsync(enrollment5);
            await db.AddAsync(enrollment6);
            await db.AddAsync(enrollment7);
            await db.AddAsync(enrollment8); 
            await db.AddAsync(enrollment9);
            await db.AddAsync(enrollment10);
            await db.AddAsync(enrollment11);
            await db.AddAsync(enrollment12);
            await db.AddAsync(enrollment13);
            await db.AddAsync(enrollment14);
            await db.AddAsync(enrollment15);
            await db.AddAsync(enrollment16);
            await db.AddAsync(enrollment17);
            await db.AddAsync(enrollment18);
            await db.AddAsync(enrollment19);
            await db.AddAsync(enrollment21);

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

            ////////////assessment (homework)\\\\\\\\\\\\
            //assessments tied to courses in active term
            var assessment = new Assessment
            {
                Score = 0,
                DueDate = null,
                MaxScore = 100,
                //Grade = grade,
                Course = course8,
                Title = "HW 1",
                Description = "Introduce Yourself"
            };
            var assessment2 = new Assessment
            {
                Score = 0,
                DueDate = null,
                MaxScore = 100,
                //Grade = grade,
                Course = course8,
                Title = "HW 2",
                Description = "Syllabus Quiz"
            };
            var assessment3 = new Assessment
            {
                Score = 0,
                DueDate = null,
                MaxScore = 100,
                //Grade = grade2,
                Course = course9,
                Title = "HW 3",
                Description = "Short Essay"
            };
            var assessment4 = new Assessment
            {
                Score = 0,
                DueDate = null,
                MaxScore = 100,
                //Grade = grade2,
                Course = course10,
                Title = "HW 4",
                Description = "Medium Essay"
            };
            var assessment5 = new Assessment
            {
                Score = 0,
                DueDate = null,
                MaxScore = 100,
                //Grade = grade2,
                Course = course11,
                Title = "Quiz 1",
                Description = "Pop Quiz on Adjectives"
            };
            var assessment6 = new Assessment
            {
                Score = 0,
                DueDate = null,
                MaxScore = 100,
                //Grade = grade2,
                Course = course13,
                Title = "HW 8",
                Description = "How to Diagram"
            };

            //assessments tied to courses in non-active term
            //(shouldnt see this unless you change active term to previous one)
            var assessment7 = new Assessment
            {
                Score = 0,
                DueDate = null,
                MaxScore = 100,
                //Grade = grade2,
                Course = course,
                Title = "ENG-1 Final",
                Description = "Final Assessment"
            };
            await db.AddAsync(assessment);
            await db.AddAsync(assessment2);
            await db.AddAsync(assessment3);
            await db.AddAsync(assessment4);
            await db.AddAsync(assessment5);
            await db.AddAsync(assessment6);
            await db.AddAsync(assessment7);

            ////////////criterion\\\\\\\\\\\\
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

            ////////////accesstype\\\\\\\\\\\\
            var accesstype = new AccessType
            {
                Accesss = AccessType.Type.Rater
            };
            await db.AddAsync(accesstype);

            ////////////student note\\\\\\\\\\\\
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

            ////////////student doc\\\\\\\\\\\\
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
            await db.AddAsync(studentdoc);
            await db.AddAsync(studentdoc2);

            await db.SaveChangesAsync();
        }
    }
}