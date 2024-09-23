using System;


namespace IPMS.Domain.ValueObjects
{
    public class GroupedRosterReferenceVO
    {
        public string CurDate { get; set; }
        public string CurDay { get; set; }
        public Nullable<int> GRP1 { get; set; }
        public Nullable<int> GRP2 { get; set; }
        public Nullable<int> GRP3 { get; set; }
        public Nullable<int> GRP4 { get; set; }
    }
}
