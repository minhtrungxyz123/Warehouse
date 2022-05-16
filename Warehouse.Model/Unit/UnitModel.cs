using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Warehouse.Model.Unit
{
    public class UnitModel
    {
        public string? Id { get; set; }

        [DisplayName("Tên đơn vị")]
        public string UnitName { get; set; }

        public bool Inactive { get; set; }

    }
}