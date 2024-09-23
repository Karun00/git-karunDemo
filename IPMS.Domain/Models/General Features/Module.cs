using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Module : EntityBase
    {
        public Module()
        {
            this.Entities = new List<Entity>();
            this.Module1 = new List<Module>();
        }
        [DataMember]
        public int ModuleID { get; set; }
        [DataMember]
        public Nullable<int> ParentModuleID { get; set; }
        [DataMember]
        public string ModuleName { get; set; }
        [DataMember]
        public string IsMobile { get; set; }
        [DataMember]
        public string MobileImage { get; set; }
        [DataMember]
        public string PageUrl { get; set; }
        [DataMember]
        public Nullable<int> OrderNo { get; set; }


        [DataMember]
        public string MobileReference { get; set; }

        [DataMember]
        public Nullable<int> Count { get; set; }
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
        [DataMember]
        public  ICollection<Entity> Entities { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<Module> Module1 { get; set; }
        [DataMember]
        public  Module Module2 { get; set; }
    }
}
