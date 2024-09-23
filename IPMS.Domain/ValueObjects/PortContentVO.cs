using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class PortContentVO
    {
        public int PortContentID { get; set; }
        public string PortCode { get; set; }
        public string ContentType { get; set; }
        public string ContentName { get; set; }
        public string LinkVisibility { get; set; }
        public string LinkType { get; set; }
        public string LinkContent { get; set; }
        public string DocumentName { get; set; }
        public Nullable<int> DocumentID { get; set; }
        public Nullable<int> ParentPortContentID { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string UserType { get; set; }
        public List<PortContentRoleVO> PortContentRole { get; set; }
        public List<PortContentVO> PortContent1 { get; set; }
    }
}
