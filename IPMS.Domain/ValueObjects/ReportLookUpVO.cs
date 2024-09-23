using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{

    [DataContract]
    public class ReportLookUpVO : EntityBase
    {
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public string SearchText { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string query { get; set; }

    }
}
