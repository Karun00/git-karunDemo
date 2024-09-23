using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class FinancialYearVO
    {
        [DataMember]
        public int FinancialYearID { get; set; }
        [DataMember]
        public System.DateTime StartDate { get; set; }
        [DataMember]
        public System.DateTime EndDate { get; set; }
        [DataMember]
        public string IsCurrentFinancialYear { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public string BudgetedValuesFYDescription { get; set; }
        [DataMember]
        public string FinancialYear { get; set; }

        [DataMember]
        public List<BudgetedValuesVO> BudgetedValuesVO { get; set; }
    }
}
