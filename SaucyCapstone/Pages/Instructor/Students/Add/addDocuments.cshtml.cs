using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;
using SaucyCapstone.Static;
using System.Security.Claims;

namespace SaucyCapstone.Pages.Instructor.Students.Add;

[Authorize]
public class addDocumentsModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;

    [BindProperty]
    public int StudentId { get; set; }
    [BindProperty]
    public StudentDoc? StudentDocs { get; set; }
    [BindProperty]
    public AccessType.Type SelectedRole { get; set; }
    public List<SelectListItem> RolesOfUser;

    public addDocumentsModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
    {
        _db = db;
        StudentDocs = new StudentDoc();
        RolesOfUser = new List<SelectListItem>();
        _hostEnvironment = hostEnvironment;
    }
    public async Task<ActionResult> OnGetAsync(int id)
    {
        foreach (var Roles in User.AllRoles())
        {
            RolesOfUser.Add(new SelectListItem { Text = Roles, Value = Roles });
        }
        StudentId = id;
        return Page(); 
    }

    public async Task<IActionResult> OnPostAsync(int id, IFormFile? file)
    {
        string wwwRootPath = _hostEnvironment.WebRootPath;
        string ex = "";
        string guid = "";
        if (file != null)
        {
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRootPath, "documents");
 
            if(!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            
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
            Student = await _db.Students.Where(d => d.StudentId == id).FirstAsync(),
            AccessType = await _db.AccessTypes.Where(d => d.Accesss == SelectedRole).FirstAsync(),
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
