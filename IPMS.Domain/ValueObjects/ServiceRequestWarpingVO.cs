using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ServiceRequestWarpingVO
    {

        public int ServiceRequestWarpingID { get; set; }
        public int ServiceRequestID { get; set; }

        public string BollardName { get; set; }
        public string ToBollardName { get; set; }
        public string FromBollardKey { get; set; }
        public string ToBollardKey { get; set; }

        public string Warp { get; set; }
        public decimal WarpDistance { get; set; }

        public string FromPositionPortCode { get; set; }
        public string FromPositionQuayCode { get; set; }
        public string FromPositionBerthCode { get; set; }
        public string FromPositionBollardCode { get; set; }
        public string ToPositionPortCode { get; set; }
        public string ToPositionQuayCode { get; set; }
        public string ToPositionBerthCode { get; set; }
        public string ToPositionBollardCode { get; set; }

        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
