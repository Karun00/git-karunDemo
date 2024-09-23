using Core.Repository;
using System;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ReportQueryTemplate : EntityBase
    {
        [DataMember]
        public int QueryTemplateId { get; set; }
        [DataMember]
        public Nullable<int> ReportbuilderId { get; set; }
        [DataMember]
        public string QueryTemplateName { get; set; }
        [DataMember]
        public string ReportHeader { get; set; }
        [DataMember]
        public string UserQuery { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual User User1 { get; set; }
        [DataMember]
        public virtual ReportBuilder ReportBuilder { get; set; }

    }
}
