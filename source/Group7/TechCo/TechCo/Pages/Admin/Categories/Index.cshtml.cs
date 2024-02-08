﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechCo.Data;
using TechCo.Models;
using Microsoft.AspNetCore.Authorization;

namespace TechCo.Pages.Admin.Categories
{
    [Authorize(Roles="Admin")]
    public class IndexModel : PageModel
    {
        private readonly TechCo.Data.ApplicationDbContext _context;

        public IndexModel(TechCo.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Category> Categs;

        public IActionResult OnGet()
        {
            Categs = _context.Category.ToList(); 
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
