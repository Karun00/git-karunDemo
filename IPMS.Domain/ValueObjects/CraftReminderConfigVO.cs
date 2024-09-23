using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for CraftReminderConfig Details
    /// </summary>
    
    [DataContract]
    public class CraftReminderConfigVO
    {
        [DataMember]
        public int CraftReminderConfigID { get; set; }
        [DataMember]
        public int CraftID { get; set; }
        [DataMember]
        public string ReminderName { get; set; }
        [DataMember]
        public string ParticularsName { get; set; }
        [DataMember]
        public string ParticularsNo { get; set; }
        [DataMember]
        public string IssuingAuthority { get; set; }
        [DataMember]
        public System.DateTime DateOfIssue { get; set; }
        [DataMember]
        public System.DateTime DateOfValidity { get; set; }
        [DataMember]
        public Nullable<int> AlertOccurance1 { get; set; }
        [DataMember]
        public string AlertPeriod1 { get; set; }
        [DataMember]
        public Nullable<int> AlertOccurance2 { get; set; }
        [DataMember]
        public string AlertPeriod2 { get; set; }
        [DataMember]
        public Nullable<int> AlertOccurance3 { get; set; }
        [DataMember]
        public string AlertPeriod3 { get; set; }
        [DataMember]
        public string ReminderStatus { get; set; }
        [DataMember]
        public string ExitReminderConfig { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public CraftVO Craft { get; set; }


    }
}
