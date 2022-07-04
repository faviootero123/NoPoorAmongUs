using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Courses;

public class IndexModel : PageModel
{
    private readonly SaucyCapstone.Data.ApplicationDbContext _context;

    public IndexModel(SaucyCapstone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Course> Course { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Courses != null)
        {
            Course = await _context.Courses.ToListAsync();
        }
    }
}
