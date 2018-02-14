using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Godius.Shop.Models;

namespace Godius.Shop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public DbSet<Item> Items { get; set; }

		public DbSet<GoodsCategory> GoodsCategories { get; set; }

		public DbSet<Goods> Goods { get; set; }

		public DbSet<ItemGoods> ItemGoods { get; set; }

		public DbSet<ResultItemGoods> ResultItemGoods { get; set; }

		public DbSet<Purchase> PurchaseHistory { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);

			//builder.Entity<Goods>()
			//	   .HasMany(G => G.ItemsGoods)
			//	   .WithOne
   //.WithMany()
   //.OnDelete(DeleteBehavior.Restrict)
		}
	}
}
