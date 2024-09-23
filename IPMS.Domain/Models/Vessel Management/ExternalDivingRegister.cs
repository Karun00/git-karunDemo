using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ExternalDivingRegister : EntityBase
    {
        [DataMember]
        public int ExternalDivingRegisterID { get; set; }
        [DataMember]
        public System.DateTime DivingLogDateTime { get; set; }
        [DataMember]
        public int CompanyName { get; set; }      
        [DataMember]
        public Nullable<int> VesselID { get; set; }
        [DataMember]
        public string PersonInCharge { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StartTime { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StopTime { get; set; }
        [DataMember]
        public string OnsiteSupervisorContNo { get; set; }
        [DataMember]
        public string OffsiteSupervisorContNo { get; set; }
        [DataMember]
        public string ClearanceNo { get; set; }
        [DataMember]
        public Nullable<int> NoOfDrivers { get; set; }
        [DataMember]
        public string PermissionObtained { get; set; }
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
        public virtual Vessel Vessel { get; set; }
        [DataMember]
        public virtual Berth Berth { get; set; }
    }
}
