using Core.Repository;
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
    public partial class PortConfiguration : EntityBase
    {

        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string ApproveCode { get; set; }
        [DataMember]
        public string RejectCode { get; set; }
        [DataMember]
        public string WorkFlowInitialStatus { get; set; }
        [DataMember]
        public int SERVREQPRECOND1 { get; set; }
        [DataMember]
        public Nullable<int> IncorrectPWDCount { get; set; }
        [DataMember]
        public string CancelCode { get; set; }
        [DataMember]
        public virtual Port Port { get; set; }
        [DataMember]
        public virtual SubCategory ApproveCodeSubCategory { get; set; }
        [DataMember]
        public virtual SubCategory RejectCodeSubCategory { get; set; }
        [DataMember]
        public virtual SubCategory WorkFlowInitialSubCategory { get; set; }
        [DataMember]
        public virtual SubCategory CancelCodeSubCategory { get; set; }

        [NotMapped]
        public int LastNotificationId { get; set; }
        [NotMapped]
        public string EmailRequired { get; set; }
        [NotMapped]
        public string SMSRequired { get; set; }

    }
}
