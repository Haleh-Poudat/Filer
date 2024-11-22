using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eBooks.Domain.Entities.Identity;

namespace eBooks.DataLayer.Configurations
{
    public class UserClaimConfiguration:IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("UserClaims");
            builder.HasKey(k => k.Id);
            builder.Property(typeof(Guid), "Id");
            builder.HasOne(u => u.Users)
                .WithMany(uc => uc.UserClaims)
                .HasForeignKey(u => u.UserId);
        }
    }
}
