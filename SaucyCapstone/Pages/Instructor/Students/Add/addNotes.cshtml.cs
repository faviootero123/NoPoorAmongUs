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
public class addNotesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public int StudentId { get; set; }
    [BindProperty]
    public Note? Note { get; set; }
    [BindProperty]
    public AccessType.Type SelectedRole { get; set; }
    public List<SelectListItem> RolesOfUser;

    public addNotesModel(ApplicationDbContext db)
    {
        _db = db;
        RolesOfUser = new List<SelectListItem>();
    }

    public ActionResult OnGetAsync(int studentId)
    {
        foreach (var Roles in User.AllRoles())
        {
            RolesOfUser.Add(new SelectListItem { Text = Roles, Value = Roles });
        }
        StudentId = studentId;
        return Page();     
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        string userId = User.UserId();

        var newNote = new Note()
        {
        Topic = Note.Topic,
        Content = Note.Content,
        CreatedDate = DateTime.Now,
        Student = await _db.Students.Where(d=>d.StudentId == id).FirstAsync() ?? new Student(),
        FacultyMember = await _db.ApplicationUsers.Where(d=>d.Id == userId).FirstAsync() ?? new ApplicationUser(),
        NoteType = await _db.AccessTypes.Where(d=>d.Accesss == SelectedRole).FirstAsync() ?? new AccessType(),
        isPrivate = Note.isPrivate,
        EditedDate = DateTime.Now
        };

        await _db.AddAsync(newNote);
        await _db.SaveChangesAsync();
        return RedirectToPage("../StudentNotes", new { id = id });
    }

}
