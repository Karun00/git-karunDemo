using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class ServiceType : EntityBase
    {
        public ServiceType()
        {
            this.ResourceAllocationMovementTypeRules = new List<ResourceAllocationMovementTypeRule>();
            this.SuppMiscServices = new List<SuppMiscService>();

            // -- Added by sandeep on 27-09-2014
            this.ServiceTypeDesignations = new List<ServiceTypeDesignation>();
            // -- end
        }

        public int ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; }
        public Nullable<bool> IsCraft { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string IsServiceType { get; set; }
        public string ServiceUOM { get; set; }
        public  ICollection<ResourceAllocationMovementTypeRule> ResourceAllocationMovementTypeRules { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public string ServiceTypeCode { get; set; }
        public  SubCategory SubCategory { get; set; }
        public  ICollection<SuppMiscService> SuppMiscServices { get; set; }

        // -- Added by sandeep on 27-09-2014
        public  ICollection<ServiceTypeDesignation> ServiceTypeDesignations { get; set; }
        // -- end

    }
}
