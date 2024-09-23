using System.Collections.Generic;


namespace IPMS.Domain.ValueObjects
{
    public class GroupedRosterVO
    {    
        public int WeekNo { get; set; }
        public List<GroupedRosterReferenceVO> rosters { get; set; }
    }
}
