using System.Collections.Generic;
using IPMS.Domain.Models;


namespace IPMS.Domain.ValueObjects
{
    public class RosterVO
    {
        public int RosterID { get; set; }
        public string RosterCode { get; set; }
        public string ResourceGroupName { get; set; }
        public int ResourceGroupID { get; set; }
        public decimal Year { get; set; }
        public string Month { get; set; }
        public int WeekNo { get; set; }
        public string CurDate { get; set; }
        public string CurDay { get; set; }
        public string Designation { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public List<RosterGroupVO> RosterGroups { get; set; }
        public SubCategory SubCategory { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
}
