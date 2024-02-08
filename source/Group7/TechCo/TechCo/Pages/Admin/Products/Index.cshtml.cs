using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechCo.Data;
using TechCo.Models;
using Microsoft.AspNetCore.Authorization;

namespace TechCo.Pages.Admin.Products
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly TechCo.Data.ApplicationDbContext _context;

        public IndexModel(TechCo.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public readonly int EPP = 12;
        public IList<Product> Product { get;set; }
        [BindProperty(SupportsGet = true)]
        public int Currentpage { get; set; } = 0;

        public async Task OnGetAsync()
        {
            Product = await _context.Product
                .Include(p => p.Category).OrderByDescending(p => p.AddedDate).Skip(EPP*Currentpage).Take(EPP).ToListAsync();
        }
    }
}
