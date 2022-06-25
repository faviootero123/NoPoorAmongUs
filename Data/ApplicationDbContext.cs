using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SaucyCapstone.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    public DbSet<Assessment> Assessments => Set<Assessment>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseEnrollment> CourseEnrollments => Set<CourseEnrollment>();
    public DbSet<CourseSession> CourseSessions => Set<CourseSession>();
    public DbSet<Criterion> Criteria => Set<Criterion>();
    public DbSet<DocType> DocTypes => Set<DocType>();
    public DbSet<Grade> Grades => Set<Grade>();
    public DbSet<Guardian> Guardians => Set<Guardian>();
    public DbSet<NoteType> NoteTypes => Set<NoteType>();
    public DbSet<School> Schools => Set<School>();
    public DbSet<Staff> StaffMembers => Set<Staff>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentDoc> StudentDocs => Set<StudentDoc>();
    public DbSet<StudentNote> StudentNotes => Set<StudentNote>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Term> Terms => Set<Term>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityUser>().Property(x => x.Id).HasMaxLength(225);
        modelBuilder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(225);
        modelBuilder.Entity<IdentityUserLogin<string>>().Property(x => x.ProviderKey).HasMaxLength(225);
        modelBuilder.Entity<IdentityUserLogin<string>>().Property(x => x.LoginProvider).HasMaxLength(225);
        modelBuilder.Entity<IdentityUserToken<string>>().Property(x => x.Name).HasMaxLength(112);
        modelBuilder.Entity<IdentityUserToken<string>>().Property(x => x.LoginProvider).HasMaxLength(112);
    }
}
