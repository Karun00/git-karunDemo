using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class IndividualVehiclePermit : EntityBase
    {
        public int IndividualVehiclePermitID { get; set; }
        public int PermitRequestID { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleRegnNo { get; set; }
        
        public string VehicleModel { get; set; }
        public string Chassis_VinNo { get; set; }
        public string Colour { get; set; }
        public virtual PermitRequest PermitRequest { get; set; }
    }
}
