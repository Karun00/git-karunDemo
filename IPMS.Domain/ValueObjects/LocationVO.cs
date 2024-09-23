using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class LocationVO
    {
        [DataMember]
        public int LocationID { get; set; }

        //public virtual OtherLocation
        
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string LocationName { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string LocationPortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }

    }
}
