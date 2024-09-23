using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
     [DataContract]
    public class Section625DDetailVO
    {
       [DataMember]
        public int Section625DDetailID { get; set; }
       [DataMember]
       public int Section625DID { get; set; }
       [DataMember]
       public string GroupCode { get; set; }
       [DataMember]
       public string DetailCode { get; set; }
    }
}
