using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels;
public class NoteVM
{
    public int StudentId { get; set; }
    public int StudentNoteId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;

    public bool isPrivate { get; set; }
}
