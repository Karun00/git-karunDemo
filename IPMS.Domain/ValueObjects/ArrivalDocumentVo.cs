using System; 
 
namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Arrival Notification Documnent
    /// </summary>
  public  class ArrivalDocumentVo
    {
        public string VCN { get; set; }
        public int DocumentID { get; set; }
        public string RecordStatus { get; set; }
        public string FileName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentCode { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public DocumentVO Documents { get; set; }
    }
}
