using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ExternalDivingRegisterVO
    {
        [DataMember]
        public int ExternalDivingRegisterID { get; set; }
        [DataMember]
        public string DivingLogDateTime { get; set; }
        [DataMember]
        public int CompanyName { get; set; }
        [DataMember]
        public Nullable<int> LocationID { get; set; }
        [DataMember]
        public Nullable<int> VesselID { get; set; }
        [DataMember]
        public string PersonInCharge { get; set; }
        [DataMember]
        public string StartTime { get; set; }
        [DataMember]
        public string StopTime { get; set; }
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
        public string CompanyNameDisplay { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public bool isPermissionObtained { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        //Added By Omprakash For Notification on 16th Dec 2014
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthKey { get; set; }        
        [DataMember]
        public ICollection<BerthVO> Berths { get; set; }    
    }
}
