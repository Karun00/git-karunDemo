using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class AgentVO
    {
        public int AgentID { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public string RegisteredName { get; set; }
        public string TradingName { get; set; }
        public string RegistrationNumber { get; set; }
        public string VATNumber { get; set; }
        public string IncomeTaxNumber { get; set; }
        public string SkillsDevLevyNumber { get; set; }
        public int BusinessAddressID { get; set; }
        public Nullable<int> PostalAddressID { get; set; }
        public string TelephoneNo1 { get; set; }
        public string TelephoneNo2 { get; set; }
        public string FaxNo { get; set; }
        public int AuthorizedContactPersonID { get; set; }
        public string SARSTaxClearance { get; set; }
        public string SAASOA { get; set; }
        public string QualifyBBBEECodes { get; set; }
        public string BBBEEStatus { get; set; }
        public string VerifyBBBEEStatus { get; set; }
        public string RecordStatus { get; set; }
        public string AnonymousUserYn { get; set; }
        public string AgentAccountno { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }

        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string WorkflowTaskCode { get; set; }
        public string WorkFlowTask { get; set; }


        public int CargoTons { get; set; }

        public int Ballast { get; set; }

        public int Bunkers { get; set; }

        public Nullable<DateTime> ScheduleFromDate { get; set; }

        public Nullable<DateTime> ScheduleToDate { get; set; }

        public int SuppDryDockID { get; set; }

        public List<AgentAccountVO> AgentAccountVO { get; set; }
        public virtual List<SuppDryDockDocumentVO> SuppDryDockDocument { get; set; }

        public string CurrentBerth { get; set; }
        public Nullable<decimal> LengthOverallInM { get; set; }
       
    }
}
