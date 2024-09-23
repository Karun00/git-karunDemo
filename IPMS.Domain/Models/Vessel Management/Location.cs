using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;


namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Location : EntityBase
    {
        public Location()
        {
            this.DivingRequests = new List<DivingRequest>();
            this.DivingRequests1 = new List<DivingRequest>();
            this.SuppHotColdWorkPermits = new List<SuppHotColdWorkPermit>();
            this.DredgingOperations = new List<DredgingOperation>();        
        }

        [DataMember]
        public int LocationID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string LocationName { get; set; }
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
        public  ICollection<DivingRequest> DivingRequests { get; set; }
        [DataMember]
        public  ICollection<DivingRequest> DivingRequests1 { get; set; }        
        
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  Port Port { get; set; }

        // -- Added by sandeep on 21-8-2014
        [DataMember]
        public  ICollection<SuppHotColdWorkPermit> SuppHotColdWorkPermits { get; set; }
        [DataMember]
        public  ICollection<DredgingOperation> DredgingOperations { get; set; }
        // -- end
    }
}
