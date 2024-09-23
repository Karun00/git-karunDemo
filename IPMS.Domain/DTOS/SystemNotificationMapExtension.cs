using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    //--By Mahesh : To get system notifications in Header...................
    public static class SystemNotificationMapExtesion
    {
        public static SystemNotificationVO MapToDTO(this SystemNotification data)
        {
            SystemNotificationVO sysVO = new SystemNotificationVO();
            sysVO.NotificationId = data.NotificationId;
            sysVO.NotificationText = data.NotificationText;
            sysVO.CreatedBy = data.CreatedBy;
            sysVO.CreatedDate = data.CreatedDate.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture); ;
            sysVO.IsRead = data.IsRead;
            sysVO.PortCode = data.PortCode;

            return sysVO;
        }

        public static List<SystemNotificationVO> MapToDTO(this IEnumerable<SystemNotification> notifications)
        {
            var systemNotificationVoList = new List<SystemNotificationVO>();
            foreach (var item in notifications)
            {
                systemNotificationVoList.Add(item.MapToDTO());
            }
            return systemNotificationVoList;
        }

    }
}
