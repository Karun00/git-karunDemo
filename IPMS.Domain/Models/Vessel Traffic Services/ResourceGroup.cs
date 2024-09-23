using System.Collections.Generic;
using Core.Repository;

namespace IPMS.Domain.Models
{
    public partial class ResourceGroup : EntityBase
    {
        public ResourceGroup()
        {
            this.ResourceEmployeeGroups = new List<ResourceEmployeeGroup>();
            this.ResourceRosters = new List<ResourceRoster>();
            this.RosterDtls = new List<RosterDtl>();
            this.RosterGroups = new List<RosterGroup>();
        }


        public int ResourceGroupID { get; set; }
        public string PortCode { get; set; }
        public string ResourceGroupName { get; set; }
        public string Position { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public  Port Port { get; set; }
        public  List<ResourceEmployeeGroup> ResourceEmployeeGroups { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  SubCategory SubCategory { get; set; }
        public  ICollection<ResourceRoster> ResourceRosters { get; set; }
        public  ICollection<RosterGroup> RosterGroups { get; set; }
        public  ICollection<RosterDtl> RosterDtls { get; set; }
        public string ResourceGroupCode { get; set; }
    }
}
