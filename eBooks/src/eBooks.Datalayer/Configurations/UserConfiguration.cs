using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eBooks.Domain.Entities.Identity;

namespace eBooks.DataLayer.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(k => k.Id);
            builder.Property(typeof(Guid), "Id");
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.Property(typeof(string), "UserName")
                .HasMaxLength(10)
                .IsRequired();

            builder.HasIndex(u => u.PhoneNumber).IsUnique(false);
            builder.Property(typeof(string), "PhoneNumber")
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(u => u.Name).IsUnique(false);
            builder.Property(typeof(string), "Name")
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(u => u.Family).IsUnique(false);
            builder.Property(typeof(string), "Family")
                .HasMaxLength(200)
                .IsRequired();
            builder.Property("Email").IsRequired(false);
        }
    }
}