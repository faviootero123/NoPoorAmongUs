using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions.Assessments
{
    public class IndexModel : PageModel
    {
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public IndexModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Assessment> Assessment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Assessments != null)
            {
                Assessment = await _context.Assessments.ToListAsync();
            }
        }
    }
}
