using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students
{
    public class addNotesModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public int StudentId { get; set; }
        public Note studentNote { get; set; }

        public addNotesModel(ApplicationDbContext db)
        {
            _db = db;
            studentNote = new Note(); 
        }

        public async Task OnGetAsync(int id)
        {
            StudentId = id;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //await _db.AddAsync(studentNote);
            //await _db.SaveChangesAsync();

            return RedirectToPage("./StudentNotes");
        }

    }
}

//if (ModelState.IsValid)
//{
//    var newNote = new Note()
//    {
//        Topic = note.Topic,
//        Content = note.Content,
//        CreatedDate = DateTime.Now,
//        Student = _db.Students.Where(d => d.StudentId == StudentId).FirstOrDefault(),
//        FacultyMember = _db.FacultyMembers.FirstOrDefault(),
//        NoteType = _db.AccessTypes.FirstOrDefault(),
//    };
//    return RedirectToPage("./StudentNotes");
//}