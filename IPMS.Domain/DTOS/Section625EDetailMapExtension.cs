using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class Section625EDetailMapExtension
    {
        public static List<Section625EDetail> MapToEntity(this IEnumerable<Section625EDetailVO> vos)
        {
            List<Section625EDetail> entities = new List<Section625EDetail>();
            foreach (var vo in vos)
            {
                entities.Add(vo.MapToEntity());
            }
            return entities;
        }
        public static List<Section625EDetailVO> MapToDTO(this IEnumerable<Section625EDetail> entities)
        {
            List<Section625EDetailVO> vos = new List<Section625EDetailVO>();
            foreach (var entity in entities)
            {
                vos.Add(entity.MapToDTO());
            }
            return vos;

        }

        public static Section625EDetailVO MapToDTO(this Section625EDetail data)
        {
            Section625EDetailVO Vo = new Section625EDetailVO();
            Vo.Section625EDetailID = data.Section625EDetailID;
            Vo.Section625EID = data.Section625EID;
            Vo.Item = data.Item;
            Vo.Quantity = data.Quantity;
            Vo.ReplacementValue = data.ReplacementValue;
            
            return Vo;
        }

        public static Section625EDetail MapToEntity(this Section625EDetailVO VO)
        {
            Section625EDetail Data = new Section625EDetail();
            Data.Section625EDetailID = VO.Section625EDetailID;
            Data.Section625EID = VO.Section625EID;
            Data.Item = VO.Item;
            Data.Quantity = VO.Quantity;
            Data.ReplacementValue = VO.ReplacementValue;
            return Data;
        }
    
    }
}
