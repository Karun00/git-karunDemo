using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{

    public class MarineRevenuePostingVO
    {

        public int RevenuePostingID { get; set; }
        public int ISPOSTED { get; set; }
        public int ResourceAllocationID { get; set; }
        public string VCN { get; set; }
        public string MovementName { get; set; }
        public string ServiceName { get; set; }
        public string GroupCode { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string AccountNo { get; set; }
        public Nullable<DateTime> StartTime { get; set; }
        public Nullable<DateTime> Endtime { get; set; }
        public string IsCalculated { get; set; }
        public string Chargedas { get; set; }
        public string UOM { get; set; }
        public string MovementType { get; set; }
        public string ServiceType { get; set; }
        public string ServiceReferenceType { get; set; }
        public string OperationType { get; set; }
        public string TaskStatus { get; set; }
        public Nullable<DateTime> RecentlyPostedDate { get; set; }
        public Nullable<decimal> DueHours { get; set; }
        public Nullable<decimal> TotalHours { get; set; }
        public bool ischecked { get; set; }
        //public Nullable<DateTime> PostingFrom { get; set; }
        public Nullable<DateTime> PostingDateTime { get; set; }
        //public Nullable<DateTime> PostedOn { get; set; }
        public Nullable<decimal> CloseMterReding { get; set; }
        public Nullable<decimal> startmtrreding { get; set; }
        public string MeterSerialNo { get; set; }
        public string BerthName { get; set; }

        


    }
}
