using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class BollardVO
    {

        [DataMember]
        public string BollardCode { get; set; }
        [DataMember]
        public string BollardName { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string QuayName { get; set; }
        [DataMember]
        public decimal QuayLength { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public decimal BerthLength { get; set; }
        [DataMember]
        public string FromBollardKey { get; set; }
        [DataMember]
        public string ToBollardKey { get; set; }
        [DataMember]
        public decimal FromMeter { get; set; }
        [DataMember]
        public decimal ToMeter { get; set; }
        [DataMember]
        public string Continous { get; set; }
        [DataMember]
        public bool ContinousStatus { get; set; }
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
        public string BolardKey { get; set; }



    }
}
