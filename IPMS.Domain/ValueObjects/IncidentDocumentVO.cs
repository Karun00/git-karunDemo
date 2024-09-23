 
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for IncidentDocument
    /// </summary>
    public class IncidentDocumentVO
    {
        public int IncidentDocument1 { get; set; }
        public int IncidentID { get; set; }
        public int DocumentID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public IncidentVO Incident { get; set; }
    }
}
