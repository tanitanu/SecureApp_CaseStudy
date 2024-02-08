﻿using TechCo.Data;
using TechCo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechCo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext db;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public List<Product> LastProds;
        public void OnGet()
        {
            LastProds = db.Product.OrderByDescending(c=>c.AddedDate).Take(8).ToList();
        }
    }
}
