
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class BerthMasterDetails : EntityBase
    {
        
         [DataMember]
        public string BerthCode { get; set; }
         [DataMember]
        public string BerthName { get; set; }
         [DataMember]
        public string ShortName { get; set; }
         [DataMember]
         public string PortCode { get; set; }
         [DataMember]
         public string QuayCode { get; set; }
         [DataMember]
        public string PortName { get; set; }
         [DataMember]
        public string QuayName { get; set; }
         [DataMember]
        public string BerthType { get; set; }
         [DataMember]
         public string BerthType1 { get; set; }
         [DataMember]
        public decimal FromMeter { get; set; }
         [DataMember]
        public decimal ToMeter { get; set; }
         [DataMember]
        public decimal Lengthm { get; set; }
         [DataMember]
        public decimal Draftm { get; set; }
         [DataMember]
        public string RecordStatus { get; set; }
         [DataMember]
         public DateTime CreatedDate { get; set; }
         [DataMember]
         public string BerthTypeName { get; set; }
      

    }
}
