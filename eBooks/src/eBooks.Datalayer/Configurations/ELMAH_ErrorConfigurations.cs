using eBooks.Domain.Entities.Logs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eBooks.Datalayer.Configurations
{
    internal class ELMAH_ErrorConfigurations : IEntityTypeConfiguration<ELMAH_Error>
    {
        public void Configure(EntityTypeBuilder<ELMAH_Error> builder)
        {
            builder.HasKey(e => e.ErrorId);
            builder.HasKey(e => e.Sequence).HasAnnotation("SqlServer:Identity", "1, 1");
            builder.Property(e => e.ErrorId).HasDefaultValueSql("NEWID()");
            builder.Property(e => e.Application).IsRequired().HasMaxLength(60);
            builder.Property(e => e.Host).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Type).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Source).IsRequired().HasMaxLength(60);
            builder.Property(e => e.Message).IsRequired().HasMaxLength(500);
            builder.Property(e => e.User).IsRequired().HasMaxLength(50);
            builder.Property(e => e.StatusCode).IsRequired();
            builder.Property(e => e.TimeUtc).IsRequired();
            builder.Property(e => e.AllXml).IsRequired();

            builder.HasIndex(e => new { e.Application, e.TimeUtc, e.Sequence });
        }
    }
}