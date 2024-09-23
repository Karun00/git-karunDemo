using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class PermitRequestSubAreaVO
    {
        public int PermitRequestSubAreaID { get; set; }
        public int PermitRequestID { get; set; }      
        public string PermitRequestSubAreaCode { get; set; }
        public string PermitRequestAreaCode { get; set; }
    }
}
