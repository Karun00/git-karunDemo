using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Quay : EntityBase
    {
        public Quay()
        {
            this.Berths = new List<Berth>();
            this.DivingRequests = new List<DivingRequest>();
        }

        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public string QuayName { get; set; }
        [DataMember]
        public decimal QuayLength { get; set; }
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
        public  ICollection<Berth> Berths { get; set; }

        [DataMember]
        public  Port Port { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }

        [DataMember]
        public  ICollection<DivingRequest> DivingRequests { get; set; }


    }
}
