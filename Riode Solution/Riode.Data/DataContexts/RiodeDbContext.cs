using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Riode.Data.Entities;
using Riode.Data.Entities.Membership;

namespace Riode.Data.DataContexts
{
	public class RiodeDbContext : IdentityDbContext<
		RiodeUser, 
		RiodeRole, 
		int, 
		RiodeUserClaim,
		RiodeUserRole,
		RiodeUserLogin,
		RiodeRoleClaim, 
		RiodeUserToken>
	{
		public RiodeDbContext(DbContextOptions options)
			:base(options)
		{

		}
		public DbSet<Subscribe> Subscribes { get; set; } 
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ProductSize> Sizes { get; set; }
		public DbSet<ProductColor> Colors { get; set; }
		public DbSet<Faq> Faqs { get; set; }

		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Specification> Specifications { get; set; }
		public DbSet<PostTag> PostTags { get; set; }
		public DbSet<BlogPostTag> BlogPostTags { get; set; }
		public DbSet<ContactComment> ContactComments { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<ProductSpecification> ProductSpecifications { get; set; }
		public DbSet<ProductPricing> ProductPricing { get; set; }




		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<RiodeUser>(e =>  //change table's name
			{
				e.ToTable("Users", "Membership"); 
			});

			modelBuilder.Entity<RiodeRole>(e =>  //change table's name
			{
				e.ToTable("Roles", "Membership"); 
			});

			modelBuilder.Entity<RiodeUserRole>(e =>  //change table's name
			{
				e.ToTable("UserRoles", "Membership"); 
			});

			modelBuilder.Entity<RiodeUserClaim>(e =>  //change table's name
			{
				e.ToTable("UserClaims", "Membership"); 
			});
			
			modelBuilder.Entity<RiodeRoleClaim>(e =>  //change table's name
			{
				e.ToTable("RoleClaims", "Membership"); 
			});

			modelBuilder.Entity<RiodeUserLogin>(e =>  //change table's name
			{
				e.ToTable("UserLogins", "Membership"); 
			});

			modelBuilder.Entity<RiodeUserToken>(e =>  //change table's name
			{
				e.ToTable("UserTokens", "Membership"); 
			});

			modelBuilder.Entity<BlogPostTag>(e =>
			{
				e.HasKey(k => new
				{
					k.BlogPostId,
					k.PostTagId
				}); 

			});
			modelBuilder.Entity<ProductSpecification>(e =>
			{
				e.HasKey(k => new
				{
					k.ProductId,
					k.SpecificationId
				}); 

			});
			modelBuilder.Entity<ProductPricing>(e =>
			{
				e.HasKey(k => new
				{
					k.ProductId,
					k.ColorId,
					k.SizeId
				}); 

			});
		}

	}
}
