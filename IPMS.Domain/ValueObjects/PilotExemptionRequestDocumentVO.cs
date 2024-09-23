
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for PilotExemption RequestDocument
    /// </summary>
    public class PilotExemptionRequestDocumentVO
    {
        public int PilotExemptionRequestDocumentID { get; set; }
        public int PilotID { get; set; }
        public int DocumentID { get; set; }
        public string RecordStatus { get; set; }
        public string FileName { get; set; }
        public string DocumentName { get; set; }
    }

}
