using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class AgentPortsVO
    {        
        public int AgentPortID { get; set; }        
        public int AgentID { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public string PortCode { get; set; }        
        public string WFStatus { get; set; }        
        public int VerifiedBy { get; set; }        
        public Nullable<System.DateTime> VerifiedDate { get; set; }        
        public int ApprovedBy { get; set; }        
        public Nullable<System.DateTime> ApprovedDate { get; set; }        
        public string RejectComments { get; set; }        
        public string RecordStatus { get; set; }        
        public int CreatedBy { get; set; }        
        public System.DateTime CreatedDate { get; set; }        
        public Nullable<int> ModifiedBy { get; set; }        
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
