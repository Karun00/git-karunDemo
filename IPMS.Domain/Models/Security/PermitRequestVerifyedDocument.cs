using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class PermitRequestVerifyedDocument:EntityBase
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
        public virtual Document Document { get; set; }
        public virtual PermitRequestVerifyedDetail PermitRequestVerifyedDetail { get; set; }
    }
}
