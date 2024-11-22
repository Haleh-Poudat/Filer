using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eBooks.Domain.Entities.Identity;

namespace eBooks.DataLayer.Configurations
{
    public class UserLoginConfiguration:IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("UserLogins");
            builder.HasKey(k => k.Id);
            builder.Property(typeof(Guid), "Id");
            builder.HasOne(u => u.Users)
                .WithMany(ul => ul.UserLogins)
                .HasForeignKey(u => u.UserId);
        }
    }
}
