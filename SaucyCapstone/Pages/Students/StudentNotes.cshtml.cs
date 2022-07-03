using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students
{
    public class StudentNotesModel : PageModel
    {
        private readonly ApplicationDbContext _db;
 
        public StudentNotesModel(ApplicationDbContext db)
        {
            _db = db;
        }   
       
        
    }   
}
