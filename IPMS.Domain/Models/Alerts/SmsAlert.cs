using Core.Repository;
using System;

namespace IPMS.Domain.Models
{
    public partial class SmsAlert : EntityBase
    {
        public long SmsTblPkID { get; set; }
        public string SmsParam { get; set; }
        public string MobileNo { get; set; }
        public string SmsContent { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> SmsCreaDate { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public string Remarks { get; set; }
    }
}
