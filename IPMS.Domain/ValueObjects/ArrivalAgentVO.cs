using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ArrivalAgentVO
    {
        public int ArrivalAgentID { get; set; }  
        public string VCN { get; set; }       
        public int AgentID { get; set; }       
        public string IsPrimary { get; set; }
    }
}
