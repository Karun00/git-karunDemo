using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
  public  class Section625BUnionVO
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
        //public Section625BVO Section625B { get; set; }
    }
}
