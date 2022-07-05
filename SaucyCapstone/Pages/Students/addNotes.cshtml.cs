using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students
{
    public class addNotesModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Note studentNote;

        public addNotesModel(ApplicationDbContext db)
        {
            _db = db;
            studentNote = new Note();
        }


    }
}
