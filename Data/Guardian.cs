using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Guardian
{
        public int GuardianId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }  
        
        [Required]
        public string Relation { get; set; }

        [Required]
        public string ContactInfo { get; set; }   
        
        public IList<StudentGuardian> Guardians { get; set; }
}
