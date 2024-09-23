using Core.Repository;
using System;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ReportQueryLookup : EntityBase
    {
  
        [DataMember]
        public string LookupColumnname { get; set; }
        [DataMember]
        public string LookupName { get; set; }
       

    }
}
