using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
    [DataContract]
    public class AccountLoginModel
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string NewPassword { get; set; }
        [DataMember]
        public bool RememberMe { get; set; }
        [DataMember]
        public string ReturnUrl { get; set; }
        [DataMember]
        public long PortID { get; set; }
        [DataMember]
        public string IsFirstTimeLogin { get; set; }
        [DataMember]
        public Nullable<DateTime> PwdExpirtyDate { get; set; }
        [NotMapped]
        public Nullable<DateTime> LoginTime { get; set; }
        [DataMember]
        public string IsMobile { get; set; }
    }
}
