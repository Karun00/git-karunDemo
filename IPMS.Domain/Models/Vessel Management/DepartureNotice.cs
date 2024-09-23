using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class DepartureNotice : EntityBase
    {
        [DataMember]
        public int DepartureID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public int? VesselID { get; set; }
        [DataMember]
        public int AgentID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string Tidal { get; set; }
        [DataMember]
        public string DaylightRestriction { get; set; }
        [DataMember]
        public string NoMainEngine { get; set; }
        [DataMember]
        public string WillSheBeUnderTow { get; set; }
        [DataMember]
        public string TowingDetails { get; set; }
        [DataMember]
        public string CurrentBerth { get; set; }
        [DataMember]
        public string SideAlongSideCode { get; set; }
        [DataMember]
        public string IsVesselDoubleBank { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EstimatedDatetimeOfSR { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string IsFinal { get; set; }

        [DataMember]
        public virtual Agent Agent { get; set; }
        [DataMember]
        public virtual ArrivalNotification ArrivalNotification { get; set; }
        [DataMember]
        public virtual Port Port { get; set; }
        [DataMember]
        public virtual WorkflowInstance WorkflowInstance { get; set; }

        [NotMapped]
        public DateTime SubmissionDate { get; set; }
        [NotMapped]
        public string VesselName { get; set; }
        [NotMapped]
        public string VesselType { get; set; }
        [NotMapped]
        public string AgentName { get; set; }
        [NotMapped]
        public string EstimatedDatetimeOfSRConverted { get; set; }
    }
}
