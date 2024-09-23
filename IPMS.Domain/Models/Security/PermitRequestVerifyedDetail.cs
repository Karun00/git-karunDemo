using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class PermitRequestVerifyedDetail : EntityBase
    {
        public PermitRequestVerifyedDetail()
        {
            this.PermitRequestVerifyedDocuments = new List<PermitRequestVerifyedDocument>();
        }

        public int PermitRequestverifyedID { get; set; }
        public Nullable<int> permitrRequestID { get; set; }
        public string CreminalCheck { get; set; }
        public string Comments { get; set; }
        public string Flag { get; set; }
        public string RecordStatus { get; set; }
        public Nullable<int> verifyedUserID { get; set; }
        public Nullable<System.DateTime> verifyedDate { get; set; }
        public  PermitRequest PermitRequest { get; set; }
        public  ICollection<PermitRequestVerifyedDocument> PermitRequestVerifyedDocuments { get; set; }
    }
}
