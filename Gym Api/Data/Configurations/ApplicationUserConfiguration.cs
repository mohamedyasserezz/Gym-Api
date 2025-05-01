using Gym_Api.Common.Consts;
using Gym_Api.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym_Api.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.FullName)
                .HasMaxLength(100);

            builder.Property(u => u.Address)
                .HasMaxLength(200);

            builder.Property(u => u.Image)
                .HasMaxLength(500);


            builder.Property(x => x.UserType)
                .HasConversion(

                    T => T.ToString(),
                    t => (UserType)System.Enum.Parse(typeof(UserType), t)
                );

            builder.OwnsMany(U => U.RefreshTokens)
                .ToTable("RefreshTokens")
                .WithOwner()
                .HasForeignKey("UserId");

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = DefaultUsers.AdminId,
                    Email = DefaultUsers.AdminEmail,
                    NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
                    UserName = DefaultUsers.AdminrUserName,
                    NormalizedUserName = DefaultUsers.AdminrUserName.ToUpper(),
                    Address = DefaultUsers.AdminAddress,
                    FullName = DefaultUsers.AdminName,
                    ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
                    EmailConfirmed = true,
                    UserType = UserType.Admin,
                    SecurityStamp = DefaultUsers.AdminSecurityStamp,
                    PasswordHash = passwordHasher.HashPassword(null!, DefaultUsers.AdminPassword)
                });

        }
    }
}
