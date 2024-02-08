using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechCo.Data;
using TechCo.Models;

namespace TechCo.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly TechCo.Data.ApplicationDbContext _context;

        public DetailsModel(TechCo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }
        public List<Product> Recommended { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product
                .Include(p => p.Category).FirstOrDefaultAsync(m => m.ID == id);
            Recommended = await _context.Product.Include(p => p.Category)
                .Where(p => p.Category == Product.Category && p.ID != Product.ID).OrderBy(p=>p.AddedDate).Take(3).ToListAsync();

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
