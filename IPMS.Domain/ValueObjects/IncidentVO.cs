using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Incident
    /// </summary>
    public class IncidentVO
    {
        public int IncidentID { get; set; }
        public string PortCode { get; set; }
        public string IncidentLocation { get; set; }
        public string IncidentDescription { get; set; }
        public string IncidentNatureDetails { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public List<string> IncedentTypeArray { get; set; }
        public List<IncidentDocumentVO> IncidentDocuments { get; set; }
        public List<IncidentNatureVO> IncidentNatures { get; set; }

    }
}
