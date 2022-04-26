using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class WareHouseConfiguration : IEntityTypeConfiguration<WareHouse>
    {
        public void Configure(EntityTypeBuilder<WareHouse> entity)
        {
            entity.ToTable("WareHouse");
            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            entity.Property(e => e.Address).HasMaxLength(255);

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.Property(e => e.Name)
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