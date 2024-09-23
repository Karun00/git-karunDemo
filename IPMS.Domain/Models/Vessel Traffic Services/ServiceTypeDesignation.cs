using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class ServiceTypeDesignation : EntityBase
    {
        public ServiceTypeDesignation()
        {
        }

        public int ServiceTypeDesignationID { get; set; }
        public int ServiceTypeID { get; set; }
        public string PortCode { get; set; }
        public string DesignationCode { get; set; }
        public string CraftType { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual Port Port { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User { get; set; }
        public virtual SubCategory SubCategory1 { get; set; }
        public virtual User User1 { get; set; }
    }
}
