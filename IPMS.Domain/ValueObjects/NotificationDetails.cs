using Core.Repository;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class NotificationDetails : EntityBase
    {
        [DataMember]
        public string NotificationTemplateCode { get; set; }

        [DataMember]
        public string NotificationTemplateName { get; set; }

        [DataMember]
        public string NotificationTemplateBase { get; set; }

        [DataMember]
        public string IsEmail { get; set; }

        [DataMember]
        public string EmailTemplate { get; set; }

        [DataMember]
        public string IsSMS { get; set; }

        [DataMember]
        public string SMSTemplate { get; set; }

        [DataMember]
        public string IsSysMessage { get; set; }

        [DataMember]
        public string SysMessageTemplate { get; set; }

        [DataMember]
        public int EntityID { get; set; }

        [DataMember]
        public string WorkflowTaskCode { get; set; }

        [DataMember]
        public string RecordStatus { get; set; }

        [DataMember]
        public int RoleID { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string EntityName { get; set; }

        [DataMember]
        public string EmailSubject { get; set; }

        [DataMember]
        public virtual ICollection<Role> Roles { get; set; }

        [DataMember]
        public virtual ICollection<Port> Ports { get; set; }
    }
}
