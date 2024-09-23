using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class BerthVO
    {
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string QuayName { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public string BerthType { get; set; }
        [DataMember]
        public string BerthTypeName { get; set; }
        [DataMember]
        public decimal QuayLength { get; set; }
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
        public string BerthKey { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public Nullable<decimal> TidalDraft { get; set; }
        [DataMember]
        public List<String> CargoType { get; set; }

        [DataMember]
        public List<String> VesselType { get; set; }

        [DataMember]
        public List<String> ReasonForVisitType { get; set; }

        [DataMember]
        public List<BollardVO> Bollards { get; set; }

        [DataMember]
        public string CargoDetails { get; set; }

        //using For Mobile
        [DataMember]
        public string FirstBolardName { get; set; }
        [DataMember]
        public string LastBollardName { get; set; }
       

    }
}
