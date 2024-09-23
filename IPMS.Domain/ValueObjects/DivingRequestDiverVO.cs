using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class DivingRequestDiverVO
    {
        [DataMember]
        public int DivingRequestDiverID { get; set; }
        [DataMember]
        public int DivingRequestID { get; set; }
        [DataMember]
        public string DiverName { get; set; }
        [DataMember]
        public string TimeLeftSurface { get; set; }
        [DataMember]
        public string TimeArrivedSurface { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }

        // -- Added by sandeep on 07-08-2014

        [DataMember]
        public string DiverType { get; set; }

        // -- end

    }
}