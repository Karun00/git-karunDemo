using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class PermitRequestDocument : EntityBase
    {
        public int PermitRequestDocumentID { get; set; }
        public int PermitRequestID { get; set; }
        public int DocumentID { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual Document Document { get; set; }
        public virtual PermitRequest PermitRequest { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
