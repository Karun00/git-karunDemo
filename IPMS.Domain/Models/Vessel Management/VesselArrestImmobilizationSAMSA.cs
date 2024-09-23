using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class VesselArrestImmobilizationSAMSA : EntityBase
    {
        public VesselArrestImmobilizationSAMSA()
        {
            this.VesselArrestDocuments = new List<VesselArrestDocument>();
            this.VesselSAMSAStopDocuments = new List<VesselSAMSAStopDocument>();
        }

        [DataMember]
        public int VAISID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string VesselArrested { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ArrestedDate { get; set; }
        [DataMember]
        public string ArrestedRemarks { get; set; }
        [DataMember]
        public string VesselReleased { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ReleasedDate { get; set; }
        [DataMember]
        public string ReleasedRemarks { get; set; }
        [DataMember]
        public string Immobilization { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ImmobilizationStartDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ImmobilizationEndDate { get; set; }
        [DataMember]
        public string ExactWorkProposed { get; set; }
        [DataMember]
        public string PollutionPrecautionTaken { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        [DataMember]
        public string SAMSAStop { get; set; }
        [DataMember]
        public Nullable<System.DateTime> SAMSAStopDate { get; set; }
        [DataMember]
        public string SAMSAStopRemarks { get; set; }
        [DataMember]
        public string SAMSACleared { get; set; }
        [DataMember]
        public Nullable<System.DateTime> SAMSAClearedDate { get; set; }
        [DataMember]
        public string SAMSAClearedRemarks { get; set; }
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
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<VesselArrestDocument> VesselArrestDocuments { get; set; }
        [DataMember]
        public  ICollection<VesselSAMSAStopDocument> VesselSAMSAStopDocuments { get; set; }
    }
}
