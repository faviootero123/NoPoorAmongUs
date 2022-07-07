using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;
using System;
using System.IO;

namespace SaucyCapstone.Pages.Students.Delete
{
    public class deleteDocumentsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public StudentDoc Doc { get; set; }

        public deleteDocumentsModel(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _hostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _db.StudentDocs == null)
            {
                return NotFound();
            }

            var doc = await _db.StudentDocs.Include(d => d.Student).FirstOrDefaultAsync(m => m.StudentDocId == id);

            if (doc == null)
            {
                return NotFound();
            }
            else
            {
                Doc = doc;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _db.StudentDocs == null)
            {
                return NotFound();
            }
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var getStudentId = await _db.StudentDocs.Include(d => d.Student).FirstOrDefaultAsync(m => m.StudentDocId == id);
            var doc = await _db.StudentDocs.FindAsync(id);

            if (doc != null)
            {
                Doc = doc;
                _db.StudentDocs.Remove(Doc);
                await _db.SaveChangesAsync();

                var path = Path.Combine(wwwRootPath, doc.Path.TrimStart('\\'));

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

            }

            return RedirectToPage("../StudentDocuments", new { id = getStudentId.Student.StudentId });
        }
    }
}
