using Core.Repository;

namespace IPMS.Domain.Models
{
    public partial class PilotExemptionRequestDocument:EntityBase
    {
        public int PilotExemptionRequestDocumentID { get; set; }
        public int PilotID { get; set; }
        public int DocumentID { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string FileName { get; set; }
        public string DocumentName { get; set; }
        public virtual Document Document { get; set; }
        public virtual Pilot Pilot { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
