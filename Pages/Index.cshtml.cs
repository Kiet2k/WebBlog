using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using razorweb.Models;

namespace razorweb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyblogContext _myblogContext;
        public IndexModel(ILogger<IndexModel> logger, MyblogContext myblogContext)
        {
            _logger = logger;
            _myblogContext = myblogContext;
        }

        public void OnGet()
        {
            
            var pots = (from p in _myblogContext.articles
                        orderby p.PublishDate descending
                        select p).ToList();
            ViewData["Posts"]= pots;
        }
    }
}
