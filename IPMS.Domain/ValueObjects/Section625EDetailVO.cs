using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class Section625EDetailVO
    {
        [DataMember]
        public int Section625EDetailID { get; set; }
        [DataMember]
        public int Section625EID { get; set; }
        [DataMember]
        public string Item { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public Nullable<int> ReplacementValue { get; set; }
    }
}
