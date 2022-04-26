// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using Warehouse.Data.Entities.Base;

namespace Warehouse.Data.Entities
{
    public partial class Unit : BaseEntity
    {
        public string UnitName { get; set; }
        public bool Inactive { get; set; }
    }
}