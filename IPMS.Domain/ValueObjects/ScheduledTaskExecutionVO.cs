using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ScheduledTaskExecutionVO
    {
        [DataMember]
        public string OperationType { get; set; }
        [DataMember]
        public string MomentType { get; set; }
        [DataMember]
        public string VCNName { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public int RecPKID { get; set; }
        [DataMember]
        public int ResourceAllocationID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StartTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PilotOnBoard { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PilotOff { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EndTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> WaitingStartTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> WaitingEndTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LineUp { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LineDown { get; set; }
        [DataMember]
        public string AdditionalTugs { get; set; }
        [DataMember]
        public string OffSteam { get; set; }
        [DataMember]
        public string MarineRevenueCleared { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Deficiencies { get; set; }
        [DataMember]
        public string FieldValue { get; set; }
        [DataMember]
        public string FieldName { get; set; }
        [DataMember]
        public int ValPKID { get; set; }
        [DataMember]
        public string FromBerthKey { get; set; }
        [DataMember]
        public string ToBerthKey { get; set; }
        [DataMember]
        public string FromBolardKey { get; set; }
        [DataMember]
        public string ToBolardKey { get; set; }
        [DataMember]
        public string MooringBolardBowKey { get; set; }
        [DataMember]
        public string MooringBolardStemKey { get; set; }
        [DataMember]
        public string BerthingSide { get; set; }
        [DataMember]
        public Nullable<decimal> OpeningMeterReading { get; set; }
        [DataMember]
        public Nullable<decimal> ClosingMeterReading { get; set; }
        [DataMember]
        public Nullable<decimal> TotalDispensed { get; set; }
        [DataMember]
        public string MeterSerialNo { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FirstSwing { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastSwing { get; set; }
        [DataMember]
        public Nullable<System.DateTime> TimeAlongSide { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FirstLineIn { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastLineIn { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FirstLineOut { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastLineOut { get; set; }
        [DataMember]
        public string ForwardDraft { get; set; }
        [DataMember]
        public string AftDraft { get; set; }
        [DataMember]
        public Nullable<decimal> VesselLength { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public string BerthKey { get; set; }
        [DataMember]
        public string DelayReason { get; set; }
    }
}
