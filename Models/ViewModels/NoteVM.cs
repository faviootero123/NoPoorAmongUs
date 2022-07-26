using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Note;

namespace Models.ViewModels;
public class NoteVM
{
    public int StudentId { get; set; }
    public int StudentNoteId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;

    public bool isPrivate { get; set; }

    public NoteLevel noteLevel { get; set; }
    public Data.AccessType.Type type { get; set; }
}
