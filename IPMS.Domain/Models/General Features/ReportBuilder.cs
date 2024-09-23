using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ReportBuilder : EntityBase
    {
        public ReportBuilder()
        {
            this.ReportQueryTemplates = new List<ReportQueryTemplate>();
        }

        [DataMember]
        public int ReportbuilderId { get; set; }
        [DataMember]
        public string Schemaname { get; set; }
        [DataMember]
        public string ReportCategory { get; set; }
        [DataMember]
        public string ReportObjectType { get; set; }
        [DataMember]
        public string ReportObjectName { get; set; }
        [DataMember]
        public string ReportDescription { get; set; }
        [DataMember]
        public string ReportQuery { get; set; }
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
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<ReportQueryTemplate> ReportQueryTemplates { get; set; }
    }
}
