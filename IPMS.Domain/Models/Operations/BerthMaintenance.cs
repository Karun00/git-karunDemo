using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    public partial class BerthMaintenance : EntityBase
    {
        public BerthMaintenance()
        {
            this.BerthMaintenanceApprovals = new List<BerthMaintenanceApproval>();
            this.BerthMaintenanceCompletions = new List<BerthMaintenanceCompletion>();
        }

        public int BerthMaintenanceID { get; set; }
        public string PortCode { get; set; }
        public string ProjectNo { get; set; }
        public string MaintenanceTypeCode { get; set; }
        public string MaintPortCode { get; set; }
        public string MaintQuayCode { get; set; }
        public string MaintBerthCode { get; set; }
        public string FromPortCode { get; set; }
        public string FromQuayCode { get; set; }
        public string FromBerthCode { get; set; }
        public string FromBollard { get; set; }
        public string ToPortCode { get; set; }
        public string ToQuayCode { get; set; }
        public string ToBerthCode { get; set; }
        public string ToBollard { get; set; }
        public System.DateTime PeriodFrom { get; set; }
        public System.DateTime PeriodTo { get; set; }
        public string OccupationTypeCode { get; set; }
        public string Precinct { get; set; }
        public string DisciplineCode { get; set; }
        public string SpecialConditions { get; set; }
        public string Description { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string BerthMaintenanceNo { get; set; }
        
        public Nullable<int> WorkflowInstanceId { get; set; }
        public  Berth Berth { get; set; }
        public  User User { get; set; }
        public  SubCategory SubCategory { get; set; }
        public  Bollard Bollard { get; set; }
        public  SubCategory SubCategory1 { get; set; }
        public  User User1 { get; set; }
        public  Port Port { get; set; }
        public  Bollard Bollard1 { get; set; }
        public  WorkflowInstance WorkflowInstance { get; set; }
        public  ICollection<BerthMaintenanceApproval> BerthMaintenanceApprovals { get; set; }
        public  ICollection<BerthMaintenanceCompletion> BerthMaintenanceCompletions { get; set; }

        [NotMapped]
        public string  MaintenanceType { get; set; }
        [NotMapped]
        public string BerthName { get; set; }
        [NotMapped]
        public string BollardsFrom { get; set; }
        [NotMapped]
        public string BollardsTo { get; set; }
        [NotMapped]
        public string ReferenceNo { get; set; }
        
    }
}
