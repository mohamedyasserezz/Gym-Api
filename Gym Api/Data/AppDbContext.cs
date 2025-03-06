using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Exercise>()
				.HasOne(e => e.Category)
				.WithMany(c => c.Exercises)
				.HasForeignKey(e => e.Category_ID)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Assignment>()
			.HasOne(a => a.User)
			.WithMany(u => u.Assignments)
			.HasForeignKey(a => a.User_ID);

			modelBuilder.Entity<Assignment>()
				.HasOne(a => a.Coach)
				.WithMany(c => c.Assignments)
				.HasForeignKey(a => a.Coach_ID);

			modelBuilder.Entity<Assignment>()
				.HasOne(a => a.Exercise)
				.WithMany(e => e.Assignments)
				.HasForeignKey(a => a.Exercise_ID);

			modelBuilder.Entity<Subscribe>()
			.HasOne(s => s.User)
			.WithMany(u => u.Subscriptions)
			.HasForeignKey(s => s.User_ID);

			modelBuilder.Entity<Subscribe>()
				.HasOne(s => s.Coach)
				.WithMany(c => c.Subscriptions)
				.HasForeignKey(s => s.Coach_ID);

			modelBuilder.Entity<NutritionPlan>()
			.HasOne(np => np.Coach)
			.WithMany(c => c.NutritionPlans)
			.HasForeignKey(np => np.Coach_ID)
			.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<NutritionPlan>()
			.HasOne(np => np.User)
			.WithOne(u => u.NutritionPlan)
			.HasForeignKey<NutritionPlan>(np => np.User_ID)
			.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Order>()
			.HasOne(o => o.User)
			.WithMany(u => u.Orders)
			.HasForeignKey(o => o.User_ID)
			.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<OrderItem>()
			.HasOne(oi => oi.Order)
			.WithMany(o => o.OrderItems)
			.HasForeignKey(oi => oi.Order_ID);

			modelBuilder.Entity<OrderItem>()
			.HasOne(oi => oi.Product)
			.WithMany(p => p.OrderItems)
			.HasForeignKey(oi => oi.Product_ID);
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Exercise> Exercises { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Coach> Coaches { get; set; }
		public DbSet<Assignment> Assignments { get; set; }
		public DbSet<Subscribe> Subscriptions { get; set; }
		public DbSet<NutritionPlan> NutritionPlans { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }



	}
}
