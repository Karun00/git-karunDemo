using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
    [DataContract]
    public class ResetPasswordLogs:EntityBase
    {
        [DataMember]


        public int ResetPasswordLogID { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }


        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [DataMember]

        public string ModifiedBy { get; set; }

        [DataMember]

        public Nullable<System.DateTime> ModifiedDate { get; set; }


        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]


        public int UserID { get; set; }



        [DataMember]


        public string AuditFlag { get; set; }



    }
}

