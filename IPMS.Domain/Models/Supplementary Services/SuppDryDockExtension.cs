using System.Runtime.Serialization;
using Core.Repository;


namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SuppDryDockExtension : EntityBase
    {
      
        [DataMember]
        public int SuppDryDockExtensionID { get; set; }
        [DataMember]
        public int SuppDryDockID { get; set; }
        [DataMember]
        public System.Nullable<System.DateTime> ScheduleFromDate { get; set; }
        [DataMember]
        public System.Nullable<System.DateTime> ScheduleToDate { get; set; }
        [DataMember]
        public string ScheduleStatus { get; set; }
        [DataMember]
        public System.Nullable<System.DateTime> ExtensionDateTime { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string TermsandConditions { get; set; }
        [DataMember]
        public System.Nullable<int> WorkflowInstanceID { get; set; }
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
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual SuppDryDock SuppDryDock { get; set; }
        [DataMember]
        public virtual WorkflowInstance WorkflowInstance { get; set; }
        //[DataMember]
        //public virtual System.Collections.Generic.ICollection<SuppDryDockDocument> SuppDryDockDocument { get; set; }
        //[DataMember]
        //public virtual Document Document { get; set; }
    }
}
