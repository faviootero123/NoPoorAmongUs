using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Student
{
    public int StudentId { get; set; }

    public string? Picture { get; set; }

    [Required]
    [Display(Name = "Student Name")]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    public string? Phone { get; set; }

    public DateTime? AcceptedDate { get; set; }

    [Required]
    public DateTime LastModifiedDate { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public StudentStatus Status { get; set; }

    public string? Address { get; set; }

    public string? Village { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal AnnualIncome { get; set; }

    [Required]
    public int SchoolLevel { get; set; }

    [Required]
    public bool FoodAssistance { get; set; }

    [Required]
    public bool ChappaAssistance { get; set; }

    [Required]
    [EnumDataType(typeof(DeterminationLevel))]
    public DeterminationLevel Determination { get; set; }

    public string? NotesAndAbout { get; set; }

    public IList<Enrollment>? Enrollments { get; set; }
    public IList<StudentGuardian>? StudentGuardians { get; set; }
    public IList<StudentDoc>? StudentDocs { get; set; }
    public IList<Rating>? Ratings { get; set; }
    public IList<Note>? Notes { get; set; }

    public enum DeterminationLevel
    {
        Low,
        AboveLow,
        Middle,
        BelowHigh,
        High
    }
    public enum StudentStatus
    {
        OpenApplication,
        Denied,
        Active,
        Graduated
    }
}
