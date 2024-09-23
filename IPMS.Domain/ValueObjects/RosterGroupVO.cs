using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    public class RosterGroupVO
    {
        public int Year { get; set; }
        public int Designation { get; set; }
        public int month { get; set; }
        public List<RosterVO> RosterAloocationLists { get; set; }
    }
}
