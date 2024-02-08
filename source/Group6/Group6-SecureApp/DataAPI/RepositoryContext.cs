using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;

namespace DataAPI
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<User>? Users { get; set; }
        public DbSet<UserProfile>? UserProfiles { get; set; }
    }
}
