using Core.Repository;
using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class BerthPlanningConfigurations: EntityBase
    {
        [DataMember]
        public int BerthPlanConfigid { get; set; }
        [DataMember]
        public Nullable<int> Days { get; set; }
        [DataMember]
        public Nullable<int> Slot { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public virtual Port Port { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
    }
}
