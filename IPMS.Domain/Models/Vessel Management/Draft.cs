using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class Draft : EntityBase  
    {
        public int DraftID { get; set; }
        public string DraftKey { get; set; }
        public string VesselName { get; set; }
        public string IMONo { get; set; }
        public Nullable<int> AgentID { get; set; }
        public string EntityCode { get; set; }
        public string EntityData { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
