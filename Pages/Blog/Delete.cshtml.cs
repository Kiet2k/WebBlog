using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razorweb.Models;

namespace razorweb.Pages_Blog
{
    public class DeleteModel : PageModel
    {
        private readonly razorweb.Models.MyblogContext _context;

        public DeleteModel(razorweb.Models.MyblogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Acticle Acticle { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Acticle = await _context.articles.FirstOrDefaultAsync(m => m.ID == id);

            if (Acticle == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Acticle = await _context.articles.FindAsync(id);

            if (Acticle != null)
            {
                _context.articles.Remove(Acticle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
