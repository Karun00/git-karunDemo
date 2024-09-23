using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class SuppDryDockDocumentVO
    {        
        public int SuppDryDockDocumentID { get; set; }        
        public int SuppDryDockID { get; set; }        
        public int DocumentID { get; set; }
        public string FileName { get; set; }
        public string RecordStatus { get; set; }        
        public int CreatedBy { get; set; }        
        public System.DateTime CreatedDate { get; set; }        
        public int ModifiedBy { get; set; }        
        public System.DateTime ModifiedDate { get; set; }
        public virtual DocumentVO Document { get; set; }        
        public string DocumentTypeName { get; set; }
      
    }
}
