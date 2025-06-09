using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym_Api.Data.Configurations
{
	public class AssignmentExerciseConfiguration : IEntityTypeConfiguration<AssignmentExercise>
	{
		public void Configure(EntityTypeBuilder<AssignmentExercise> builder)
		{
			builder
				.HasOne(ae => ae.Exercise)
				.WithMany(e => e.AssignmentExercises)
				.HasForeignKey(ae => ae.Exercise_ID)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(ae => ae.Assignment)
				.WithMany(a => a.AssignmentExercises)
				.HasForeignKey(ae => ae.AssignmentId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
