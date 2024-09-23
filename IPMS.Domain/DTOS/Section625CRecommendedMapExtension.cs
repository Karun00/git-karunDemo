using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
     public static class Section625CRecommendedMapExtension
    {

         public static List<Section625CRecommended> MapToEntity(this IEnumerable<Section625CRecommendedVO> vos)
        {
            List<Section625CRecommended> entities = new List<Section625CRecommended>();
            foreach (var vo in vos)
            {
                entities.Add(vo.MapToEntity());
            }
            return entities;
        }
         public static List<Section625CRecommendedVO> MapToDTO(this IEnumerable<Section625CRecommended> entities)
        {
            List<Section625CRecommendedVO> vos = new List<Section625CRecommendedVO>();
            foreach (var entity in entities)
            {
                vos.Add(entity.MapToDTO());
            }
            return vos;

        }
         public static Section625CRecommendedVO MapToDTO(this Section625CRecommended data)
         {
             Section625CRecommendedVO Vo = new Section625CRecommendedVO();
             Vo.Section625CRecommendedID = data.Section625CRecommendedID;
             Vo.Section625CID = data.Section625CID;
             Vo.RecommendedStep = data.RecommendedStep;
             Vo.TargetDateTime = data.TargetDateTime;
             Vo.ActionBy = data.ActionBy;
             Vo.CompletedDate = data.CompletedDate;
             return Vo;
         }

         public static Section625CRecommended MapToEntity(this Section625CRecommendedVO VO)
         {
             Section625CRecommended Data = new Section625CRecommended();
             Data.Section625CRecommendedID = VO.Section625CRecommendedID;
             Data.Section625CID = VO.Section625CID;
             Data.RecommendedStep = VO.RecommendedStep;
             Data.TargetDateTime = VO.TargetDateTime;
             Data.ActionBy = VO.ActionBy;
             Data.CompletedDate = VO.CompletedDate;
             return Data;
         }
    }
}
