using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class PortInformationReferenceVO
    {
        public ICollection<RoleVO> GetRoles { get; set; }       
    }
}
