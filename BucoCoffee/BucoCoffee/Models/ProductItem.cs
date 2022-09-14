using System;

namespace BucoCoffee.Models
{
    public class ProductItem
    {
        public Guid Id { get; set; }
        public Guid SelectedProductTypeId { get; set; }
        public Guid SelectedPackingTypeId { get; set; }
        public string PackingDate { get; set; } // DateTime
        public string PackageDate { get; set; } // DateTime
        public string PackerName { get; set; }
        public int PackageAmount { get; set; }
        public double Weight { get; set; }
        public string Comment { get; set; }

        public ProductType ProductKeyType { get; set; }
        public PackingType PackingKeyType { get; set; }
    }
}
