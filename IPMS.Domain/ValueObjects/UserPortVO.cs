using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class UserPortVO : EntityBase
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string WFStatus { get; set; }
        [DataMember]
        public Nullable<int> VerifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> VerifiedDate { get; set; }
        [DataMember]
        public Nullable<int> ApprovedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        [DataMember]
        public string RejectComments { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [DataMember]
        public string IsFinal { get; set; }
    }
   
}
