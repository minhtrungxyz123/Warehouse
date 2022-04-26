using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class WareHouseItemCategoryConfiguration : IEntityTypeConfiguration<WareHouseItemCategory>
    {
        public void Configure(EntityTypeBuilder<WareHouseItemCategory> entity)
        {
            entity.ToTable("WareHouseItemCategory");
            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Inactive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.ParentId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Path)
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}