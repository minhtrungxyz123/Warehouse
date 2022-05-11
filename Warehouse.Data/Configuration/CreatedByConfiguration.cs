using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.Entities;

namespace Warehouse.Data.Configuration
{
    public class CreatedByConfiguration : IEntityTypeConfiguration<CreatedBy>
    {
        public void Configure(EntityTypeBuilder<CreatedBy> builder)
        {
            builder.ToTable("CreatedBy");

            builder.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.AccountName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Avarta).HasMaxLength(255);

            builder.Property(e => e.DateCreate).HasColumnType("datetime");

            builder.Property(e => e.DateRegister).HasColumnType("datetime");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}