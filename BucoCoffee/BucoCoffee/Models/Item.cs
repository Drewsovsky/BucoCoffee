using System;

namespace BucoCoffee.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public ProductType SelectedProductType { get; set; }
        public string PackingDate { get; set; } // DateTime
        public string PackageDate { get; set; } // DateTime
        public string PackerName { get; set; }
        public int PackageAmount { get; set; }
        public int Weight { get; set; }
        public string Comment { get; set; }
    }
}
