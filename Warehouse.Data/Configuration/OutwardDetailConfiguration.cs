using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class OutwardDetailConfiguration : IEntityTypeConfiguration<OutwardDetail>
    {
        public void Configure(EntityTypeBuilder<OutwardDetail> entity)
        {
            entity.ToTable("OutwardDetail");
            entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

            entity.Property(e => e.AccountMore).HasMaxLength(255);

            entity.Property(e => e.AccountYes).HasMaxLength(255);

            entity.Property(e => e.Amount)
                .HasColumnType("decimal(15, 2)")
                .HasDefaultValueSql("((0.00))");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.CustomerName).HasMaxLength(255);

            entity.Property(e => e.DepartmentId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.DepartmentName).HasMaxLength(255);

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.EmployeeName).HasMaxLength(255);

            entity.Property(e => e.ItemId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.OutwardId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Price)
                .HasColumnType("decimal(15, 2)")
                .HasDefaultValueSql("((0.00))");

            entity.Property(e => e.ProjectId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.ProjectName).HasMaxLength(255);

            entity.Property(e => e.Quantity).HasColumnType("decimal(15, 2)");

            entity.Property(e => e.StationId)
                .HasMaxLength(36)
                .IsUnicode(false);

            entity.Property(e => e.StationName).HasMaxLength(255);

            entity.Property(e => e.Status).HasMaxLength(255);

            entity.Property(e => e.Uiprice)
                .HasColumnName("UIPrice")
                .HasColumnType("decimal(15, 2)")
                .HasDefaultValueSql("((0.00))");

            entity.Property(e => e.Uiquantity)
                .HasColumnName("UIQuantity")
                .HasColumnType("decimal(15, 2)");

            entity.Property(e => e.UnitId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}