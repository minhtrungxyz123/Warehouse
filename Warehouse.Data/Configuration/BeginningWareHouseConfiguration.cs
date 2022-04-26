using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class BeginningWareHouseConfiguration : IEntityTypeConfiguration<BeginningWareHouse>
    {
        public void Configure(EntityTypeBuilder<BeginningWareHouse> builder)
        {
            builder.ToTable("BeginningWareHouse");
            builder.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            builder.Property(e => e.CreatedBy).HasMaxLength(100);

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.ModifiedBy).HasMaxLength(100);

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Quantity).HasColumnType("decimal(15, 2)");

            builder.Property(e => e.UnitId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.UnitName).HasMaxLength(255);

            builder.Property(e => e.WareHouseId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}