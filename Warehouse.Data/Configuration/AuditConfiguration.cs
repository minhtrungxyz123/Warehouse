using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.ToTable("Audit");
            builder.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            builder.Property(e => e.CreatedBy).HasMaxLength(100);

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Description).HasMaxLength(255);

            builder.Property(e => e.ModifiedBy).HasMaxLength(100);

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.VoucherCode)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.VoucherDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.WareHouseId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false);
        }
    }
}