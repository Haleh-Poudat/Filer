using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.Domain.Entities.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBooks.Datalayer.Configurations
{
    public class LibraryCategoryConfiguration : IEntityTypeConfiguration<LibraryCategory>
    {
        public void Configure(EntityTypeBuilder<LibraryCategory> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(1024).IsRequired(false);
            builder.HasOne(p => p.Parent)
                .WithMany()
                .HasForeignKey(p => p.ParentId).IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.RowVersion).IsRowVersion();
            builder.HasOne(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).OnDelete(DeleteBehavior.Restrict);
        }
    }
}