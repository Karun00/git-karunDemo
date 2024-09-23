using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ReportQueryDataTypeOperatorVO
    {
        [DataMember]
        public int OperatorId { get; set; }
        [DataMember]
        public string ApplicableDataType { get; set; }

    }
}
