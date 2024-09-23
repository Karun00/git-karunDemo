using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
     public static class Section625CPreventMapExtension
    {
         public static List<Section625CPrevent> MapToEntity(this IEnumerable<Section625CPreventVO> vos)
         {
             List<Section625CPrevent> section625cprevententities = new List<Section625CPrevent>();
             foreach (var vo in vos)
             {
                 section625cprevententities.Add(vo.MapToEntity());
             }
             return section625cprevententities;
         }

         public static List<Section625CPreventVO> MapToDTO(this IEnumerable<Section625CPrevent> section625cprevententities)
         {
             List<Section625CPreventVO> section625cpreventvos = new List<Section625CPreventVO>();
             foreach (var section625cprevententity in section625cprevententities)
             {
                 section625cpreventvos.Add(section625cprevententity.MapToDTO());
             }
             return section625cpreventvos;

         }



         public static Section625CPreventVO MapToDTO(this Section625CPrevent data)
         {
             Section625CPreventVO Vo = new Section625CPreventVO();
             Vo.Section625CPreventID = data.Section625CPreventID;
             Vo.Section625CID = data.Section625CID;
             Vo.PreventStep = data.PreventStep;
             Vo.TargetDateTime = data.TargetDateTime;
             Vo.ActionBy = data.ActionBy;
             Vo.CompletedDate = data.CompletedDate;
             return Vo;
         }

         public static Section625CPrevent MapToEntity(this Section625CPreventVO VO)
         {
             Section625CPrevent data = new Section625CPrevent();
             data.Section625CPreventID = VO.Section625CPreventID;
             data.Section625CID = VO.Section625CID;
             data.PreventStep = VO.PreventStep;
             data.TargetDateTime = VO.TargetDateTime;
             data.ActionBy = VO.ActionBy;
             data.CompletedDate = VO.CompletedDate;
             return data;
         }
    }
}
