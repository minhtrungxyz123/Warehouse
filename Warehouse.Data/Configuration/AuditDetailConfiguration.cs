using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class AuditDetailConfiguration : IEntityTypeConfiguration<AuditDetail>
    {
        public void Configure(EntityTypeBuilder<AuditDetail> builder)
        {
            builder.ToTable("AuditDetail");
            builder.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            builder.Property(e => e.AuditId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.AuditQuantity).HasColumnType("decimal(15, 2)");

            builder.Property(e => e.Conclude)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.ItemId)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.Quantity).HasColumnType("decimal(15, 2)");
        }
    }
}