using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym_Api.Data.Configurations
{
    public class AssignmentConfigurations : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder
            .HasOne(a => a.User)
            .WithMany(u => u.Assignments)
            .HasForeignKey(a => a.User_ID);

            builder
                .HasOne(a => a.Coach)
                .WithMany(c => c.Assignments)
            .HasForeignKey(a => a.Coach_ID);

            builder
                .HasOne(a => a.Exercise)
                .WithMany(e => e.Assignments)
                .HasForeignKey(a => a.Exercise_ID);
        }
    }
}
