using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for MobileModule
    /// </summary>
    [DataContract]
    public partial class MobileModuleVO
    {
        [DataMember]
        public int ModuleID { get; set; }
        [DataMember]
        public Nullable<int> ParentModuleID { get; set; }
        [DataMember]
        public string ModuleName { get; set; }
        [DataMember]
        public string MobileReference { get; set; }
        [DataMember]
        public string IsMobile { get; set; }
        [DataMember]
        public string MobileImage { get; set; }
        [DataMember]
        public string PageUrl { get; set; }
        [DataMember]
        public Nullable<int> Count { get; set; }
        [DataMember]
        public bool DisplayCount { get; set; }
        [DataMember]
        public Nullable<int> OrderNo { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
