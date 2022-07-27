using Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SaucyCapstone.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SaucyCapstone.Static;

namespace SaucyCapstone.Pages.Instructor.Students;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public List<Student> Students { get; set; }
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger)
    {
        _logger = logger;
        _db = db;
        Students = new List<Student>();
    }
    public async Task<ActionResult> OnGetAsync()
    {
        string userId = User.UserId();

        Students = await _db.Students.Where(u => u.Status == Student.StudentStatus.Active && u.IsActive == true).ToListAsync();
        _logger.LogDebug("Testing serilog");
        return Page();
    }

}

