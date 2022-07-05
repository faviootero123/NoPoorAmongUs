using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class NoteType
{
    public int NoteTypeId { get; set; }

    public IList<Note> Notes { get; set; }

    [Required]
    public string Type { get; set; }


}
