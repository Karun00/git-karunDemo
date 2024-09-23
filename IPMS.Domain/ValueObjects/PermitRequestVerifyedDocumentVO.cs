using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class PermitRequestVerifyedDocumentVO
    {
        public int PermitRequestverifyedDocumentID { get; set; }
        public Nullable<int> PermitRequestverifyedID { get; set; }
        public string FileName { get; set; }
        public Nullable<int> DocumentID { get; set; }
        public string DocumentName { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }


    }
}
