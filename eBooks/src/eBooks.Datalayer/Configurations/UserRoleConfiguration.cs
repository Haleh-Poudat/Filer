using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eBooks.Domain.Entities.Identity;

namespace eBooks.DataLayer.Configurations
{
    public class UserRoleConfiguration:IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasOne(u => u.Users)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(u => u.Roles)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(u => u.RoleId);
            
        }
    }
}
