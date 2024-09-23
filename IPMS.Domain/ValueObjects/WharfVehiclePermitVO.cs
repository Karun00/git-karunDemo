using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class WharfVehiclePermitVO
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
        public string TelephoneNo { get; set; }
        public string Hometelephone { get; set; }
        public Nullable<int> ContractDuration { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }

    }
}
