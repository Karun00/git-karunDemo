using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Core.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppDryDock : EntityBase
    {
        public SuppDryDock()
        {
            this.SuppDryDockDocuments = new List<SuppDryDockDocument>();
            this.SuppMiscServices = new List<SuppMiscService>();
            this.SuppDryDockExtensions = new List<SuppDryDockExtension>();
        }

        [DataMember]
        public int SuppDryDockID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public System.DateTime FromDate { get; set; }
        [DataMember]
        public System.DateTime ToDate { get; set; }
        [DataMember]
        public string BarkeelCode { get; set; }
        [DataMember]
        public Nullable<int> CargoTons { get; set; }
        [DataMember]
        public Nullable<int> Ballast { get; set; }
        [DataMember]
        public Nullable<int> Bunkers { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ExtensionDateTime { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string TermsandConditions { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceID { get; set; }
        [DataMember]
        public string DockPortCode { get; set; }
        [DataMember]
        public string DockQuayCode { get; set; }
        [DataMember]
        public string DockBerthCode { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ScheduleFromDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ScheduleToDate { get; set; }
        [DataMember]
        public string ScheduleStatus { get; set; }
        [DataMember]
        public string Chamber { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EnteredDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OnBlocksDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DryDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FinishedDockDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OffBlocksDateTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LeftDockDateTime { get; set; }
        [DataMember]
        public  ArrivalNotification ArrivalNotification { get; set; }
        [DataMember]
        //public  Port Port { get; set; }
        public  Berth Berth { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  WorkflowInstance WorkflowInstance { get; set; }
        [DataMember]
        public  ICollection<SuppDryDockDocument> SuppDryDockDocuments { get; set; }
        [DataMember]
        public  ICollection<SuppMiscService> SuppMiscServices { get; set; }
        //[DataMember]
        //public  ICollection<SuppDockUnDockTime> SuppDockUnDockTimes { get; set; }


        [NotMapped]
        public  string VesselName { get; set; }
        [NotMapped]
        public  string VesselAgent { get; set; }
        [NotMapped]
        public  System.DateTime ApplicationDateTime { get; set; }

        [DataMember]
        public  ICollection<SuppDryDockExtension> SuppDryDockExtensions { get; set; }


        //-- Added by sandeep on 05-02-2015
        [NotMapped]
        public  string PortCode { get; set; }
        [NotMapped]
        public  string Comments { get; set; }
        [NotMapped]
        public  int UserTypeId { get; set; }
        //-- end
    }
}
