using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eBooks.Domain.Entities.Identity;

namespace eBooks.DataLayer.Configurations
{
    public class RoleClaimConfiguration:IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaims");
            builder.HasKey(k => k.Id);
            builder.Property(typeof(Guid), "Id");
            builder.HasOne(r => r.Roles)
                .WithMany(rc => rc.RoleClaims)
                .HasForeignKey(rc=>rc.RoleId);
        }
    }
}
