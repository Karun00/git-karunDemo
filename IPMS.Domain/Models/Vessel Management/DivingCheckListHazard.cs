using System.Runtime.Serialization;
using Core.Repository;
using System.Collections.Generic;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class DivingCheckListHazard : EntityBase
    {
        [DataMember]
        public int DivingCheckListHazardID { get; set; }
        [DataMember]
        public int DivingCheckListID { get; set; }
        [DataMember]
        public string Hazard { get; set; }
        [DataMember]
        public string Cause { get; set; }
        [DataMember]
        public string Action { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public virtual DivingCheckList DivingCheckList { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
    }
}
