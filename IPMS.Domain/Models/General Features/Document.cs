using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Document : EntityBase
    {
        public Document()
        {
            this.AgentDocuments = new List<AgentDocument>();
            this.ArrivalDocuments = new List<ArrivalDocument>();
            this.BerthOccupationDocuments = new List<BerthOccupationDocument>();
            // this.DockingPlans = new List<DockingPlan>();
            this.DockingPlanDocuments = new List<DockingPlanDocument>();
            this.DredgingPriorityDocuments = new List<DredgingPriorityDocument>();
            this.IncidentDocuments = new List<IncidentDocument>();
            this.LicenseRequestDocuments = new List<LicenseRequestDocument>();
            this.PilotCertificates = new List<PilotCertificate>();
            this.PilotExemptionRequestDocuments = new List<PilotExemptionRequestDocument>();
            this.ServiceRequestDocuments = new List<ServiceRequestDocument>();
            this.ServiceRequestSailings = new List<ServiceRequestSailing>();
            this.VesselAgentChangeDocuments = new List<VesselAgentChangeDocument>();
            this.VesselArrestDocuments = new List<VesselArrestDocument>();
            this.VesselSAMSAStopDocuments = new List<VesselSAMSAStopDocument>();

            // -- Added by sandeep on 20-8-2014
            this.SuppDryDockDocuments = new List<SuppDryDockDocument>();
            this.SuppHotColdWorkPermitDocuments = new List<SuppHotColdWorkPermitDocument>();
            // -- end

            this.PermitRequestDocuments = new List<PermitRequestDocument>();
            this.PermitRequestVerifyedDocuments = new List<PermitRequestVerifyedDocument>();

        }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string DocumentPath { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FileType { get; set; }
        [DataMember]
        public byte[] Data { get; set; }
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
        public ICollection<AgentDocument> AgentDocuments { get; set; }
        [DataMember]
        public SubCategory SubCategory { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory1 { get; set; }
        [DataMember]
        public ICollection<ArrivalDocument> ArrivalDocuments { get; set; }
        [DataMember]
        public ICollection<BerthOccupationDocument> BerthOccupationDocuments { get; set; }
        [DataMember]
        //public  ICollection<DockingPlan> DockingPlans { get; set; }
        public ICollection<DockingPlanDocument> DockingPlanDocuments { get; set; }
        [DataMember]
        public ICollection<ServiceRequestDocument> ServiceRequestDocuments { get; set; }
        [DataMember]
        public ICollection<ServiceRequestSailing> ServiceRequestSailings { get; set; }
        [DataMember]
        public ICollection<VesselAgentChangeDocument> VesselAgentChangeDocuments { get; set; }
        [DataMember]
        public ICollection<DredgingPriorityDocument> DredgingPriorityDocuments { get; set; }
        //-- Added By  Srinivas Malepati, on 08 july 2014, to add new feature - VesselArrestImmobilizationSAMSAStop
        public ICollection<VesselArrestDocument> VesselArrestDocuments { get; set; }
        public ICollection<VesselSAMSAStopDocument> VesselSAMSAStopDocuments { get; set; }
        public ICollection<PilotCertificate> PilotCertificates { get; set; }
        public ICollection<PilotExemptionRequestDocument> PilotExemptionRequestDocuments { get; set; }
        public ICollection<LicenseRequestDocument> LicenseRequestDocuments { get; set; }
        [DataMember]
        public ICollection<PermitRequestDocument> PermitRequestDocuments { get; set; }

        [DataMember]
        public ICollection<PermitRequestVerifyedDocument> PermitRequestVerifyedDocuments { get; set; }
        [DataMember]
        public ICollection<IncidentDocument> IncidentDocuments { get; set; }

        // -- Added by sandeep on 20-8-2014

        [DataMember]
        public ICollection<SuppDryDockDocument> SuppDryDockDocuments { get; set; }
        [DataMember]
        public ICollection<SuppHotColdWorkPermitDocument> SuppHotColdWorkPermitDocuments { get; set; }

        // -- end
        //// -- Added by shankar on 01-nov-14
        public ICollection<PortContent> PortContents { get; set; }
        ////-- end


    }
}

