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
    public class GroupedPendingTaskVO
    {
        [DataMember]
        public string EntityCode { get; set; }

        [DataMember]
        public string EntityName { get; set; }

        [DataMember]
        public string TaskName { get; set; }

        [DataMember]
        public string TaskCode { get; set; }

        [DataMember]
        public string TaskDescription { get; set; }

        [DataMember]
        public string PreviousRemarks { get; set; }

        [DataMember]
        public string APIUrl { get; set; }

        [DataMember]
        public string PageUrl { get; set; }

        [DataMember]
        public string SubCatName { get; set; }

        [DataMember]
        public List<PendingTaskReferenceVO> pendingTasks { get; set; }

        [DataMember]
        public string EntityColumns { get; set; }
        [DataMember]
        public string RequestName { get; set; }
    }
}



