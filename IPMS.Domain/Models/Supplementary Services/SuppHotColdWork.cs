using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Repository;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppHotColdWork : EntityBase
    {
        public SuppHotColdWork()
        {
            this.SuppHotColdWorkDocuments = new List<SuppHotColdWorkDocument>();
            this.SuppHotWorkInspections = new List<SuppHotWorkInspection>();
        }

        [DataMember]
        public int SuppHotColdWorkID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string ServiceTypeCode { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public System.DateTime FromDate { get; set; }
        [DataMember]
        public System.DateTime ToDate { get; set; }
        [DataMember]
        public int LocationID { get; set; }
        [DataMember]
        public string GasFreeCertificateAvailable { get; set; }
        [DataMember]
        public System.DateTime GasFreeCertificateValidity { get; set; }
        [DataMember]
        public string GasFreeIssuingAuthority { get; set; }
        [DataMember]
        public string AdditionalConditionsApplied { get; set; }
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
        public  Location Location { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<SuppHotColdWorkDocument> SuppHotColdWorkDocuments { get; set; }
        [DataMember]
        public  ICollection<SuppHotWorkInspection> SuppHotWorkInspections { get; set; }
    }
}
