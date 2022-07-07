using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class AccessType
{
    public int AccessTypeId { get; set; }

    [Required]
    [EnumDataType(typeof(Type))]
    public Type Accesss { get; set; }
    public IList<Note> Notes { get; set; }
    public IList<StudentDoc> StudentDocs { get; set; }

    public enum Type
    {
        Rater,
        Instructor,
        SocialWorker,
        Admin
    }
}
