using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models  
{
    public partial class Section625ABCD:EntityBase
    {
        public Section625ABCD()
        {
            this.Section625B = new List<Section625B>();
            this.Section625C = new List<Section625C>();
            this.Section625D = new List<Section625D>();
            this.Section625E = new List<Section625E>();
            this.Section625G = new List<Section625G>();
        }

        public int Section625ABCDID { get; set; }
        public string TOMSLogEntryNo { get; set; }
        public string OperatorName { get; set; }
        public string LincseNumber { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string SiteTerminal { get; set; }
        public Nullable<System.DateTime> ChangeControlDateTime { get; set; }
        public string CDName { get; set; }
        public string CDDesignation { get; set; }
        public string CDContactNumber { get; set; }
        public string CDMobileNumber { get; set; }
        public string CDEmailID { get; set; }
        public string CDAddress { get; set; }
        public string ChangeControlLicensedOperator { get; set; }
        public string AnticipatedImpactOnBBBEERating { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> Hour24Report625ID { get; set; }
        public  Hour24Report625 Hour24Report625 { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  ICollection<Section625B> Section625B { get; set; }
        public  ICollection<Section625C> Section625C { get; set; }
        public  ICollection<Section625D> Section625D { get; set; }
        public  ICollection<Section625E> Section625E { get; set; }
        public  ICollection<Section625G> Section625G { get; set; }
    }
}
