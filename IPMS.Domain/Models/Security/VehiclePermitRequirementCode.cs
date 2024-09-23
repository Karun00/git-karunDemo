using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class VehiclePermitRequirementCode:EntityBase
       {
        public int PermitRequirementCodeID { get; set; }
        public int VehiclePermitID { get; set; }
        public string PermitRequirementCode { get; set; }
        public int PermitRequestID { get; set; }
        public virtual PermitRequest PermitRequest { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual VehiclePermit VehiclePermit { get; set; }
    }
}

