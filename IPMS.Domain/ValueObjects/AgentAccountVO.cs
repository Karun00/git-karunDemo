using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class AgentAccountVO
    {
        public int AgentAccountID { get; set; }
        public int AgentID { get; set; }
        //public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string RecordStatus { get; set; }
        public string PortCode { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
