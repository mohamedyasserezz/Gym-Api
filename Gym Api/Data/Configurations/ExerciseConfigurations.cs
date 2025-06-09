using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym_Api.Data.Configurations
{
	public class ExerciseConfigurations : IEntityTypeConfiguration<Exercise>
	{
		public void Configure(EntityTypeBuilder<Exercise> builder)
		{
			builder
					.HasOne(e => e.Category)
					.WithMany(c => c.Exercises)
					.HasForeignKey(e => e.Category_ID)
					.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
