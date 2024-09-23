using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public class QuayMasterDetails : EntityBase
    {
        //[DataMember]
        //public long QuayID { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string QuayName { get; set; }
        [DataMember]
        public string ShortName { get; set; }
        [DataMember]
        public decimal QuayLength { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        //public string StatusName { get; set; }
        //[DataMember]
        public decimal CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<long> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
