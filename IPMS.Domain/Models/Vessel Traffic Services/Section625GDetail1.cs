using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class Section625GDetail1:EntityBase
    {
        public int Section625GDetail1ID { get; set; }
        public int Section625GID { get; set; }
        public string RISubCatCode { get; set; }
        public int Hour24Report625ID { get; set; }
        public virtual Hour24Report625 Hour24Report625 { get; set; }
        public virtual Section625G Section625G { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
