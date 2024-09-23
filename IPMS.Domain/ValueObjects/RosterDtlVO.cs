using IPMS.Domain.Models;


namespace IPMS.Domain.ValueObjects
{
    public class RosterDtlVO
    {
        public int ResourceGroupID { get; set; }
        public int ShiftID { get; set; }
        public string Designation { get; set; }
        public int month { get; set; }
        public System.DateTime RosterDate { get; set; }
        public string RecordStatus { get; set; }
        public ResourceGroup ResourceGroup { get; set; }
        public Shift Shift { get; set; }
    }
}
