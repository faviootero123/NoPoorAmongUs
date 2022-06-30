using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Student
{
    //Properties
    public int StudentId { get; set; }
    //public string FirstName { get; set; } = string.Empty;
    //public string LastName { get; set; } = string.Empty;
    //public DateTime AcceptedDate { get; set; }
    //public DateTime ModifiedDate { get; set; }
    //public DateTime LastModifiedDate { get; set; }

    //Relationships
    //[ForeignKey("ApplicantId")]
    public int ApplicantId { get; set; }
    public Applicant ApplicationInformation { get; set; }
    public IEnumerable<StudentNote> Notes { get; set; }
    public IEnumerable<StudentDoc> Docs { get; set; }
    public IEnumerable<ClassEnrollment> ClassEnrollments { get; set; }
}
