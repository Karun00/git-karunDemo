using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class DredgingOperation : EntityBase
    {
        public DredgingOperation()
        {
            this.BerthOccupationDocuments = new List<BerthOccupationDocument>();
        }

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
        public System.DateTime RequiredDate { get; set; }
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
        public Nullable<int> DPAWorkflowInstanceID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OccupationFrom { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OccupationTo { get; set; }
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
        public  Berth Berth { get; set; }
        [DataMember]
        public  ICollection<BerthOccupationDocument> BerthOccupationDocuments { get; set; }
        [DataMember]
        public  Craft Craft { get; set; }
        [DataMember]
        public  Location Location { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  WorkflowInstance WorkflowInstance { get; set; }
        [DataMember]
        public  WorkflowInstance WorkflowInstance1 { get; set; }
        [DataMember]
        public  WorkflowInstance WorkflowInstance2 { get; set; }
        [DataMember]
        public  DredgingPriority DredgingPriority { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        //[DataMember]
        //public  SubCategory SubCategory1 { get; set; }
        [DataMember]
        public  FinancialYear FinancialYear { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  SubCategory SubCategory2 { get; set; }
        [DataMember]
        public string DredgerName { get; set; }
        [DataMember]
        public Nullable<System.DateTime> VolumeOccupationFrom { get; set; }
        [DataMember]
        public Nullable<System.DateTime> VolumeOccupationTo { get; set; }
        [DataMember]
        public string VolumeOccupationDuration { get; set; }

        [NotMapped]
        public string DredgingMaterial { get; set; }
        [NotMapped]
        public string AreaName { get; set; }

        [NotMapped]
        public string Month { get; set; }
        [NotMapped]
        public string RequireDate { get; set; }

        [NotMapped]
        public string PortName { get; set; }


    }
}

