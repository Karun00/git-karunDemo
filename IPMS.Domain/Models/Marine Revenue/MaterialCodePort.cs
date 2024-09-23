using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
    public partial class MaterialCodePort : EntityBase
    {
        public int MaterialCodePortID { get; set; }
        public Nullable<int> MaterialCodeMasterid { get; set; }
        public string PortCode { get; set; }
        public virtual MaterialCodeMaster MaterialCodeMaster { get; set; }
        public virtual Port Port { get; set; }
    }
}
