using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{

    public class PendingRegistrationVerification : EntityBase
    {
        public long ApplicantID { get; set; }
        public long PortID { get; set; }
        public string ApplicantName { get; set; }
        public string RegisteredNo { get; set; }
        public string VatNo { get; set; }
        public string SubmissionDate { get; set; }
        public string Status { get; set; }
        public string RejectedRemarks { get; set; }
        public Nullable<long> RejectedBy { get; set; }
        public Nullable<System.DateTime> RejectedDate { get; set; }
    }
}
