using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.WebApi.Migrations
{
    public partial class up : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditCouncil",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    AuditId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    EmployeeId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    EmployeeName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditCouncil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    AuditId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ItemId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    AuditQuantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Conclude = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditDetailSerial",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ItemId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    Serial = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    AuditDetailId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditDetailSerial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeginningWareHouse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    WareHouseId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ItemId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    UnitId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    UnitName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeginningWareHouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inward",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    VoucherCode = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    VoucherDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    WareHouseID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    Deliver = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VendorId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReasonDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Reference = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OnDelete = table.Column<bool>(type: "bit", nullable: false),
                    DeliverPhone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DeliverAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeliverDepartment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReceiverPhone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ReceiverAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReceiverDepartment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Voucher = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inward", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InwardDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    InwardId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ItemId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    UnitId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    UIQuantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    UIPrice = table.Column<decimal>(type: "decimal(15,2)", nullable: false, defaultValueSql: "((0.00))"),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false, defaultValueSql: "((0.00))"),
                    Quantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false, defaultValueSql: "((0.00))"),
                    DepartmentId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmployeeId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StationId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OnDelete = table.Column<bool>(type: "bit", nullable: false),
                    AccountMore = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    AccountYes = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InwardDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Outward",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    VoucherCode = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    VoucherDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    WareHouseID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ToWareHouseId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Deliver = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReasonDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Reference = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OnDelete = table.Column<bool>(type: "bit", nullable: false),
                    ReceiverDepartment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReceiverAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReceiverPhone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DeliverDepartment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeliverAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeliverPhone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Voucher = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outward", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutwardDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    OutwardId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ItemId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    UnitId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    UIQuantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    UIPrice = table.Column<decimal>(type: "decimal(15,2)", nullable: false, defaultValueSql: "((0.00))"),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false, defaultValueSql: "((0.00))"),
                    Quantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false, defaultValueSql: "((0.00))"),
                    DepartmentId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmployeeId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StationId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OnDelete = table.Column<bool>(type: "bit", nullable: false),
                    AccountMore = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AccountYes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutwardDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SerialWareHouse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ItemId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    Serial = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    InwardDetailId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    OutwardDetailId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    IsOver = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerialWareHouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    UnitName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Inactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Inactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WareHouse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    Code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValueSql: "('')"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ParentId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Inactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseBalance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ItemId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    WarehouseId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    Quantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false, defaultValueSql: "((0.00))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseBalance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WareHouseItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VendorID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    VendorName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    Inactive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouseItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WareHouseItemCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    ParentId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false),
                    Path = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Inactive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouseItemCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WareHouseItemUnit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ItemId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    UnitId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ConvertRate = table.Column<int>(type: "int", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouseItemUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WareHouseLimit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    WareHouseId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    ItemId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    UnitId = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValueSql: "('')"),
                    UnitName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    MinQuantity = table.Column<int>(type: "int", nullable: false),
                    MaxQuantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouseLimit", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditCouncil");

            migrationBuilder.DropTable(
                name: "AuditDetail");

            migrationBuilder.DropTable(
                name: "AuditDetailSerial");

            migrationBuilder.DropTable(
                name: "BeginningWareHouse");

            migrationBuilder.DropTable(
                name: "Inward");

            migrationBuilder.DropTable(
                name: "InwardDetail");

            migrationBuilder.DropTable(
                name: "Outward");

            migrationBuilder.DropTable(
                name: "OutwardDetail");

            migrationBuilder.DropTable(
                name: "SerialWareHouse");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "WareHouse");

            migrationBuilder.DropTable(
                name: "WarehouseBalance");

            migrationBuilder.DropTable(
                name: "WareHouseItem");

            migrationBuilder.DropTable(
                name: "WareHouseItemCategory");

            migrationBuilder.DropTable(
                name: "WareHouseItemUnit");

            migrationBuilder.DropTable(
                name: "WareHouseLimit");
        }
    }
}
