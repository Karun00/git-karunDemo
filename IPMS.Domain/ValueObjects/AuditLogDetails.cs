using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class AuditLogDetails : EntityBase
    {
        [DataMember]
        public int AuditTrailID { get; set; }

        [DataMember]
        public int AuditTrailConfigID { get; set; }

        [DataMember]
        public string EntryOrExit { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string UserIPAddress { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string ControlerName { get; set; }

        [DataMember]
        public string ActionName { get; set; }

        [DataMember]
        public string UserFriendlyDescription { get; set; }

        [DataMember]
        public string AuditDateTime { get; set; }

        [DataMember]
        public string UserComputerName { get; set; }
    }
}
