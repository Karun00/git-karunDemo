using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class NotificationPortMapExtension
    {
        public static List<NotificationPortVO> MapToDTO(this List<NotificationPort> notifications)
        {
            List<NotificationPortVO> notificationVOs = new List<NotificationPortVO>();
            if (notifications != null)
            {
                foreach (var notify in notifications)
                {
                    notificationVOs.Add(notify.MapToDTO());

                }
            }
            return notificationVOs;
        }
        public static NotificationPortVO MapToDTO(this NotificationPort data)
        {
            NotificationPortVO notificationVo = new NotificationPortVO();
            if (data != null)
            {
                notificationVo.NotificationTemplateCode = data.NotificationTemplateCode;
                notificationVo.PortCode = data.PortCode;
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
