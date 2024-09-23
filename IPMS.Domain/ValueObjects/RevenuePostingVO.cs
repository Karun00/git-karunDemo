using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{

    public class RevenuePostingVO
    {

        public int RevenuePostingID { get; set; }
        public string vcn { get; set; }
        public string VesselName { get; set; }
        public string VeselAutoName { get; set; }
        public string PortCode { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public string SAPAccNo { get; set; }
        public Nullable<int> AgentID { get; set; }
        public int AgentAccountID { get; set; }
        public string AccountNo { get; set; }
        public string PostingStatus { get; set; }
        public Nullable<System.DateTime> ATA { get; set; }
        public Nullable<System.DateTime> ATD { get; set; }
        public string ReasonForVisit { get; set; }
        public string LastPortOfCall { get; set; }
        public string NextPortOfCall { get; set; }
        public string VesselType { get; set; }
        public string RegisteredName { get; set; }
        public string VoyageIn { get; set; }
        public string VoyageOut { get; set; }
        public string AnyDangerousGoodsonBoard { get; set; }
        public string Arrno { get; set; }
        public string SAPVesselNo { get; set; }
        
        public Nullable<decimal> GRT { get; set; }

        public string CallSign { get; set; }
        public string IMONo { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual ICollection<RevenuePostingDtlVO> RevenuePostingDtls { get; set; }

    }
}
