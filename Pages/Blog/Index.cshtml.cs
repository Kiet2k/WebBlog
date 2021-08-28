using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razorweb.Models;

namespace razorweb.Pages_Blog
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly razorweb.Models.MyblogContext _context;

        public IndexModel(razorweb.Models.MyblogContext context)
        {
            _context = context;
        }

        public IList<Acticle> Acticle { get;set; }
        public const int itempage =5;
        [BindProperty(SupportsGet =true,Name ="p")]
        public int currentPage{get;set;}
        public int countPage{get;set;} 

        public async Task OnGetAsync(String SearchString)
        {
            int totalActicle = await _context.articles.CountAsync();
            countPage = (int)Math.Ceiling((double)totalActicle/itempage);
            if(currentPage<1){
                currentPage =1;
            }
            if(currentPage>countPage){
                currentPage=countPage;
            }

            // Acticle = await _context.articles.ToListAsync();
            var qr = (from p in _context.articles
                    orderby p.PublishDate descending
                    select p).Skip((currentPage - 1)*itempage)
                    .Take(itempage);
            if(!string.IsNullOrEmpty(SearchString)){
               Acticle = qr.Where(a=>a.Title.Contains(SearchString)).ToList();

            }
            else{
                Acticle = await qr.ToListAsync();
            }
            
        }
    }
}
