using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    public partial class BollardMasterDetails : EntityBase
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
        public string BerthCode { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public decimal FromMeter { get; set; }
        [DataMember]
        public decimal ToMeter { get; set; }
        [DataMember]
        public string Continous { get; set; }       
        [DataMember]
        public string Description { get; set; }        
        [DataMember]
        public string RecordStatus { get; set; }
      


    }
}
