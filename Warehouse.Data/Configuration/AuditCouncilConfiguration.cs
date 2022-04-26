using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class AuditCouncilConfiguration : IEntityTypeConfiguration<AuditCouncil>
    {
        public void Configure(EntityTypeBuilder<AuditCouncil> builder)
        {
            builder.ToTable("AuditCouncil");
            builder.Property(e => e.Id)
                   .HasMaxLength(36)
                   .IsUnicode(false)
                   .HasDefaultValueSql("('')");

            builder.Property(e => e.AuditId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.EmployeeId)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Role)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}