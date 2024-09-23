using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class Section625DDetail:EntityBase
    {
        public int Section625DDetailID { get; set; }
        public int Section625DID { get; set; }
        public string GroupCode { get; set; }
        public string DetailCode { get; set; }
        public int Hour24Report625ID { get; set; }
        public virtual Hour24Report625 Hour24Report625 { get; set; }
        public virtual Section625D Section625D { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
