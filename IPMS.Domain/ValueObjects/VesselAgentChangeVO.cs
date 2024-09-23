using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using IPMS.Domain.Models;
namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class VesselAgentChangeVO
    {

        [DataMember]
        public int VesselAgentChangeID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public int ProposedAgent { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string ReasonForTransferCode { get; set; }

        [DataMember]
        public string ReasonForVisit { get; set; }
        //[DataMember]
        //public List<string> ReasonForVisitName { get; set; }
        [DataMember]
        public string ReasonForVisitName { get; set; }
        [DataMember]
        //public DateTime EffectiveDateTime { get; set; }
        public string EffectiveDateTime { get; set; }

        [DataMember]
        public virtual User CreatedUser { get; set; }
        [DataMember]
        public virtual User ModifiedUser { get; set; }
        [DataMember]
        public virtual AgentVO Agent { get; set; }
        [DataMember]
        public virtual SubCategoryVO SubCategory { get; set; }
        [DataMember]
        public virtual ArrivalNotificationVO ArrivalNotification { get; set; }
        [DataMember]
        public virtual WorkflowInstance WorkflowInstatnce { get; set; }
        
        [DataMember]
        public virtual VesselVO Vessel { get; set; }
        [DataMember]
        public string AnyDangerousGoodsonBoard { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        [DataMember]
        public Nullable<int> CurrentAgentID { get; set; }
        [DataMember]
        public virtual string ProposedAgentName { get; set; }
        [DataMember]
        public virtual string RequestedAgentName { get; set; }
        [DataMember]
        public string WorkFlowStatus { get; set; }
        [DataMember]
        public List<VesselAgentChangeDocumentVO> VesselAgentChangeDocuments { get; set; }


        internal Models.VesselAgentChange MapToEntity()
        {
            throw new NotImplementedException();
        }
    }
}
