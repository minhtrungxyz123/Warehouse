using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class WareHouseItemUnitConfiguration : IEntityTypeConfiguration<WareHouseItemUnit>
    {
        public void Configure(EntityTypeBuilder<WareHouseItemUnit> entity)
        {
            entity.ToTable("WareHouseItemUnit");
            entity.Property(e => e.Id)
                     .HasMaxLength(36)
                     .IsUnicode(false)
                     .HasDefaultValueSql("('')");

            entity.Property(e => e.IsPrimary)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.UnitId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.UnitName)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}