using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PermitRequestAreaMapExtension
    {
        public static List<PermitRequestArea> MapToEntity(this IEnumerable<PermitRequestAreaVO> vos)
        {
            List<PermitRequestArea> entities = new List<PermitRequestArea>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<PermitRequestAreaVO> MapToDTO(this IEnumerable<PermitRequestArea> entities)
        {
            List<PermitRequestAreaVO> vos = new List<PermitRequestAreaVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;

        }
        public static PermitRequestAreaVO MapToDTO(this PermitRequestArea data)
        {
            PermitRequestAreaVO Vo = new PermitRequestAreaVO();
            if (data != null)
            {
                Vo.PermitRequestAreaID = data.PermitRequestAreaID;
                Vo.PermitRequestID = data.PermitRequestID;
                Vo.PermitRequestAreaCode = data.PermitRequestAreaCode;
            }
            return Vo;
        }
        public static PermitRequestArea MapToEntity(this PermitRequestAreaVO VO)
        {
            PermitRequestArea data = new PermitRequestArea();
            if (VO != null)
            {
                data.PermitRequestAreaID = VO.PermitRequestAreaID;
                data.PermitRequestID = VO.PermitRequestID;
                data.PermitRequestAreaCode = VO.PermitRequestAreaCode;
            }
            return data;
        }

        public static List<string> MapToPermitRequestAreaArray(this ICollection<PermitRequestArea> permitrequestareas)
        {
            List<string> PermitRequestAreas = new List<string>();
            if (permitrequestareas != null)
            {
                foreach (var permitrequestarea in permitrequestareas)
                {
                    PermitRequestAreas.Add(permitrequestarea.PermitRequestAreaCode);
                }
            }
            return PermitRequestAreas;
        }
      
        /// <summary>
        /// Data List Transfer from Array to TerminalOperatorCargoHandling List 
        /// </summary>
        /// <param name="CargoKeys"></param>
        /// <param name="terminalOperatorId"></param>
        /// <returns></returns>
        public static List<PermitRequestArea> MapToEntityPermitRequestArea(this List<string> PermitRequestAreas, int PermitRequestID)
        {


            List<PermitRequestArea> PermitRequestAreaslist = new List<PermitRequestArea>();
            if (PermitRequestAreas != null)
            {
                foreach (var PermitRequestArea in PermitRequestAreas)
                {
                    PermitRequestArea permitrequestarea = new PermitRequestArea();
                    permitrequestarea.PermitRequestID = PermitRequestID;
                    permitrequestarea.PermitRequestAreaCode = PermitRequestArea.ToString();

                    PermitRequestAreaslist.Add(permitrequestarea);
                }
            }
            return PermitRequestAreaslist;
            //return berthKeyArray;
        }
        
    
    }
}
