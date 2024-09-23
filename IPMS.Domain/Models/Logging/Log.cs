using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Log : EntityBase
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public System.DateTime Date { get; set; }

        [DataMember]
        public string Thread { get; set; }

        [DataMember]
        public string Level { get; set; }

        [DataMember]
        public string Logger { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Exception { get; set; }
    }
}
