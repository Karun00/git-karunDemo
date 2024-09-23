using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;


namespace IPMS.Domain.Models
{
    public partial class StatementFact : EntityBase
    {
        public StatementFact()
        {
            this.StatementFactBunkers = new List<StatementFactBunker>();
            this.StatementFactEvents = new List<StatementFactEvent>();
            this.StatementCommodities = new List<StatementCommodity>();
        }

        public int StatementFactID { get; set; }
        public string VCN { get; set; }
        public string OperationCode { get; set; }
        public string MasterName { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public decimal ArrivalFuel { get; set; }
        public decimal ArrivalDiesel { get; set; }
        public decimal SailingFuel { get; set; }
        public decimal SailingDiesel { get; set; }
        public Nullable<System.DateTime> EOSPDateTime { get; set; }
        public Nullable<System.DateTime> GangwayDown { get; set; }
        public Nullable<System.DateTime> NORTendered { get; set; }
        public Nullable<System.DateTime> NORAccepted { get; set; }
        public Nullable<System.DateTime> StevedoreOnBoard { get; set; }
        public Nullable<System.DateTime> StevedoreStart { get; set; }
        public Nullable<System.DateTime> StevedoreEnd { get; set; }
        public Nullable<System.DateTime> StevedoreOff { get; set; }
        public Nullable<int> CranesDeployed { get; set; }
        public Nullable<System.DateTime> StartCargo { get; set; }
        public Nullable<System.DateTime> EndCargo { get; set; }
        public  ArrivalNotification ArrivalNotification { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  SubCategory SubCategory { get; set; }
        public  ICollection<StatementFactBunker> StatementFactBunkers { get; set; }
        public  ICollection<StatementFactEvent> StatementFactEvents { get; set; }
        public ICollection<StatementCommodity> StatementCommodities { get; set; }
    }
}
