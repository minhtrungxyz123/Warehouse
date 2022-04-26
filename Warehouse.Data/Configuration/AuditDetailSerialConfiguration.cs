using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class AuditDetailSerialConfiguration : IEntityTypeConfiguration<AuditDetailSerial>
    {
        public void Configure(EntityTypeBuilder<AuditDetailSerial> builder)
        {
            builder.ToTable("AuditDetailSerial");
            builder.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            builder.Property(e => e.AuditDetailId)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.Serial)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}