using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class PermitReasonVO
    {
        public int PermitReasonID { get; set; }
        public int PermitRequestID { get; set; }
        public string ReasonCode { get; set; }
    }
}
