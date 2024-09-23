using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class Section625GDetail2MapExtension
    {
        public static List<Section625GDetail2VO> MapToDTO(this IEnumerable<Section625GDetail2> entities)
        {
            List<Section625GDetail2VO> Vos = new List<Section625GDetail2VO>();
            foreach (var item in entities)
            {
                Vos.Add(item.MapToDTO());
            }
            return Vos;
        }

        public static List<Section625GDetail2> MapToEntity(this IEnumerable<Section625GDetail2VO> Vos)
        {
            List<Section625GDetail2> entities = new List<Section625GDetail2>();
            foreach (var item in Vos)
            {
                entities.Add(item.MapToEntity());
            }

            return entities;
        }
        public static Section625GDetail2VO MapToDTO(this Section625GDetail2 data)
        {
            Section625GDetail2VO VO = new Section625GDetail2VO();
            VO.Section625GDetail2ID = data.Section625GDetail2ID;
            VO.Section625GID = data.Section625GID;
            VO.Description = data.Description;
            VO.ResponsiblePerson = data.ResponsiblePerson;
            VO.TargetCompletion = data.TargetCompletion;
            VO.DateCompletion = data.DateCompletion;
            return VO;
        }

        public static Section625GDetail2 MapToEntity(this Section625GDetail2VO VO)
        {
            Section625GDetail2 data = new Section625GDetail2();
            data.Section625GDetail2ID = VO.Section625GDetail2ID;
            data.Section625GID = VO.Section625GID;
            data.Description = VO.Description;
            data.ResponsiblePerson = VO.ResponsiblePerson;
            data.TargetCompletion = VO.TargetCompletion;
            data.DateCompletion = VO.DateCompletion;   
            return data;
        }
    }
}
