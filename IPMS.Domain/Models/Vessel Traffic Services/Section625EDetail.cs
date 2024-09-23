using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class Section625EDetail:EntityBase
    {
        public int Section625EDetailID { get; set; }
        public int Section625EID { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> ReplacementValue { get; set; }
        public int Hour24Report625ID { get; set; }
        public virtual Hour24Report625 Hour24Report625 { get; set; }
        public virtual Section625E Section625E { get; set; }
    }
}
