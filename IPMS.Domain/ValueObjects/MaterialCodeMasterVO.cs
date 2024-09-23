using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class MaterialCodeMasterVO
    {
        [DataMember]
        public int MaterialCodeMasterid { get; set; }
        [DataMember]
        public string GroupCode { get; set; }
        [DataMember]
        public string MaterialCode { get; set; }
        [DataMember]
        public string MovementType { get; set; }
        [DataMember]
        public string ServiceType { get; set; }
        [DataMember]
        public string MaterialDescription { get; set; }
        [DataMember]
        public string UOM { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public string IsCalculated { get; set; }
        [DataMember]
        public string Chargedas { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
