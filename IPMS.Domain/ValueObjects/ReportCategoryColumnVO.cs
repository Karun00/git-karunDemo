using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ReportCategoryColumnVO    
    {
        [DataMember]
        public string COLUMN_NAME { get; set; }
        [DataMember]
        public string DATA_TYPE { get; set; }
        [DataMember]
        public string query { get; set; }
    }
}
