using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    
    [DataContract]
    public partial class FuelConsumptionDailyLogVO
    {
        [DataMember]
        public int FuelConsumptionDailyLogID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public int CraftID { get; set; }
        [DataMember]
        public string PreviousROB { get; set; }
        [DataMember]
        public string PresentROB { get; set; }
        [DataMember]
        public string StartRunningHrs { get; set; }
        [DataMember]
        public string EndRunningHrs { get; set; }
        [DataMember]
        public string RunningHours { get; set; }
        [DataMember]
        public string AvgFuelConsumed { get; set; }
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
        public CraftVO Crafts { get; set; }
        [DataMember]
        public string FuelReceived { get; set; }
        [DataMember]
        public string StartDateTime { get; set; }
        [DataMember]
        public string EndDateTime { get; set; }
 
    }
}
