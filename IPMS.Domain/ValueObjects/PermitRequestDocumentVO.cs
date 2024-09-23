using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public partial class PermitRequestDocumentVO
    {
        public int PermitRequestDocumentID { get; set; }
        public int PermitRequestID { get; set; }
        public int DocumentID { get; set; }
        public string FileName { get; set; }
        public string DocumentName { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        //public DocumentVO Document { get; set; }     
      
    }
}
