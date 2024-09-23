using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class WharfVehiclePermit:EntityBase
    {
        public int WharfVehiclePermitID { get; set; }
        public int PermitRequestID { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleRegnNo { get; set; }
        public string VehicleDescription { get; set; }
        public string VehicleRegisterd { get; set; }
        public string VehiclePointed { get; set; }
        public string Reason { get; set; }
        public string MobileNo { get; set; }
        public string PermitRequirement { get; set; }
        public string ContractorNo { get; set; }
        public string TemporaryPermits { get; set; }
        public string AccessGates { get; set; }
        public string OtherSpecify { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string TelephoneNo { get; set; }
        public Nullable<int> ContractDuration { get; set; }
        public string Hometelephone { get; set; }
        public virtual PermitRequest PermitRequest { get; set; }
        public virtual ICollection<PermitRequestAccessGates> PermitRequestAccessGates { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual SubCategory SubCategory1 { get; set; }
        public virtual SubCategory SubCategory2 { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
