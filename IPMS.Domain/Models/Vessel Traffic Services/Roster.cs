using System;
using System.Collections.Generic;
using Core.Repository;

namespace IPMS.Domain.Models
{
    public partial class Roster : EntityBase
    {
        public Roster()
        {
            this.RosterGroups = new List<RosterGroup>();
        }

        public int RosterID { get; set; }
        public string RosterCode { get; set; }
        public decimal Year { get; set; }
        public decimal Week { get; set; }
        public string Designation { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public  User User { get; set; }
        public  SubCategory SubCategory { get; set; }
        public  User User1 { get; set; }
        public  ICollection<RosterGroup> RosterGroups { get; set; }
    }
}
