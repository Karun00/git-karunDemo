using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class NotificationRoleMapExtension
    {
        public static List<NotificationRoleVO> MapToDTO(this List<NotificationRole> notifications)
        {
            List<NotificationRoleVO> notificationVOs = new List<NotificationRoleVO>();
            if (notifications != null)
            {
                foreach (var notify in notifications)
                {
                    notificationVOs.Add(notify.MapToDTO());

                }
            }
            return notificationVOs;
        }
        public static NotificationRoleVO MapToDTO(this NotificationRole data)
        {
            
                NotificationRoleVO notificationVo = new NotificationRoleVO();
                if (data != null)
                {
                notificationVo.NotificationRoleID = data.NotificationRoleID;
                notificationVo.NotificationTemplateCode = data.NotificationTemplateCode;
                notificationVo.RoleID = data.RoleID;
                notificationVo.RecordStatus = data.RecordStatus;
                notificationVo.CreatedBy = data.CreatedBy;
                notificationVo.CreatedDate = data.CreatedDate;
                notificationVo.ModifiedBy = data.ModifiedBy;
                notificationVo.ModifiedDate = data.ModifiedDate;
            }
            return notificationVo;

    }
    }
}
