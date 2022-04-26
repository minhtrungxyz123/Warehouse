using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class OutwardConfiguration : IEntityTypeConfiguration<Outward>
    {
        public void Configure(EntityTypeBuilder<Outward> entity)
        {
            entity.ToTable("Outward");
            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            entity.Property(e => e.CreatedBy).HasMaxLength(100);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Deliver).HasMaxLength(255);

            entity.Property(e => e.DeliverAddress).HasMaxLength(50);

            entity.Property(e => e.DeliverDepartment).HasMaxLength(50);

            entity.Property(e => e.DeliverPhone)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Description).HasMaxLength(255);

            entity.Property(e => e.ModifiedBy).HasMaxLength(100);

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Reason).HasMaxLength(255);

            entity.Property(e => e.ReasonDescription).HasMaxLength(255);

            entity.Property(e => e.Receiver).HasMaxLength(255);

            entity.Property(e => e.ReceiverAddress).HasMaxLength(50);

            entity.Property(e => e.ReceiverDepartment).HasMaxLength(50);

            entity.Property(e => e.ReceiverPhone)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Reference)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.ToWareHouseId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.Voucher)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.VoucherCode)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.VoucherDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.WareHouseId)
                .IsRequired()
                .HasColumnName("WareHouseID")
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}