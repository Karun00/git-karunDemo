using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects

{
    public class NotificationTemplateVO
    {

        public string NotificationTemplateCode { get; set; }

        public string NotificationTemplateName { get; set; }

        public string NotificationTemplateBase { get; set; }

        public int EntityID { get; set; }

        public string EntityName { get; set; }

        public string WorkflowTaskCode { get; set; }

        public string IsEmail { get; set; }

        public string EmailSubject { get; set; }

        public string EmailTemplate { get; set; }

        public string IsSMS { get; set; }

        public string SMSTemplate { get; set; }

        public string IsSysMessage { get; set; }

        public string SysMessageTemplate { get; set; }

        public string RecordStatus { get; set; }

        public int CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        // public EntityVO Entity { get; set; }

        public List<NotificationPortVO> NotificationPort { get; set; }

        public List<NotificationRoleVO> NotificationRole { get; set; }

        public int? NotificationId { get; set; }

        public UserMasterVO User { get; set; }

        public SubCategoryVO SubCategory { get; set; }
        
        public EntityVO Entity { get; set; }

        public ICollection<IndividualPermitApplicationDetails> IndividualPermitApplicationDetails { get; set; }
        public string EmailAddress { get; set; }

    }
}
