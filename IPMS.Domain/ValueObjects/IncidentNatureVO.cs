
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for IncidentNature
    /// </summary>
    public class IncidentNatureVO
    {
        public int IncidentNatureID { get; set; }
        public int IncidentID { get; set; }
        public string IncidentNature1 { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public IncidentVO Incident { get; set; }
        public SubCategoryVO SubCategory { get; set; }
    }
}
