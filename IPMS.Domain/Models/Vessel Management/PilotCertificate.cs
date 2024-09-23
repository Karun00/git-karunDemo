using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;

namespace IPMS.Domain.Models
{
    public partial class PilotCertificate:EntityBase
    {
            public int PilotCertificateID { get; set; }
            public int PilotID { get; set; }
            public int DocumentID { get; set; }
            public string RecordStatus { get; set; }
            public int CreatedBy { get; set; }
            public System.DateTime CreatedDate { get; set; }
            public Nullable<int> ModifiedBy { get; set; }
            public System.DateTime ModifiedDate { get; set; }
            public virtual Document Document { get; set; }
            public virtual Pilot Pilot { get; set; }
            public virtual User User { get; set; }
            public virtual User User1 { get; set; }
       
    }
}
