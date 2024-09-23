using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class ServiceRequestSailingVO
    {

        public int ServiceRequestSailingID { get; set; }
        public int ServiceRequestID { get; set; }
        public bool MarineRevenueCleared { get; set; }
        public Nullable<int> DocumentID { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public DocumentVO ServiceRequestDocument { get; set; }

    }
}
