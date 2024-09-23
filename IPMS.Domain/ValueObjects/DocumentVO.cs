using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
    public class DocumentVO
    {
        public int DocumentID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string FileName { get; set; }
        public string RecordStatus { get; set; }
        public string FileType { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public byte[] Data { get; set; }
    }
}
