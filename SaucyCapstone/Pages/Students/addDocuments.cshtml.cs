using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students
{
    public class addDocumentsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public StudentDoc studentDoc;

        public addDocumentsModel(ApplicationDbContext db)
        {
            _db = db;
            studentDoc = new StudentDoc();
        }

        public void OnGet()
        {
        }
    }
}
