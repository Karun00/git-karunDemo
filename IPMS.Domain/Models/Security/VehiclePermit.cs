using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class VehiclePermit : EntityBase
    {
        public int VehiclePermitID { get; set; }
        public int PermitRequestID { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleRegnNo { get; set; }
        public string PermitRequirementCode { get; set; }
        public string Reason { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual PermitRequest PermitRequest { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<VehiclePermitRequirementCode> VehiclePermitRequirementCodes { get; set; }
    }
}
