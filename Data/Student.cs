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
        public enum Determination
        {
            Low,
            AboveLow,
            Middle,
            BelowHigh,
            High
        }

        public int StudentId { get; set; }

        public string? Picture { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? Phone { get; set; }

        [Required]
        public DateTime AcceptedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public StudentStatus StudentStatus { get; set; }

        public string? Address { get; set; }

        public string? Village { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal AnnualIncome { get; set; }

        public char SchoolLevel { get; set; }

        public bool FoodAssistance { get; set; }

        public bool ChappaAssistance { get; set; }

        public Determination DeterminationLevel { get; set; }

        public string? NotesAndAbout { get; set; }
    }
}
