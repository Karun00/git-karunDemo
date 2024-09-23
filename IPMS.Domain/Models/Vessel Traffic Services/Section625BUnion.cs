using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models 
{
    public partial class Section625BUnion:EntityBase
    {
        public int Section625BUnionID { get; set; }
        public int Section625BID { get; set; }
        public string UnionName { get; set; }
        public int TotalMembership { get; set; }
        public Nullable<int> TotalRosteredForShift { get; set; }
        public Nullable<int> TotalPresent { get; set; }
        public Nullable<int> TotalStrike { get; set; }
        public Nullable<int> TotalLeave { get; set; }
        public Nullable<int> TotalSick { get; set; }
        public Nullable<int> ReplacementLeave { get; set; }
        public int Hour24Report625ID { get; set; }
        public virtual Hour24Report625 Hour24Report625 { get; set; }
        public virtual Section625B Section625B { get; set; }
    }
}
