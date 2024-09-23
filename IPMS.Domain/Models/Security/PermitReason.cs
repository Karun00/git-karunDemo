using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class PermitReason : EntityBase
    {
        public int PermitReasonID { get; set; }
        public int PermitRequestID { get; set; }
        public string ReasonCode { get; set; }
      
        public virtual PermitRequest PermitRequest { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        
    }
}