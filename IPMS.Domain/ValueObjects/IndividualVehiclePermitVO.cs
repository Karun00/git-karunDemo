using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class IndividualVehiclePermitVO
    {

        public int IndividualVehiclePermitID { get; set; }
        public int PermitRequestID { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleRegnNo { get; set; }
      
        public string VehicleModel { get; set; }
        public string Chassis_VinNo { get; set; }
        public string Colour { get; set; }
       
    }
}
