using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class PermitRequestArea : EntityBase
    {
        public PermitRequestArea()
        {
          //  this.PermitRequestSubAreas = new List<PermitRequestSubArea>();
        }
        public int PermitRequestAreaID { get; set; }
        public int PermitRequestID { get; set; }
        public string PermitRequestAreaCode { get; set; }

      //  public ICollection<PermitRequestSubArea> PermitRequestSubAreas { get; set; }

        public virtual PermitRequest PermitRequest { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
