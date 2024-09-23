
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for LicenseRequest Document
    /// </summary>
    public class LicenseRequestDocumentVO
    {

        public int LicenseRequestID { get; set; }
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public DocumentVO Documents { get; set; }
    }
}
