using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class NotificationTemplateMapExtension
    {
        public static List<NotificationTemplateVO> MapToDTO(this List<NotificationTemplate> notifications)
        {
            List<NotificationTemplateVO> notificationVOs = new List<NotificationTemplateVO>();
            if (notifications != null)
            {
                foreach (var notify in notifications)
                {
                    notificationVOs.Add(notify.MapToDTO());

                }
            }
            return notificationVOs;
        }
        public static NotificationTemplateVO MapToDTO(this NotificationTemplate data)
        {
            NotificationTemplateVO notificationVo = new NotificationTemplateVO();
            if (data != null)
            {
                notificationVo.NotificationTemplateCode = data.NotificationTemplateCode;
                notificationVo.NotificationTemplateName = data.NotificationTemplateName;
                notificationVo.NotificationTemplateBase = data.NotificationTemplateBase;
                notificationVo.EntityID = data.EntityID;
                notificationVo.EntityName = data.Entity.EntityName;
                notificationVo.IsEmail = data.IsEmail;
                notificationVo.EmailSubject = data.EmailSubject;
                notificationVo.EmailTemplate = data.EmailTemplate;
                notificationVo.IsSMS = data.IsSMS;
                notificationVo.SMSTemplate = data.SMSTemplate;
                notificationVo.IsSysMessage = data.IsSysMessage;
                notificationVo.SysMessageTemplate = data.SysMessageTemplate;
                notificationVo.RecordStatus = data.RecordStatus;
                notificationVo.CreatedBy = data.CreatedBy;
                notificationVo.CreatedDate = data.CreatedDate;
                notificationVo.ModifiedBy = data.ModifiedBy;
                notificationVo.ModifiedDate = data.ModifiedDate;

                //if (data.Entity != null)
                //{
                //    notificationVo.Entity = data.Entity != null ? data.Entity.MapToDTO() : null;
                //}
                if (data.NotificationPorts != null)
                {
                    notificationVo.NotificationPort = data.NotificationPorts.ToList().MapToDTO();
                }
                if (data.NotificationRoles != null)
                {
                    notificationVo.NotificationRole = data.NotificationRoles.ToList().MapToDTO();
                }
                if (data.SubCategory != null)
                {
                    notificationVo.WorkflowTaskCode = data.SubCategory.SubCatCode;
                }
            }
            return notificationVo;

    }
    }
}
