// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Data.Entities.Base;

namespace Warehouse.Data.Entities
{
    public partial class WareHouseItemUnit : BaseEntity
    {
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public string? UnitName { get; set; }
        public int? ConvertRate { get; set; }
        public bool? IsPrimary { get; set; }
    }
}