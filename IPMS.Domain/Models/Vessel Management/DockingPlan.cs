using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class DockingPlan : EntityBase
    {
        public DockingPlan()
        {
            this.DockingPlanDocuments = new List<DockingPlanDocument>();
        }
        [DataMember]
        public int DockingPlanID { get; set; }
        [DataMember]
        public int VesselID { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceID { get; set; }
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
        public string PortCode { get; set; }
        [DataMember]
        public string DockingPlanNo { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  Port Port { get; set; }
        [DataMember]
        public  Vessel Vessel { get; set; }
        [DataMember]
        public  WorkflowInstance WorkflowInstance { get; set; }
        [DataMember]
        public  ICollection<DockingPlanDocument> DockingPlanDocuments { get; set; }
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string IsFinal { get; set; }

        [NotMapped]
        public  string ReferenceNo { get; set; }
        [NotMapped]
        public  string VesselName { get; set; }
        [NotMapped]
        public  string VesselAgent { get; set; }
        [NotMapped]
        public  System.DateTime ApplicationDateTime { get; set; }
    }
}
