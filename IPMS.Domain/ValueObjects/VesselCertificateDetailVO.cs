using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class VesselCertificateDetailVO
    {
        [DataMember]
        public int VACERTID { get; set; }
        [DataMember]
        public int VesselID { get; set; }
        [DataMember]
        public string CertificateName { get; set; }
        [DataMember]
        public string CertificateNo { get; set; }
        [DataMember]
        //public Nullable<System.DateTime> DateOfIssue { get; set; }
        public string DateOfIssue { get; set; }
        [DataMember]
        //public Nullable<System.DateTime> DateOfValidity { get; set; }
        public string DateOfValidity { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }

      
    }
}
