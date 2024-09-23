using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    public class DeploymentPlanVO
    {
        [DataMember]
        public int DeploymentPlanID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
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
        public string Description { get; set; }
        [DataMember]
        public Nullable<int> FinancialYearID { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string DateName { get; set; }
        [DataMember]
        public string FinancialYearDate { get; set; }
        [DataMember]
        public System.DateTime StartDate { get; set; }
        [DataMember]
        public System.DateTime EndDate { get; set; }
       

        [DataMember]
        public ICollection<FinancialYearVO> FinancialYears { get; set; }

        [DataMember]
        public ICollection<SubCategoryVO> DredgingColors { get; set; }

        [DataMember]
        public ICollection<SubCategoryVO> DredgingTypes { get; set; }
        [DataMember]
        public ICollection<PortVO> PortTypes { get; set; }
        [DataMember]
        public ICollection<PortGeneralConfigsVO> CraftColors { get; set; }
        [DataMember]
      //  public List<StatementVCNVO> StatementFactEvents { get; set; }     
        public List<PlannedDeploymentVO> DeploymentBudget { get; set; }

        
    }
    public class PlannedDeploymentVO
    {
        [DataMember]
        public int CraftID { get; set; }
        [DataMember]
        public string CraftName { get; set; }
        [DataMember]
        public string DredgerColorCode { get; set; }
        [DataMember]
        public string SubCatCode { get; set; }

        [DataMember]
        public string PortCode { get; set; }

        [DataMember]
        public int DeploymentBudgetID { get; set; }
        [DataMember]
        public int DeploymentPlanID { get; set; }
        [DataMember]
        public int Budget { get; set; }
        [DataMember]
        public int DredgPlan { get; set; }
        [DataMember]
        public Nullable<int> Jan { get; set; }
        [DataMember]
        public Nullable<int> JanCraftID { get; set; }
        [DataMember]
        public Nullable<int> Feb { get; set; }
        [DataMember]
        public Nullable<int> FebCraftID { get; set; }
        [DataMember]
        public Nullable<int> Mar { get; set; }
        [DataMember]
        public Nullable<int> MarCraftID { get; set; }
        [DataMember]
        public Nullable<int> Apr { get; set; }
        [DataMember]
        public Nullable<int> AprCraftID { get; set; }
        [DataMember]
        public Nullable<int> May { get; set; }
        [DataMember]
        public Nullable<int> MayCraftID { get; set; }
        [DataMember]
        public Nullable<int> Jun { get; set; }
        [DataMember]
        public Nullable<int> JunCraftID { get; set; }
        [DataMember]
        public Nullable<int> Jul { get; set; }
        [DataMember]
        public Nullable<int> JulCraftID { get; set; }
        [DataMember]
        public Nullable<int> Aug { get; set; }
        [DataMember]
        public Nullable<int> AugCraftID { get; set; }
        [DataMember]
        public Nullable<int> Sep { get; set; }
        [DataMember]
        public Nullable<int> SepCraftID { get; set; }
        [DataMember]
        public Nullable<int> Oct { get; set; }
        [DataMember]
        public Nullable<int> OctCraftID { get; set; }
        [DataMember]
        public Nullable<int> Nov { get; set; }
        [DataMember]
        public Nullable<int> NovCraftID { get; set; }
        [DataMember]
        public Nullable<int> Dec { get; set; }
        [DataMember]
        public Nullable<int> DecCraftID { get; set; }
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
        
    }
}
