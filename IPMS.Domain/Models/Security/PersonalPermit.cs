using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class PersonalPermit : EntityBase
    {
        public int PersonalPermitID { get; set; }
        public int PermitRequestID { get; set; }
        public string  PermitCategoryCode { get; set; }
        public string  AllNPASites { get; set; }
        public string  SpecificNPASites { get; set; }
        public string  SpecifyArea { get; set; }
        public string  LeaseholdSite { get; set; }
        public string  PhysicalAddress { get; set; }
        public string  AdhocPermits { get; set; }
        public string  TemporaryPermits { get; set; }
        public string  AllPorts { get; set; }
        public string  ConstructionArea { get; set; }
        public string  PermanentPermits { get; set; }
        public string  Reason { get; set; }
        public string  RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string  permittype { get; set; }
      

        public virtual PermitRequest PermitRequest { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual SubCategory SubCategory1 { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual SubCategory SubCategory2 { get; set; }
        public virtual SubCategory SubCategory3 { get; set; }
    }
}
