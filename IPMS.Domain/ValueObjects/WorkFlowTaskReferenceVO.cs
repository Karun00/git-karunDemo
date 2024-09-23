using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{
    public class WorkFlowTaskReferenceVO
    {
        public ICollection<SubCategoryVO> WorkFlowEvents { get; set; }
        public ICollection<EntityVO> Entities { get; set; }
        public ICollection<RoleVO> Roles { get; set; }
      
    }
}
