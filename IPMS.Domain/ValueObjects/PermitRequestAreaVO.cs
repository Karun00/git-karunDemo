using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class PermitRequestAreaVO
    {
        public int PermitRequestAreaID { get; set; }
        public int PermitRequestID { get; set; }
        public string PermitRequestAreaCode { get; set; }
        public List<string> PermitRequestSubArea { get; set; }

    }
}
