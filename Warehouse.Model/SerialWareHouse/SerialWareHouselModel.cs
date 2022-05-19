using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Model.SerialWareHouse
{
    public class SerialWareHouselModel
    {
        public  string? Id { get; set; }
        public string ItemId { get; set; }

        public string Serial { get; set; }

        public string InwardDetailId { get; set; }

        public string OutwardDetailId { get; set; }

        public bool IsOver { get; set; }
    }
}
