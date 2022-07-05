using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Ratings
{
    public class IndexModel : PageModel
    {
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public IndexModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // public IList<Rating> Rating { get;set; } = default!;
        public IList<Student> Student { get; set; } = default!;
        public IList<Criterion> Criterion { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Ratings != null)
            {
                //Rating = await _context.Ratings.ToListAsync();
                Student = await _context.Students.ToListAsync();
                Criterion = await _context.Criteria.ToListAsync();
            }
            if (!_context.Students.Any())
            {
                if (!_context.Criteria.Any())
                {
                    var criteria = new List<Criterion> { 
                        new Criterion { Description = "Academics", Weight = new decimal(.25) },
                        new Criterion { Description = "Age", Weight = new decimal(.05) },
                        new Criterion { Description = "Distance", Weight = new decimal(.05) },
                        new Criterion { Description = "Family support and interest", Weight = new decimal(.4) },
                        new Criterion { Description = "Finanaces", Weight = new decimal(.25) }
                    };
                    _context.AddRange(criteria);
                    _context.SaveChanges();
                }
                var students = new List<Student>
                {
                    new Student { FirstName = "Karl", LastName = "Guy", DateOfBirth = new DateTime(1990, 7, 7), AcceptedDate = DateTime.Now, LastModifiedDate = DateTime.Now, IsActive = true, Status = new Student.StudentStatus(), SchoolLevel = '1',  FoodAssistance = false, ChappaAssistance = true, Determination = new Student.DeterminationLevel() },
                };
                _context.AddRange(students);
                _context.SaveChanges();
            }
        }
        //public async Task OnPostAsync(){
          
        //}
    }
}
