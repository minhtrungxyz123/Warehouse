using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class WareHouseLimitConfiguration : IEntityTypeConfiguration<WareHouseLimit>
    {
        public void Configure(EntityTypeBuilder<WareHouseLimit> entity)
        {
            entity.ToTable("WareHouseLimit");
            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.UnitId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.UnitName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.WareHouseId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}