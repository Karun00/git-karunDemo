using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class PermitRequestSubArea : EntityBase
    {
        public int PermitRequestSubAreaID { get; set; }
        public int PermitRequestID { get; set; }      
        public string PermitRequestSubAreaCode { get; set; }
        public string PermitRequestAreaCode { get; set; }

        public virtual PermitRequest PermitRequest { get; set; }      
        public virtual SubCategory SubCategory { get; set; }
        public virtual SuperCategory SuperCategory { get; set; }
        
    }
}

