using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TechCo.Models;

namespace TechCo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TechCo.Models.Category> Category { get; set; }
        public DbSet<TechCo.Models.Product> Product { get; set; }
        public DbSet<TechCo.Models.Order> Order { get; set; }
    }
}
