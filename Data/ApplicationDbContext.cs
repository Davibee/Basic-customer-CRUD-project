using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using V3.Models;
namespace V3.Data
{
	public class ApplicationDbContext:IdentityDbContext
	{
		public DbSet<Customer> Customers { get; set; }
		public DbSet<MembershipType>  MembershipTypes { get; set; }
		public DbSet <Movie> Movies { get; set; }


		public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
			: base (options)
		{
			
		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=app.db");
            }
        }

        public ApplicationDbContext()
        {
        }
    }
}

