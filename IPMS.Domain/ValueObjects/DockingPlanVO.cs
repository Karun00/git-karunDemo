using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class DockingPlanVO
    {
        [DataMember]
        public int DockingPlanID { get; set; }
        [DataMember]
        public int VesselID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
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
        public string CallSign { get; set; }
        [DataMember]
        public string workflowRemarks { get; set; }
        [DataMember]
        public string IsConfirmCancel { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string VesslType { get; set; }        
        [DataMember]
        public string IMONo { get; set; }
        [DataMember]
        public string VesselType { get; set; }
        [DataMember]
        public Nullable<decimal> LengthOverallInM { get; set; }
        [DataMember]
        public Nullable<decimal> BeamInM { get; set; }
        [DataMember]
        public string PortOfRegistry { get; set; }
        [DataMember]
        public string DockingPlanStatus { get; set; }
        [DataMember]
        public Nullable<decimal> VesselGRT { get; set; }

        [DataMember]
        public string DockingPlanNo { get; set; }
        [DataMember]
        public string ReferenceNo { get; set; }


        [DataMember]
        public string IsFinal { get; set; }        

        [DataMember]
        public bool EditVisible { get; set; }

        [DataMember]
        public string AgentName { get; set; }
        [DataMember]
        public System.DateTime ApplicationDateTime { get; set; }

        [DataMember]
        public List<DockingPlanDocumentVO> DockingPlanDocumentsVO { get; set; }
    }

    public class DockingPlanDocumentVO
    {        
        [DataMember]
        public int DockingPlanDocumentID { get; set; }           
        [DataMember]
        public int DockingPlanID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string FileName { get; set; }
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
        public string DocumentTypeName { get; set; }
    }

    public class DockingPlanUserVO
    {
        [DataMember]
        public int UserTypeId { get; set; }
        [DataMember]
        public string  AgentName { get; set; }    

    }
}
