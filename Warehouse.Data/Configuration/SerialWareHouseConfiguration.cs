using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class SerialWareHouseConfiguration : IEntityTypeConfiguration<SerialWareHouse>
    {
        public void Configure(EntityTypeBuilder<SerialWareHouse> entity)
        {
            entity.ToTable("SerialWareHouse");
            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            entity.Property(e => e.InwardDetailId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.OutwardDetailId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Serial)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}