using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechCo.Data;
using TechCo.Models;

namespace TechCo.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly TechCo.Data.ApplicationDbContext _context;

        public IndexModel(TechCo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Order
                .Include(o => o.Client).ToListAsync();
        }
    }
}
