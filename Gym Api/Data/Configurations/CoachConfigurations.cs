using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym_Api.Data.Configurations
{
	public class CoachConfigurations : IEntityTypeConfiguration<Coach>
	{
		public void Configure(EntityTypeBuilder<Coach> builder)
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
