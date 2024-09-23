using System;
using System.Collections.Generic;
using Core.Repository;
namespace IPMS.Domain.Models
{
    public partial class EmailAlert:EntityBase
    {
        public long EmaliTblPkID { get; set; }
        public string EmailParam { get; set; }
        public string EmailID { get; set; }
        public string EmailContent { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> EmailCreaDate { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public string Remarks { get; set; }
    }
}
