using Core.Repository;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ReportQueryOperator : EntityBase
    {
        public ReportQueryOperator()
        {
            this.ReportQueryDataTypeOperators = new List<ReportQueryDataTypeOperator>();
        }
        [DataMember]
        public int OperatorId { get; set; }
        [DataMember]
        public string OperatorName { get; set; }
        [DataMember]
        public string OperatorValue { get; set; }
        [DataMember]
        public  ICollection<ReportQueryDataTypeOperator> ReportQueryDataTypeOperators { get; set; }


    }
}
