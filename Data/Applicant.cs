using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Applicant
{
    //Properties 
    public int ApplicantId { get; set; }
    //public string FirstName { get; set; }
    //public string LastName { get; set; }
    //public int Age { get; set; }
    //public DateTime BirthDate { get; set; }
    
    //RelationShips
    public Student? Student { get; set; }
    public IEnumerable<Rating> Ratings { get; set; }
    public IEnumerable<Guardian> Guardians { get; set; }
}
