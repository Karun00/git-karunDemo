using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class WasteDeclarationVO
    {
        [DataMember]
        public int WasteDeclarationID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string MarpolCode { get; set; }
        [DataMember]
        public string ClassCode { get; set; }
        [DataMember]
        public int LicenseRequestID { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public string DeclarationName { get; set; }
        [DataMember]
        public string Others { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }        
    }
}
