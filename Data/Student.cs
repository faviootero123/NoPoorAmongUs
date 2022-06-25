using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Student
    {
        #region Properties
        public int StudentId { get; set; }

        public string? Picture { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? Phone { get; set; }

        [Required]
        public DateTime AcceptedDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsStudent { get; set; }

        public string? Address { get; set; }

        public string? Village { get; set; }

        [Range(-90, 90)]
        [Column(TypeName = "decimal(18, 15)")]
        public Decimal? Latitude { get; set; }

        [Range(-90, 90)]
        [Column(TypeName = "decimal(18, 15)")]
        public Decimal? Longitude { get; set; }

        public string? AnnualIncome { get; set; }

        //1 = 9th grade | 2 = 10th grade | 3 = 11th grade | 4 - 12th grade
        [Range(1, 4)]
        public int SchoolLevel { get; set; }

        public bool FoodAssistance { get; set; }

        public bool ChappaAssistance { get; set; }

        //1 = Low | 2 = Above Low | 3 = Medium | 4 = Below High | 5 = High
        [Range(1, 5)]
        public int Determination { get; set; }

        public string? NotesAndAbout { get; set; }
        #endregion

        #region Relationships
        //Many Students to one Guardian
        [ForeignKey("GuardianId")]
        public Guardian Guardian { get; set; }
        public int GuardianId { get; set; }

        //One Student to many StudentDocs
        public IEnumerable<StudentDoc> StudentDocs { get; set; }

        //One Student to many StudentNotes
        public IEnumerable<StudentNote> StudentNotes { get; set; }

        //One Student to many Criteria
        public IEnumerable<Criterion> Criteria { get; set; }

        //One Student to many CourseEnrollments
        public IEnumerable<CourseEnrollment>? CourseEnrollments { get; set; }
        #endregion
    }
}
