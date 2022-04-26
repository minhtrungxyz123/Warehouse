using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class InwardDetailConfiguration : IEntityTypeConfiguration<InwardDetail>
    {
        public void Configure(EntityTypeBuilder<InwardDetail> builder)
        {
            builder.ToTable("InwardDetail");
            builder.Property(e => e.Id)
                   .HasMaxLength(36)
                   .IsUnicode(false)
                   .HasDefaultValueSql("('')");

            builder.Property(e => e.AccountMore)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.AccountYes)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Amount)
                .HasColumnType("decimal(15, 2)")
                .HasDefaultValueSql("((0.00))");

            builder.Property(e => e.CustomerId)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.CustomerName).HasMaxLength(255);

            builder.Property(e => e.DepartmentId)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.DepartmentName).HasMaxLength(255);

            builder.Property(e => e.EmployeeId)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.EmployeeName).HasMaxLength(255);

            builder.Property(e => e.InwardId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.Price)
                .HasColumnType("decimal(15, 2)")
                .HasDefaultValueSql("((0.00))");

            builder.Property(e => e.ProjectId)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.ProjectName).HasMaxLength(255);

            builder.Property(e => e.Quantity).HasColumnType("decimal(15, 2)");

            builder.Property(e => e.StationId)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.StationName).HasMaxLength(255);

            builder.Property(e => e.Status).HasMaxLength(255);

            builder.Property(e => e.Uiprice)
                .HasColumnName("UIPrice")
                .HasColumnType("decimal(15, 2)")
                .HasDefaultValueSql("((0.00))");

            builder.Property(e => e.Uiquantity)
                .HasColumnName("UIQuantity")
                .HasColumnType("decimal(15, 2)");

            builder.Property(e => e.UnitId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}