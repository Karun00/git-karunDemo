using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ReportQueryDataTypeOperator : EntityBase
    {
        [DataMember]
        public int OperatorId { get; set; }
        [DataMember]
        public string ApplicableDataType { get; set; }
        [DataMember]
        public virtual ReportQueryOperator ReportQueryOperator { get; set; }


    }
}
