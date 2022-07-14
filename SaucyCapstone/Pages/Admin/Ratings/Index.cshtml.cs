using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Ratings;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Rating> Rating { get;set; } = default!;
    public IList<Student> Student { get; set; } = default!;
    public IList<Criterion> Criterion { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Ratings != null)
        {
            Rating = await _context.Ratings.ToListAsync();
            Student = await _context.Students.Include(x => x.Ratings).ThenInclude(x => x.Criterion).ToListAsync();
            Criterion = await _context.Criteria.ToListAsync();
        }
        if (!_context.Students.Any())
        {


        }
    }
    //public async Task OnPostAsync(){

    //}
}
