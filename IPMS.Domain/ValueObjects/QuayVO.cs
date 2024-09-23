using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{

    [DataContract]
    public class QuayVO
    {
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public string QuayName { get; set; }
        [DataMember]
        public decimal QuayLength { get; set; }
        [DataMember]
        public string Description { get; set; }
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
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string QuayKey { get; set; }
        [DataMember]
        public List<BerthVO> berthlist { get; set; }
        [DataMember]
        public List<BollardVO> bollardlist { get; set; }

        [DataMember]
        public List<String> CargoType { get; set; }


    }
}
