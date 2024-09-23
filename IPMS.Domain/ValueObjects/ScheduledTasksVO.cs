using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ScheduledTasksVO
    {
        [DataMember]
        public int ResourceAllocationID { get; set; }
        [DataMember]
        public string ServiceReferenceType { get; set; }
        [DataMember]
        public string ServiceReferenceTypeName { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public int ServiceRequestID { get; set; }
        [DataMember]
        public string OperationType { get; set; }
        [DataMember]
        public string OperationTypeName { get; set; }
        [DataMember]
        public Nullable<int> ResourceID { get; set; }
        [DataMember]
        public string ResourceType { get; set; }
        [DataMember]
        public string ResourceTypeName { get; set; }
        [DataMember]
        public string StartTime { get; set; }
        [DataMember]
        public string EndTime { get; set; }
        [DataMember]
        public string TaskStatus { get; set; }
        [DataMember]
        public string TaskStatusName { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public string MovementTypes { get; set; }
        [DataMember]
        public decimal momentTimeDue { get; set; }
        [DataMember]
        public string IsExecute { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string FromBetrth { get; set; }
    }

    [DataContract]
    public class ScheduledTasksViewVO
    {
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string OperationType { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string LOA { get; set; }
        [DataMember]
        public string Draft { get; set; }
        [DataMember]
        public string VesselType { get; set; }
        [DataMember]
        public string GRT { get; set; }
        [DataMember]
        public string IMDG { get; set; }
        [DataMember]
        public string Daylight { get; set; }
        [DataMember]
        public string Tidal { get; set; }
        [DataMember]
        public string Movement { get; set; }
        [DataMember]
        public string FromBetrth { get; set; }
        [DataMember]
        public string ToBerth { get; set; }
        [DataMember]
        public string SpaceonBerth { get; set; }
        [DataMember]
        public string MovementTime { get; set; }
        [DataMember]
        public string Alongside { get; set; }
        [DataMember]
        public string Warp { get; set; }
        [DataMember]
        public string Quantityintons { get; set; }
        [DataMember]
        public string MovementType { get; set; }
        [DataMember]
        public List<string> tasks { get; set; }
        [DataMember]
        public string SideAlongSide { get; set; }
        [DataMember]
        public string IsTidal { get; set; }
        [DataMember]
        public string OwnSteam { get; set; }
        [DataMember]
        public string Nomainengine { get; set; }

    }
}
