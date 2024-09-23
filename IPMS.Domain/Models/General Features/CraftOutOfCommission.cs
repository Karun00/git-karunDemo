using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class CraftOutOfCommission : EntityBase
    {
        [DataMember]
        public int CraftOutOfCommissionID { get; set; }
        [DataMember]
        public int CraftID { get; set; }
        //[DataMember]
        //public int ExpectedDuration { get; set; }
        [DataMember]
        public decimal ExpectedDuration { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string CraftCommissionStatus { get; set; }
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
        public virtual Craft Craft { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory1 { get; set; }

        //Added By Santosh B on 12-Jan-2015
        [DataMember]
        public Nullable<DateTime> OutOfCommissionDate { get; set; }
        [DataMember]
        public Nullable<DateTime> BackToCommissionDate { get; set; }
    }
}
