using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPMS.Domain.Models
{
    public partial class AgentAccount : EntityBase
    {
        public AgentAccount()
        {
            this.RevenueStopLists = new List<RevenueStopList>();
        }

        public int AgentAccountID { get; set; }
        public int AgentID { get; set; }
        //public string AccountName { get; set; }
        public string AccountNo { get; set; }         
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string PortCode { get; set; }
        public  Agent Agent { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  Port Port { get; set; }
        public  ICollection<RevenueStopList> RevenueStopLists { get; set; }
    }
}
