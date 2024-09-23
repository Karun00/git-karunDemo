using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class DeploymentBudget : EntityBase
    {

        public int DeploymentBudgetID { get; set; }
        public int DeploymentPlanID { get; set; }
        public int Budget { get; set; }
        public int DredgPlan { get; set; }
        public Nullable<int> Jan { get; set; }
        public Nullable<int> JanCraftID { get; set; }
        public Nullable<int> Feb { get; set; }
        public Nullable<int> FebCraftID { get; set; }
        public Nullable<int> Mar { get; set; }
        public Nullable<int> MarCraftID { get; set; }
        public Nullable<int> Apr { get; set; }
        public Nullable<int> AprCraftID { get; set; }
        public Nullable<int> May { get; set; }
        public Nullable<int> MayCraftID { get; set; }
        public Nullable<int> Jun { get; set; }
        public Nullable<int> JunCraftID { get; set; }
        public Nullable<int> Jul { get; set; }
        public Nullable<int> JulCraftID { get; set; }
        public Nullable<int> Aug { get; set; }
        public Nullable<int> AugCraftID { get; set; }
        public Nullable<int> Sep { get; set; }
        public Nullable<int> SepCraftID { get; set; }
        public Nullable<int> Oct { get; set; }
        public Nullable<int> OctCraftID { get; set; }
        public Nullable<int> Nov { get; set; }
        public Nullable<int> NovCraftID { get; set; }
        public Nullable<int> Dec { get; set; }
        public Nullable<int> DecCraftID { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string DredgingType { get; set; }
        public virtual Craft Craft { get; set; }
        public virtual Craft Craft1 { get; set; }
        public virtual Craft Craft2 { get; set; }
        public virtual Craft Craft3 { get; set; }
        public virtual Craft Craft4 { get; set; }
        public virtual Craft Craft5 { get; set; }
        public virtual Craft Craft6 { get; set; }
        public virtual Craft Craft7 { get; set; }
        public virtual Craft Craft8 { get; set; }
        public virtual Craft Craft9 { get; set; }
        public virtual Craft Craft10 { get; set; }
        public virtual Craft Craft11 { get; set; }
        public virtual User User { get; set; }
        public virtual DeploymentPlan DeploymentPlan { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User1 { get; set; }
    }
}
