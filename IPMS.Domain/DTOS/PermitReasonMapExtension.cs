using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PermitReasonMapExtension
    { 
        public static List<PermitReason> MapToEntity(this IEnumerable<PermitReasonVO> vos)
        {
            List<PermitReason> entities = new List<PermitReason>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        public static List<PermitReasonVO> MapToDTO(this IEnumerable<PermitReason> entities)
        {
            List<PermitReasonVO> vos = new List<PermitReasonVO>();
            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    vos.Add(entity.MapToDTO());
                }
            }
            return vos;

        }
        public static PermitReasonVO MapToDTO(this PermitReason data)
        {
            PermitReasonVO Vo = new PermitReasonVO();
            if (data != null)
            {
                Vo.PermitReasonID = data.PermitReasonID;
                Vo.PermitRequestID = data.PermitRequestID;
                Vo.ReasonCode = data.ReasonCode;
            }
            return Vo;
        }
        public static PermitReason MapToEntity(this PermitReasonVO VO)
        {
            PermitReason data = new PermitReason();
            if (VO != null)
            {
                data.PermitReasonID = VO.PermitReasonID;
                data.PermitRequestID = VO.PermitRequestID;
                data.ReasonCode = VO.ReasonCode;
            }
            return data;
        }
        public static List<string> MapToPermitReasonArray(this ICollection<PermitReason> permitresons)
        {
            List<string> PermitReasonAreas = new List<string>();
            if (permitresons != null)
            {
                foreach (var permitrequestarea in permitresons)
                {
                    PermitReasonAreas.Add(permitrequestarea.ReasonCode);
                }
            }
            return PermitReasonAreas;
        }
      
        /// <summary>
        /// Data List Transfer from Array to TerminalOperatorCargoHandling List 
        /// </summary>
        /// <param name="CargoKeys"></param>
        /// <param name="terminalOperatorId"></param>
        /// <returns></returns>
        public static List<PermitReason> MapToEntityPermitReason(this List<string> PermitReasons, int PermitRequestID)
        {


            List<PermitReason> PermitReasonslist = new List<PermitReason>();
            if (PermitReasons != null)
            {
                foreach (var PermitReason in PermitReasons)
                {
                    PermitReason PermitReason1 = new PermitReason();
                    PermitReason1.PermitRequestID = PermitRequestID;
                    PermitReason1.ReasonCode = PermitReason.ToString();

                    PermitReasonslist.Add(PermitReason1);
                }
            }
            return PermitReasonslist;
            //return berthKeyArray;
        }


        
        
    
    }
}
