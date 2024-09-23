using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
    public partial class PortContentRole : EntityBase
    {
        public int PortContentID { get; set; }
        public int RoleID { get; set; }
        public string UserType { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual PortContent PortContent { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Role Role { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
