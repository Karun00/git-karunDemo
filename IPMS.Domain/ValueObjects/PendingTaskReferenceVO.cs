using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class PendingTaskReferenceVO
    {
        [DataMember]
        public int WorkflowInstanceId { get; set; }
        [DataMember]
        public string WorkflowTaskCode { get; set; }
        [DataMember]
        public string ReferenceID { get; set; }
        [DataMember]
        public string ReferenceData { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string TaskName { get; set; }
        [DataMember]
        public string TaskDescription { get; set; }
        [DataMember]
        public string PreviousRemarks { get; set; }
        [DataMember]
        public string TaskCode { get; set; }
        [DataMember]
        public string APIUrl { get; set; }
        [DataMember]
        public string DateTimeConfigFormat { get; set; }

        [DataMember]
        public string HasRemarks { get; set; }


    }
}



