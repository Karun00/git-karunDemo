using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Berth : EntityBase
    {
        public Berth()
        {
            this.ArrivalCommodities = new List<ArrivalCommodity>();
            this.ArrivalNotifications = new List<ArrivalNotification>();
            this.ArrivalNotifications1 = new List<ArrivalNotification>();
            this.ArrivalNotifications2 = new List<ArrivalNotification>();

            this.BerthCargoes = new List<BerthCargo>();
            this.BerthMaintenances = new List<BerthMaintenance>();
            this.BerthReasonForVisits = new List<BerthReasonForVisit>();
            this.BerthVesselTypes = new List<BerthVesselType>();
            this.Bollards = new List<Bollard>();
            this.OtherServiceRecordings = new List<OtherServiceRecording>();
            this.ServiceRequestShiftings = new List<ServiceRequestShifting>();
            this.TerminalOperatorBerths = new List<TerminalOperatorBerth>();
            this.FuelReceipts = new List<FuelReceipt>();
            this.ShiftingBerthingTaskExecutions = new List<ShiftingBerthingTaskExecution>();
            this.ShiftingBerthingTaskExecutions1 = new List<ShiftingBerthingTaskExecution>();
            //this.ShiftingBerthingTaskExecutions2 = new List<ShiftingBerthingTaskExecution>();
            //this.ShiftingBerthingTaskExecutions3 = new List<ShiftingBerthingTaskExecution>();
            this.SuppDryDocks = new List<SuppDryDock>();

            // -- Added by sandeep on 21-8-2014
            this.SuppServiceRequests = new List<SuppServiceRequest>();
            this.DredgingOperations = new List<DredgingOperation>();
            // -- end
            this.SuppDryDocks = new List<SuppDryDock>();
            this.StatementCommodities = new List<StatementCommodity>();
            this.ExternalDivingRegisters = new List<ExternalDivingRegister>();
        }

        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public string BerthType { get; set; }
        [DataMember]
        public decimal FromMeter { get; set; }
        [DataMember]
        public decimal ToMeter { get; set; }
        [DataMember]
        public decimal Lengthm { get; set; }
        [DataMember]
        public decimal Draftm { get; set; }
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
        public Nullable<decimal> TidalDraft { get; set; }
        [DataMember]
        public  ICollection<ArrivalCommodity> ArrivalCommodities { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications1 { get; set; }

        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications2 { get; set; }

        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  Quay Quay { get; set; }
        [DataMember]
        public  ICollection<BerthCargo> BerthCargoes { get; set; }
        [DataMember]
        public  ICollection<Bollard> Bollards { get; set; }

        [DataMember]
        public  ICollection<ServiceRequestShifting> ServiceRequestShiftings { get; set; }
        [DataMember]
        public  ICollection<TerminalOperatorBerth> TerminalOperatorBerths { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenance> BerthMaintenances { get; set; }
        [DataMember]
        public  ICollection<BerthReasonForVisit> BerthReasonForVisits { get; set; }
        [DataMember]
        public  ICollection<BerthVesselType> BerthVesselTypes { get; set; }
        [DataMember]
        public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions { get; set; }
        [DataMember]
        public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions1 { get; set; }
        //[DataMember]
        //public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions2 { get; set; }
        //[DataMember]
        //public  ICollection<ShiftingBerthingTaskExecution> ShiftingBerthingTaskExecutions3 { get; set; }

        public string BerthKey()
        {
            return PortCode + "." + QuayCode + "." + BerthCode;
        }

        [DataMember]
        public  ICollection<FuelReceipt> FuelReceipts { get; set; }

        // -- Added by sandeep on 21-8-2014
        [DataMember]
        public  ICollection<SuppServiceRequest> SuppServiceRequests { get; set; }
        [DataMember]
        public  ICollection<DredgingOperation> DredgingOperations { get; set; }
        // -- end
        [DataMember]
        public  ICollection<SuppDryDock> SuppDryDocks { get; set; }
        [DataMember]
        public  ICollection<OtherServiceRecording> OtherServiceRecordings { get; set; }

        [DataMember]
        public ICollection<StatementCommodity> StatementCommodities { get; set; } 

        [DataMember]
        public ICollection<ExternalDivingRegister> ExternalDivingRegisters { get; set; }         
    }
}
