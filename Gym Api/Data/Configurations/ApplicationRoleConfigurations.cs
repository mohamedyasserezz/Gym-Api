using Gym_Api.Common.Consts;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym_Api.Data.Configurations
{
    public class ApplicationRoleConfigurations : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData([
                new ApplicationRole
                {
                    Id = DefaultRoles.AdminRoleId,
                    Name = DefaultRoles.Admin,
                    NormalizedName = DefaultRoles.Admin.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.CoachRoleId,
                    Name = DefaultRoles.Coach,
                    NormalizedName = DefaultRoles.Coach.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.CoachRoleConcurrencyStamp
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.UserRoleId,
                    Name = DefaultRoles.User,
                    NormalizedName = DefaultRoles.User.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.UserRoleConcurrencyStamp,
                    IsDefault = true
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.TraineeRoleId,
                    Name = DefaultRoles.Trainee,
                    NormalizedName = DefaultRoles.Trainee.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.TraineeRoleConcurrencyStamp
                }]);
        }
    }
}
