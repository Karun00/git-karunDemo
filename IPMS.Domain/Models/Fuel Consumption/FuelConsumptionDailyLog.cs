using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class FuelConsumptionDailyLog : EntityBase
    {

        public int FuelConsumptionDailyLogID { get; set; }
        public string PortCode { get; set; }
        public int CraftID { get; set; }
        public decimal PreviousROB { get; set; }
        public decimal PresentROB { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public System.DateTime EndDateTime { get; set; }
        public decimal RunningHours { get; set; }
        public decimal AvgFuelConsumed { get; set; }
        public Nullable<decimal> FuelReceived { get; set; }   
        public string Remarks { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public decimal StartRunningHrs { get; set; }
        public decimal EndRunningHrs { get; set; }
        public virtual Craft Craft { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Port Port { get; set; }
    }
}
