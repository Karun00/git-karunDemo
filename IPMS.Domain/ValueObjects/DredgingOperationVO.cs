using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class DredgingOperationVO 
    {
        [DataMember]
        public int DredgingOperationID { get; set; }
        [DataMember]
        public int DredgingPriorityID { get; set; }
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public Nullable<int> AreaLocationID { get; set; }
        [DataMember]
        public string TypeCode { get; set; }
        [DataMember]
        public string RequiredDate { get; set; }
        [DataMember]
        public Nullable<decimal> DesignDepth { get; set; }
        [DataMember]
        public Nullable<decimal> PromulgateDepth { get; set; }
        [DataMember]
        public decimal Requirement { get; set; }
        [DataMember]
        public string DPARemarks { get; set; }
        [DataMember]
        public string AreaType { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string BerthKey { get; set; }
        [DataMember]
        public Nullable<int> DPAWorkflowInstanceID { get; set; }
        [DataMember]
        public string OccupationFrom { get; set; }
        [DataMember]
        public string OccupationTo { get; set; }
        [DataMember]
        public string OccupationDuration { get; set; }
        [DataMember]
        public Nullable<int> DOWorkflowInstanceID { get; set; }
        [DataMember]
        public Nullable<decimal> Volume { get; set; }
        [DataMember]
        public Nullable<int> CraftID { get; set; }
        [DataMember]
        public string DredgingTask { get; set; }
        [DataMember]
        public string DredgingDelay { get; set; }
        [DataMember]
        public string DVRemarks { get; set; }
        [DataMember]
        public Nullable<int> DVWorkflowInstanceID { get; set; }
        [DataMember]
        public string DredgingStatus { get; set; }
        [DataMember]
        public string IsDPAFinal { get; set; }
        [DataMember]
        public string IsDOFinal { get; set; }
        [DataMember]
        public string IsDVFinal { get; set; }
        [DataMember]
        public Nullable<int> FinancialYearID { get; set; }
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
        public virtual List<BerthOccupationDocumentVO> BerthOccupationDocumentVO { get; set; }
        [DataMember]
        public string AreaName { get; set; }
        [DataMember]
        public string DredgingMaterial { get; set; }        
        [DataMember]
        public string FinancialYearDate { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public string LocationName { get; set; }
        [DataMember]
        public string ToDate { get; set; }
        [DataMember]
        public string FromDate { get; set; }
         [DataMember]
        public string TypeName { get; set; }
         [DataMember]
         public string RequireDate { get; set; }
        [DataMember]
         public string DredgerName { get; set; }
        [DataMember]
        public string VolumeOccupationFrom { get; set; }
        [DataMember]
        public string VolumeOccupationTo { get; set; }
        [DataMember]
        public string VolumeOccupationDuration { get; set; }

        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string workflowRemarks { get; set; }
    }
}
