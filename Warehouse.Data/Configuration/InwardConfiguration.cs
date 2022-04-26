using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class InwardConfiguration : IEntityTypeConfiguration<Inward>
    {
        public void Configure(EntityTypeBuilder<Inward> builder)
        {
            builder.ToTable("Inward");
            builder.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            builder.Property(e => e.CreatedBy).HasMaxLength(100);

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Deliver).HasMaxLength(255);

            builder.Property(e => e.DeliverAddress).HasMaxLength(50);

            builder.Property(e => e.DeliverDepartment).HasMaxLength(50);

            builder.Property(e => e.DeliverPhone)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Description).HasMaxLength(255);

            builder.Property(e => e.ModifiedBy).HasMaxLength(100);

            builder.Property(e => e.ModifiedDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Reason).HasMaxLength(255);

            builder.Property(e => e.ReasonDescription).HasMaxLength(255);

            builder.Property(e => e.Receiver).HasMaxLength(255);

            builder.Property(e => e.ReceiverAddress).HasMaxLength(50);

            builder.Property(e => e.ReceiverDepartment).HasMaxLength(50);

            builder.Property(e => e.ReceiverPhone)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Reference)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.VendorId)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.Voucher)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.VoucherCode)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.VoucherDate)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.WareHouseId)
                .IsRequired()
                .HasColumnName("WareHouseID")
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}