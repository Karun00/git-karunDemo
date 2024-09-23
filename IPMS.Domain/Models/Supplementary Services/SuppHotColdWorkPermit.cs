using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Repository;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppHotColdWorkPermit : EntityBase
    {
        public SuppHotColdWorkPermit()
        {
            this.SuppHotColdWorkPermitDocuments = new List<SuppHotColdWorkPermitDocument>();
        }

        [DataMember]
        public int SuppHotColdWorkPermitID { get; set; }
        [DataMember]
        public int SuppServiceRequestID { get; set; }
        [DataMember]
        public string GassFreeCertificateAvailable { get; set; }
        [DataMember]
        public Nullable<System.DateTime> GassFreeCertificateValidity { get; set; }
        [DataMember]
        public string GassFreeIssuingAuthority { get; set; }
        [DataMember]
        public int LocationID { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public  Location Location { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  SuppServiceRequest SuppServiceRequest { get; set; }
        [DataMember]
        public  ICollection<SuppHotColdWorkPermitDocument> SuppHotColdWorkPermitDocuments { get; set; }

        //-- Added by sandeep on 13-04-2015
        [DataMember]
        public string OtherLocation { get; set; }
        //-- end

    }
}
