using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ReportQueryOperatorVO
    {
        [DataMember]
        public int OperatorId { get; set; }
        [DataMember]
        public string OperatorName { get; set; }
        [DataMember]
        public string OperatorValue { get; set; }
    }
}
