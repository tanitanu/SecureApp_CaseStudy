﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UnSecureApp.Models;

namespace UnSecureApp.Models;

public partial class SecureAppContext : DbContext
{
    public SecureAppContext()
    {
    }

    public SecureAppContext(DbContextOptions<SecureAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserProfile> UserProfile { get; set; }
    public virtual DbSet<ChangePassword> ChangePassword { get; set; }


}