using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    public partial class PermitRequestAccessGates:EntityBase
    { 
        public int PermitRequestAccessGatesID { get; set; }
        public int WharfVehiclePermitID { get; set; }
        public string AccessGates { get; set; }
        public int PermitRequestID { get; set; }
        public virtual PermitRequest PermitRequest { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual WharfVehiclePermit WharfVehiclePermit { get; set; }
    }
}

