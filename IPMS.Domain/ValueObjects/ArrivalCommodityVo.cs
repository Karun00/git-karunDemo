
using System;
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Arrival Notification Commodity
    /// </summary>
    public class ArrivalCommodityVo
    {
        public int ArrivalCommodityID { get; set; }
        public string VCN { get; set; }
        public string PortCode { get; set; }
        public string QuayCode { get; set; }
        public string BerthCode { get; set; }
        public string CargoType { get; set; }
        public string Package { get; set; }
        public string UOM { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CommodityBerthKey { get; set; }

        //-- Added by sandeep om 23-03-2015
        public string BerthName { get; set; }
        public string CargoName { get; set; }
        public string UOMName { get; set; }
        public string PackageName { get; set; }
        //-- end
        public string Commodity { get; set; }
    }
}
