using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Gym_Api.Data.Configurations
{
	public class UserConfigurations : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(d => d.UserId);


			builder.HasOne(d => d.ApplicationUser)
				.WithMany()
				.HasForeignKey(d => d.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.NoAction);

		
		}
	}
}
