using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class ReportBuilderVO
    {
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
        public string SearchFilter { get; set; }
        [DataMember]
        public string DisplayColumns { get; set; }
        [DataMember]
        public string OrderBy { get; set; }
        [DataMember]
        public string OrderByAD { get; set; }
        [DataMember]
        public string Parameters { get; set; }
        [DataMember]
        public string Result { get; set; }
        [DataMember]
        public string jsonText { get; set; }
        [DataMember]
        public string ColumnTypes { get; set; }
        [DataMember]
        public string QueryTemplateName { get; set; }
        [DataMember]
        public string ReportHeader { get; set; }
        [DataMember]
        public string DateFormat { get; set; }
    }
}
