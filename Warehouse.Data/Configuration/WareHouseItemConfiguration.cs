using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class WareHouseItemConfiguration : IEntityTypeConfiguration<WareHouseItem>
    {
        public void Configure(EntityTypeBuilder<WareHouseItem> entity)
        {
            entity.ToTable("WareHouseItem");
            entity.Property(e => e.Id)
                   .HasMaxLength(36)
                   .IsUnicode(false)
                   .HasDefaultValueSql("('')");

            entity.Property(e => e.CategoryId)
                .HasColumnName("CategoryID")
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Country).HasMaxLength(50);

            entity.Property(e => e.Description).HasMaxLength(50);

            entity.Property(e => e.Inactive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.Property(e => e.UnitId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.VendorId)
                .HasColumnName("VendorID")
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.VendorName)
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}