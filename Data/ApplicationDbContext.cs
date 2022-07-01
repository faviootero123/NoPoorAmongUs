using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SaucyCapstone.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Assessment> Assessments => Set<Assessment>();
    public DbSet<AssessmentGrade> AssessmentGrades => Set<AssessmentGrade>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Criterion> Criteria => Set<Criterion>();
    public DbSet<DocType> DocTypes => Set<DocType>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<FacultyMember> FacultyMembers => Set<FacultyMember>();
    public DbSet<Guardian> Guardians => Set<Guardian>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<NoteType> NoteTypes => Set<NoteType>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<School> Schools => Set<School>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<SessionAssessment> SessionAssessments => Set<SessionAssessment>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentDoc> StudentDocs => Set<StudentDoc>();
    public DbSet<StudentGuardian> StudentGuardians => Set<StudentGuardian>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Term> Terms => Set<Term>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityUser>().Property(x => x.Id).HasMaxLength(225);
        modelBuilder.Entity<ApplicationRole>().Property(x => x.Id).HasMaxLength(225);
        modelBuilder.Entity<IdentityUserLogin<string>>().Property(x => x.ProviderKey).HasMaxLength(225);
        modelBuilder.Entity<IdentityUserLogin<string>>().Property(x => x.LoginProvider).HasMaxLength(225);
        modelBuilder.Entity<IdentityUserToken<string>>().Property(x => x.Name).HasMaxLength(112);
        modelBuilder.Entity<IdentityUserToken<string>>().Property(x => x.LoginProvider).HasMaxLength(112);
    }
}
