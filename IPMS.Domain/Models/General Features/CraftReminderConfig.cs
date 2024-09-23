using Core.Repository;
using System;
using System.Collections.Generic;

namespace IPMS.Domain.Models
{
    public partial class CraftReminderConfig : EntityBase
    {
        public int CraftReminderConfigID { get; set; }
        public int CraftID { get; set; }
        public string ReminderName { get; set; }
        public string ParticularsNo { get; set; }
        public string IssuingAuthority { get; set; }
        public System.DateTime DateOfIssue { get; set; }
        public System.DateTime DateOfValidity { get; set; }
        public Nullable<int> AlertOccurance1 { get; set; }
        public string AlertPeriod1 { get; set; }
        public Nullable<int> AlertOccurance2 { get; set; }
        public string AlertPeriod2 { get; set; }
        public Nullable<int> AlertOccurance3 { get; set; }
        public string AlertPeriod3 { get; set; }
        public string ReminderStatus { get; set; }
        public string ExitReminderConfig { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual Craft Craft { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual SubCategory SubCategory1 { get; set; }
        public virtual SubCategory SubCategory2 { get; set; }
        public virtual User User { get; set; }
        public virtual SubCategory SubCategory3 { get; set; }
        public virtual User User1 { get; set; }
        public virtual SubCategory SubCategory4 { get; set; }
        public virtual SubCategory SubCategory5 { get; set; }
    }
}
