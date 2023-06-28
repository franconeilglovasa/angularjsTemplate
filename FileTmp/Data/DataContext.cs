using System;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using FileTmp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FileTmp.Data
{
	public class DataContext: IdentityDbContext<TestUser>
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        //public DbSet<TestUser> TestUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}

