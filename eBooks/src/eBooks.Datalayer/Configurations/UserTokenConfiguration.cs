using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eBooks.Domain.Entities.Identity;

namespace eBooks.DataLayer.Configurations
{
    public class UserTokenConfiguration:IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
            builder.HasKey(k => k.Id);
            builder.Property(typeof(Guid), "Id");
            builder.HasOne(u => u.Users)
                .WithMany(ut => ut.UserTokens)
                .HasForeignKey(u => u.UserId);
        }
    }
}
