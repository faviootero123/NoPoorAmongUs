using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students.Add
{
    public class addDocumentsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public int StudentId { get; set; }
        [BindProperty]
        public StudentDoc StudentDocs { get; set; }

        public addDocumentsModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            StudentDocs = new StudentDoc();
            _hostEnvironment = hostEnvironment;
        }

        public async Task OnGetAsync(int id)
        {
            StudentId = id;
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile? file)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string ex = "";
            string guid = "";
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"documents\");
                var extension = Path.GetExtension(file.FileName);
                if (StudentDocs.Path != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, StudentDocs.Path.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                StudentDocs.Path = @"\documents\" + fileName + extension;
                ex = extension;
                guid = fileName;
            }

            var newDoc = new StudentDoc()
            {
                Student = _db.Students.Where(d => d.StudentId == id).FirstOrDefault(),
                AccessType = _db.AccessTypes.FirstOrDefault(),
                Name = StudentDocs.Name,
                Description = StudentDocs.Description,
                Path = StudentDocs.Path,
                FileGUID = guid,
                Extension = ex,       
                UploadDate = DateTime.Now
            };
            await _db.AddAsync(newDoc);
            await _db.SaveChangesAsync();
            return RedirectToPage("../StudentDocuments", new { id = id });
        }
    }
}
