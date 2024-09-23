using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class CraftOutOfCommissionVO
    {
        [DataMember]
        public int CraftOutOfCommissionID { get; set; }
        [DataMember]
        public int CraftID { get; set; }
        [DataMember]
        public decimal ExpectedDuration { get; set; }

        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string CraftCommissionStatus { get; set; }

        [DataMember]
        public string ReasonName { get; set; }
        [DataMember]
        public string CraftTypeName { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string CraftCode { get; set; }
        [DataMember]
        public string CraftName { get; set; }
        [DataMember]
        public string CraftType { get; set; }
        [DataMember]
        public string IMONo { get; set; }

        //Added By Santosh B on 12-Jan-2015
        [DataMember]
        public Nullable<DateTime> OutOfCommissionDate { get; set; }
        [DataMember]
        public Nullable<DateTime> BackToCommissionDate { get; set; }
    }
}
