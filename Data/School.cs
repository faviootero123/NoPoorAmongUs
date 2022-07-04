using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class School
{
    public int SchoolId { get; set; }

<<<<<<< HEAD
=======
    public IList<Subject>? Subjects { get; set; }

>>>>>>> f5457e00d29cc68b16b789f1e370a79ca7e54763
    [Required]
    public string SchoolName { get; set; }
}
