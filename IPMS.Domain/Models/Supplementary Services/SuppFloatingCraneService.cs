﻿using System.Runtime.Serialization;
using Core.Repository;


namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppFloatingCraneService : EntityBase
    {
        [DataMember]
        public int SuppFloatingCraneServiceID { get; set; }
        [DataMember]
        public int SuppFloatingCraneID { get; set; }
        [DataMember]
        public decimal MassMT { get; set; }
        [DataMember]
        public long Quantity { get; set; }
        [DataMember]
        public string Description { get; set; }
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
        public virtual SuppFloatingCrane SuppFloatingCrane { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
    }
}