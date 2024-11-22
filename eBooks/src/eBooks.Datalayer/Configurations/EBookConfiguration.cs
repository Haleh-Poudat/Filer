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
    internal class EBookConfiguration : IEntityTypeConfiguration<Ebook>
    {
        public void Configure(EntityTypeBuilder<Ebook> builder)
        {
            builder.HasOne(p => p.LibraryCategory).WithMany(p => p.Ebooks).HasForeignKey(p => p.LibraryCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(1000).IsRequired(false);
            builder.Property(p => p.Path).IsRequired().HasMaxLength(250);

            builder.Property(p => p.RowVersion).IsRowVersion();
            builder.HasOne(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).OnDelete(DeleteBehavior.Restrict);
        }
    }
}