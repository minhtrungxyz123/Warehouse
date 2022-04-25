using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BestHair.Domain.Model1
{
    public partial class WarehouseManagementContext : DbContext
    {
        public WarehouseManagementContext()
        {
        }

        public WarehouseManagementContext(DbContextOptions<WarehouseManagementContext> options)
            : base(options)
        {
        }

//        public virtual DbSet<Audit> Audit { get; set; }
//        public virtual DbSet<AuditCouncil> AuditCouncil { get; set; }
//        public virtual DbSet<AuditDetail> AuditDetail { get; set; }
//        public virtual DbSet<AuditDetailSerial> AuditDetailSerial { get; set; }
//        public virtual DbSet<BeginningWareHouse> BeginningWareHouse { get; set; }
//        public virtual DbSet<Inward> Inward { get; set; }
//        public virtual DbSet<InwardDetail> InwardDetail { get; set; }
//        public virtual DbSet<Outward> Outward { get; set; }
//        public virtual DbSet<OutwardDetail> OutwardDetail { get; set; }
//        public virtual DbSet<SerialWareHouse> SerialWareHouse { get; set; }
//        public virtual DbSet<Unit> Unit { get; set; }
//        public virtual DbSet<Vendor> Vendor { get; set; }
//        public virtual DbSet<WareHouse> WareHouse { get; set; }
//        public virtual DbSet<WareHouseItem> WareHouseItem { get; set; }
//        public virtual DbSet<WareHouseItemCategory> WareHouseItemCategory { get; set; }
//        public virtual DbSet<WareHouseItemUnit> WareHouseItemUnit { get; set; }
//        public virtual DbSet<WareHouseLimit> WareHouseLimit { get; set; }
//        public virtual DbSet<WarehouseBalance> WarehouseBalance { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=TRUNGPV;Database=WarehouseManagement;Trusted_Connection=True;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {

//            modelBuilder.Entity<AuditCouncil>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.AuditId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.EmployeeId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.EmployeeName)
//                    .HasMaxLength(100)
//                    .IsUnicode(false);

//                entity.Property(e => e.Role)
//                    .HasMaxLength(100)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<AuditDetail>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.AuditId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.AuditQuantity).HasColumnType("decimal(15, 2)");

//                entity.Property(e => e.Conclude)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.ItemId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.Quantity).HasColumnType("decimal(15, 2)");
//            });

//            modelBuilder.Entity<AuditDetailSerial>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.AuditDetailId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.ItemId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Serial)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");
//            });

//            modelBuilder.Entity<BeginningWareHouse>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.CreatedBy).HasMaxLength(100);

//                entity.Property(e => e.CreatedDate)
//                    .HasColumnType("datetime2(0)")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.ItemId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

//                entity.Property(e => e.ModifiedDate)
//                    .HasColumnType("datetime2(0)")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.Quantity).HasColumnType("decimal(15, 2)");

//                entity.Property(e => e.UnitId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.UnitName).HasMaxLength(255);

//                entity.Property(e => e.WareHouseId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");
//            });

//            modelBuilder.Entity<Inward>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.CreatedBy).HasMaxLength(100);

//                entity.Property(e => e.CreatedDate)
//                    .HasColumnType("datetime2(0)")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.Deliver).HasMaxLength(255);

//                entity.Property(e => e.DeliverAddress).HasMaxLength(50);

//                entity.Property(e => e.DeliverDepartment).HasMaxLength(50);

//                entity.Property(e => e.DeliverPhone)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Description).HasMaxLength(255);

//                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

//                entity.Property(e => e.ModifiedDate)
//                    .HasColumnType("datetime2(0)")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.Reason).HasMaxLength(255);

//                entity.Property(e => e.ReasonDescription).HasMaxLength(255);

//                entity.Property(e => e.Receiver).HasMaxLength(255);

//                entity.Property(e => e.ReceiverAddress).HasMaxLength(50);

//                entity.Property(e => e.ReceiverDepartment).HasMaxLength(50);

//                entity.Property(e => e.ReceiverPhone)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Reference)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.VendorId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.Voucher)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.VoucherCode)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.VoucherDate)
//                    .HasColumnType("datetime2(0)")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.WareHouseId)
//                    .IsRequired()
//                    .HasColumnName("WareHouseID")
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");
//            });

//            modelBuilder.Entity<InwardDetail>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.AccountMore)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.AccountYes)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.Amount)
//                    .HasColumnType("decimal(15, 2)")
//                    .HasDefaultValueSql("((0.00))");

//                entity.Property(e => e.CustomerId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.CustomerName).HasMaxLength(255);

//                entity.Property(e => e.DepartmentId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.DepartmentName).HasMaxLength(255);

//                entity.Property(e => e.EmployeeId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.EmployeeName).HasMaxLength(255);

//                entity.Property(e => e.InwardId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.ItemId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Price)
//                    .HasColumnType("decimal(15, 2)")
//                    .HasDefaultValueSql("((0.00))");

//                entity.Property(e => e.ProjectId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.ProjectName).HasMaxLength(255);

//                entity.Property(e => e.Quantity).HasColumnType("decimal(15, 2)");

//                entity.Property(e => e.StationId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.StationName).HasMaxLength(255);

//                entity.Property(e => e.Status).HasMaxLength(255);

//                entity.Property(e => e.Uiprice)
//                    .HasColumnName("UIPrice")
//                    .HasColumnType("decimal(15, 2)")
//                    .HasDefaultValueSql("((0.00))");

//                entity.Property(e => e.Uiquantity)
//                    .HasColumnName("UIQuantity")
//                    .HasColumnType("decimal(15, 2)");

//                entity.Property(e => e.UnitId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");
//            });

//            modelBuilder.Entity<Outward>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.CreatedBy).HasMaxLength(100);

//                entity.Property(e => e.CreatedDate)
//                    .HasColumnType("datetime2(0)")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.Deliver).HasMaxLength(255);

//                entity.Property(e => e.DeliverAddress).HasMaxLength(50);

//                entity.Property(e => e.DeliverDepartment).HasMaxLength(50);

//                entity.Property(e => e.DeliverPhone)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Description).HasMaxLength(255);

//                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

//                entity.Property(e => e.ModifiedDate)
//                    .HasColumnType("datetime2(0)")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.Reason).HasMaxLength(255);

//                entity.Property(e => e.ReasonDescription).HasMaxLength(255);

//                entity.Property(e => e.Receiver).HasMaxLength(255);

//                entity.Property(e => e.ReceiverAddress).HasMaxLength(50);

//                entity.Property(e => e.ReceiverDepartment).HasMaxLength(50);

//                entity.Property(e => e.ReceiverPhone)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Reference)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.ToWareHouseId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.Voucher)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.VoucherCode)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.VoucherDate)
//                    .HasColumnType("datetime2(0)")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.WareHouseId)
//                    .IsRequired()
//                    .HasColumnName("WareHouseID")
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");
//            });

//            modelBuilder.Entity<OutwardDetail>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.AccountMore).HasMaxLength(255);

//                entity.Property(e => e.AccountYes).HasMaxLength(255);

//                entity.Property(e => e.Amount)
//                    .HasColumnType("decimal(15, 2)")
//                    .HasDefaultValueSql("((0.00))");

//                entity.Property(e => e.CustomerId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.CustomerName).HasMaxLength(255);

//                entity.Property(e => e.DepartmentId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.DepartmentName).HasMaxLength(255);

//                entity.Property(e => e.EmployeeId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.EmployeeName).HasMaxLength(255);

//                entity.Property(e => e.ItemId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.OutwardId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Price)
//                    .HasColumnType("decimal(15, 2)")
//                    .HasDefaultValueSql("((0.00))");

//                entity.Property(e => e.ProjectId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.ProjectName).HasMaxLength(255);

//                entity.Property(e => e.Quantity).HasColumnType("decimal(15, 2)");

//                entity.Property(e => e.StationId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.StationName).HasMaxLength(255);

//                entity.Property(e => e.Status).HasMaxLength(255);

//                entity.Property(e => e.Uiprice)
//                    .HasColumnName("UIPrice")
//                    .HasColumnType("decimal(15, 2)")
//                    .HasDefaultValueSql("((0.00))");

//                entity.Property(e => e.Uiquantity)
//                    .HasColumnName("UIQuantity")
//                    .HasColumnType("decimal(15, 2)");

//                entity.Property(e => e.UnitId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");
//            });

//            modelBuilder.Entity<SerialWareHouse>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.InwardDetailId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.ItemId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.OutwardDetailId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.Serial)
//                    .IsRequired()
//                    .HasMaxLength(50)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");
//            });

//            modelBuilder.Entity<Unit>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.UnitName).HasMaxLength(255);
//            });

//            modelBuilder.Entity<Vendor>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Address).HasMaxLength(50);

//                entity.Property(e => e.Code)
//                    .IsRequired()
//                    .HasMaxLength(20)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.ContactPerson).HasMaxLength(50);

//                entity.Property(e => e.Email)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Name).HasMaxLength(50);

//                entity.Property(e => e.Phone)
//                    .HasMaxLength(20)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<WareHouse>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Address).HasMaxLength(255);

//                entity.Property(e => e.Code)
//                    .IsRequired()
//                    .HasMaxLength(255)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Description).HasMaxLength(255);

//                entity.Property(e => e.Name)
//                    .HasMaxLength(100)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.ParentId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.Path)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<WareHouseItem>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.CategoryId)
//                    .HasColumnName("CategoryID")
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.Code)
//                    .IsRequired()
//                    .HasMaxLength(20)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Country).HasMaxLength(50);

//                entity.Property(e => e.Description).HasMaxLength(50);

//                entity.Property(e => e.Inactive)
//                    .IsRequired()
//                    .HasDefaultValueSql("((1))");

//                entity.Property(e => e.Name).HasMaxLength(50);

//                entity.Property(e => e.UnitId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.VendorId)
//                    .HasColumnName("VendorID")
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.VendorName)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<WareHouseItemCategory>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Code)
//                    .IsRequired()
//                    .HasMaxLength(20)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Description)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.Inactive)
//                    .IsRequired()
//                    .HasDefaultValueSql("((1))");

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(100)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.ParentId)
//                    .HasMaxLength(36)
//                    .IsUnicode(false);

//                entity.Property(e => e.Path)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<WareHouseItemUnit>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.IsPrimary)
//                    .IsRequired()
//                    .HasDefaultValueSql("((1))");

//                entity.Property(e => e.ItemId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.UnitId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");
//            });

//            modelBuilder.Entity<WareHouseLimit>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.CreatedBy)
//                    .HasMaxLength(100)
//                    .IsUnicode(false);

//                entity.Property(e => e.CreatedDate)
//                    .HasColumnType("datetime2(0)")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.ItemId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.ModifiedBy)
//                    .HasMaxLength(100)
//                    .IsUnicode(false);

//                entity.Property(e => e.ModifiedDate)
//                    .HasColumnType("datetime2(0)")
//                    .HasDefaultValueSql("(getdate())");

//                entity.Property(e => e.UnitId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.UnitName)
//                    .HasMaxLength(255)
//                    .IsUnicode(false);

//                entity.Property(e => e.WareHouseId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");
//            });

//            modelBuilder.Entity<WarehouseBalance>(entity =>
//            {
//                entity.Property(e => e.Id)
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Amount)
//                    .HasColumnType("decimal(15, 2)")
//                    .HasDefaultValueSql("((0.00))");

//                entity.Property(e => e.ItemId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");

//                entity.Property(e => e.Quantity).HasColumnType("decimal(15, 2)");

//                entity.Property(e => e.WarehouseId)
//                    .IsRequired()
//                    .HasMaxLength(36)
//                    .IsUnicode(false)
//                    .HasDefaultValueSql("('')");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
