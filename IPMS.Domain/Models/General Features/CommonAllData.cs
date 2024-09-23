using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
    [DataContract]
    public  class CommonAllData : EntityBase
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ModelName { get; set; }
        [DataMember]
        public string ModelData { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public int? ModifiedBy { get; set; }
        [DataMember]
        public DateTime? ModifiedDate { get; set; }  
    }
}
