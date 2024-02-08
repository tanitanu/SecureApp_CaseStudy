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
    public class DetailsModel : PageModel
    {
        private readonly TechCo.Data.ApplicationDbContext _context;

        public DetailsModel(TechCo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order
                .Include(o => o.Client).FirstOrDefaultAsync(m => m.ID == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
