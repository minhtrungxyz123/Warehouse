﻿namespace Warehouse.WebApp.Models
{
    public class SelectItem
    {
        public object id { get; set; }
        public string text { get; set; }
        public bool selected { get; set; } = false;
    }
}