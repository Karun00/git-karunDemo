using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppWaterService:EntityBase
    {
        public SuppWaterService()
        {
            this.SuppServiceResourceAllocs = new List<SuppServiceResourceAlloc>();
        }

        [DataMember]
        public int SuppWaterServiceID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public System.DateTime ServiceDate { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public Nullable<long> Quantity { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceID { get; set; }
        [DataMember]
        public string Remarks { get; set; }
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
        public  ArrivalNotification ArrivalNotification { get; set; }
        [DataMember]
        public  Berth Berth { get; set; }
        [DataMember]
        public  ICollection<SuppServiceResourceAlloc> SuppServiceResourceAllocs { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  WorkflowInstance WorkflowInstance { get; set; }
    }
}
