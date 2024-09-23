using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
    public partial class RevenuePosting : EntityBase
    {
        public RevenuePosting()
        {
            this.RevenuePostingDtls = new List<RevenuePostingDtl>();
        }

        public int RevenuePostingID { get; set; }
        public string vcn { get; set; }
        public string PortCode { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public string SAPAccNo { get; set; }
        public Nullable<int> AgentID { get; set; }
        public string PostingStatus { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public  Agent Agent { get; set; }
        public  ArrivalNotification ArrivalNotification { get; set; }
        public  Port Port { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  ICollection<RevenuePostingDtl> RevenuePostingDtls { get; set; }
    }
}
