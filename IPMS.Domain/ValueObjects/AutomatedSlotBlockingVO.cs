using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class AutomatedSlotBlockingVO
    {
        [DataMember]
        public int AutomatedSlotBlockingId { get; set; }
        [DataMember]
        public string FromDate { get; set; }
        [DataMember]
        public string ToDate { get; set; }
        [DataMember]
        public string SlotFrom { get; set; }
        [DataMember]
        public string SlotTo { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Reason { get; set; }  
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public int TotalSlots { get; set; }
        [DataMember]
        public string Other { get; set; }
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
        public ICollection<SubCategory> Reasons { get; set; }
        [DataMember]
        public double StartTime { get; set; }
        [DataMember]
        public double EndTime { get; set; }
        [DataMember]
        public double ToStartTime { get; set; }
        [DataMember]
        public string ReasonName { get; set; }
        [DataMember]
        public string SlotPeriod { get; set; }
        [DataMember]
        public DateTime SlotFromDate { get; set; }
        [DataMember]
        public DateTime SlotToDate { get; set; }
        [DataMember]
        public int SlotNumber { get; set; }
        [DataMember]
        public bool EditVisible { get; set; }
        [DataMember]
        public ICollection<SlotVO> Slots { get; set; }

    }
}
