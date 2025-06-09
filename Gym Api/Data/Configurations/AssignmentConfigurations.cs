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
            .HasForeignKey(a => a.User_ID)
            .OnDelete(DeleteBehavior.Restrict); 

            builder
                .HasOne(a => a.Coach)
                .WithMany(c => c.Assignments)
            .HasForeignKey(a => a.Coach_ID)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
