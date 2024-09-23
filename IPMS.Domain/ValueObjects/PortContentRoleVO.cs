using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class PortContentRoleVO
    {
        public int PortContentID { get; set; }
        public int RoleID { get; set; }
        public string UserType { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string DocumentName { get; set; }
        public Nullable<int> DocumentID { get; set; }
    }
}
