using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using static Data.Student;

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
                Student = await _context.Students.Include(x => x.Ratings).ThenInclude(x => x.Criterion).ToListAsync();
                Criterion = await _context.Criteria.ToListAsync();
            }
            if (!_context.Students.Any())
            {
                if (!_context.Criteria.Any())
                {
                    var criteria = new List<Criterion> {
                        new Criterion
                        {
                            Description = "Academics",
                            Weight = .25m
                        },
                        new Criterion
                        {
                            Description = "Age",
                            Weight = .05m
                        },
                        new Criterion
                        {
                            Description = "Distance",
                            Weight = .05m
                        },
                        new Criterion
                        {
                            Description = "Family support and interest",
                            Weight = .4m
                        },
                        new Criterion
                        {
                            Description = "Finanaces",
                            Weight = .25m
                        }
                    };
                    _context.AddRange(criteria);
                    _context.SaveChanges();
                }
                var students = new List<Student>
                {
                    new Student
                    {
                        FirstName = "Karl",
                        LastName = "Guy",
                        DateOfBirth = new DateTime(1990, 7, 7),
                        AcceptedDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now,
                        IsActive = true,
                        Status = StudentStatus.OpenApplication,
                        SchoolLevel = '1',
                        FoodAssistance = false,
                        ChappaAssistance = true,
                        Determination = DeterminationLevel.Middle
                    },
                };
                _context.AddRange(students);
                _context.SaveChanges();
            }
        }
        //public async Task OnPostAsync(){

        //}
    }
}
