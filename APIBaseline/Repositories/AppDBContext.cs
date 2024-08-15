using APIBaseline.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBaseline.Repositories
{
	public class AppDBContext : DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configuring User entity
			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable("User");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
				// Configure other properties and relationships
			});

			// Configuring UserRole entity
			modelBuilder.Entity<UserRole>(entity =>
			{
				entity.ToTable("UserRole");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.RoleName).IsRequired().HasMaxLength(100);
				// Configure other properties and relationships
			});

			// Configure relationships
			modelBuilder.Entity<User>()
				.HasMany(u => u.UserRoles)
				.WithOne()
				.HasForeignKey(ur => ur.UserId);
		}
	}
}
