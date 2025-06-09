using Gym_Api.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gym_Api.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : 
		IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
	{
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			

			modelBuilder.Entity<Subscribe>()
			.HasOne(s => s.User)
			.WithMany(u => u.Subscriptions)
			.HasForeignKey(s => s.User_ID)
			.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Subscribe>()
				.HasOne(s => s.Coach)
				.WithMany(c => c.Subscriptions)
				.HasForeignKey(s => s.Coach_ID)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<NutritionPlan>()
			.HasOne(n => n.coach)
			.WithMany(c => c.NutritionPlans)
			.HasForeignKey(np => np.Coach_ID)
			.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<NutritionPlan>()
	        .HasOne(np => np.user)
	        .WithMany(u => u.NutritionPlans)
	        .HasForeignKey(np => np.User_ID)
	        .OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<Order>()
			.HasOne(o => o.User)
			.WithMany(u => u.Orders)
			.HasForeignKey(o => o.User_ID)
			.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<OrderItem>()
			.HasOne(oi => oi.Order)
			.WithMany(o => o.OrderItems)
			.HasForeignKey(oi => oi.Order_ID);

			modelBuilder.Entity<OrderItem>()
			.HasOne(oi => oi.Product)
			.WithMany(p => p.OrderItems)
			.HasForeignKey(oi => oi.Product_ID);


			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

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
		public DbSet<AssignmentExercise> assignmentExercises { get; set; }

	}
}
