using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
       public static class UserPortMapExtension
    {
           public static List<UserPortVO> MapToDto(this IEnumerable<UserPort> portList)
           {
               List<UserPortVO> userportvoList = new List<UserPortVO>();
               if (portList != null)
               {
                   foreach (var data in portList)
                   {
                       userportvoList.Add(data.MapToDto());

                   }
               }
               return userportvoList;
           }

           public static UserPortVO MapToDto(this UserPort data)
           {
               UserPortVO USVO = new UserPortVO();

               if (data != null)
               {
                   USVO.UserID = data.UserID;
                   USVO.PortCode = data.PortCode;
                   USVO.WFStatus = data.WFStatus;
                   USVO.RejectComments  = data.RejectComments ;
                   USVO.RecordStatus = data.RecordStatus;
                   USVO.CreatedBy = data.CreatedBy;
                   USVO.IsFinal = data.IsFinal;
               }
               return USVO;
           }

    }
}


