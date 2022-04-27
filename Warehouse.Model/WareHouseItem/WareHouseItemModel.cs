﻿namespace Warehouse.Model.WareHouseItem
{
    public class WareHouseItemModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string Country { get; set; }
        public string UnitId { get; set; }
        public bool? Inactive { get; set; }
    }
}