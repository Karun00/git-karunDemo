using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class PermitRequestVerifyedDetailVO
    {
        public int PermitRequestverifyedID { get; set; }
        public Nullable<int> permitrRequestID { get; set; }
        public string CreminalCheck { get; set; }
        public string Comments { get; set; }
        public string Flag { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<int> verifyedUserID { get; set; }
        public Nullable<System.DateTime> verifyedDate { get; set; }
       // public List<PermitRequestVerifyedDocumentVO> PermitRequestVerifyedDocuments { get; set; }
        public List<PermitRequestVerifyedDocumentVO> PermitRequestverifyedbySSADocuments { get; set; }
        public List<PermitRequestVerifyedDocumentVO> PermitRequestverifyedbySAPSDocuments { get; set; }

    }
}
