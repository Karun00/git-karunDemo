using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class DivingCheckListHazardVO
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
    }
}
