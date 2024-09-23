using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
    public partial class RevenueStopList : EntityBase
    {
        public RevenueStopList()
        {
            this.RevenueAccountStatus = new List<RevenueAccountStatus>();
        }

        public int RevenueStopListID { get; set; }
        public string PortCode { get; set; }
        public int AgentID { get; set; }
        public int AgentAccountID { get; set; }
        public System.DateTime StopDate { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public  Agent Agent { get; set; }  
        public  Port Port { get; set; }
        public  ICollection<RevenueAccountStatus> RevenueAccountStatus { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  AgentAccount AgentAccount { get; set; }
        public global::Core.Repository.ObjectState ObjectState { get; set; }
    }
}

