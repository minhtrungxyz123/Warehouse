using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> entity)
        {
            entity.ToTable("Unit");
            entity.Property(e => e.Id)
                   .HasMaxLength(36)
                   .IsUnicode(false)
                   .HasDefaultValueSql("('')");

            entity.Property(e => e.UnitName).HasMaxLength(255);
        }
    }
}