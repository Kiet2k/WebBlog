using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using razorweb.Models;

namespace razorweb.Pages_Blog
{
    public class CreateModel : PageModel
    {
        private readonly razorweb.Models.MyblogContext _context;

        public CreateModel(razorweb.Models.MyblogContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Acticle Acticle { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.articles.Add(Acticle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
