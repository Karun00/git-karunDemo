using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PermitRequestSubAreaMapExtension
    {
        public static List<PermitRequestSubArea> MapToEntity(this IEnumerable<PermitRequestSubAreaVO> vos)
        {
            List<PermitRequestSubArea> entities = new List<PermitRequestSubArea>();
            if (vos != null)
            {
                foreach (var vo in vos)
                {
                    entities.Add(vo.MapToEntity());
                }
            }
            return entities;
        }
        //public static List<PermitRequestSubAreaVO> MapToDTO(this IEnumerable<PermitRequestSubArea> entities)
        //{
        //    List<PermitRequestSubAreaVO> vos = new List<PermitRequestSubAreaVO>();
        //    if (entities != null)
        //    {
        //        foreach (var entity in entities)
        //        {
        //            vos.Add(entity.MapToDTO());
        //        }
        //    }
        //    return vos;

        //}
        //public static PermitRequestSubAreaVO MapToDTO(this PermitRequestSubArea data)
        //{
        //    PermitRequestSubAreaVO Vo = new PermitRequestSubAreaVO();
        //    if (data != null)
        //    {
        //        Vo.PermitRequestSubAreaID = data.PermitRequestSubAreaID;
        //        Vo.PermitRequestID = data.PermitRequestID;
        //        Vo.PermitRequestSubAreaCode = data.PermitRequestSubAreaCode;
        //        Vo.PermitRequestAreaID = data.PermitRequestAreaID;
        //    }
        //    return Vo;
        //}
        public static PermitRequestSubArea MapToEntity(this PermitRequestSubAreaVO VO)
        {
            PermitRequestSubArea data = new PermitRequestSubArea();
            if (VO != null)
            {
                data.PermitRequestSubAreaID = VO.PermitRequestSubAreaID;
                data.PermitRequestID = VO.PermitRequestID;
                data.PermitRequestSubAreaCode = VO.PermitRequestSubAreaCode;
                data.PermitRequestAreaCode = VO.PermitRequestAreaCode;
            }
            return data;
        }

       

        public static List<string> MapToPermitRequestSubAreaArray(this ICollection<PermitRequestSubArea> permitrequestsubareas)
        {
            List<string> PermitRequestSubAreas = new List<string>();
            if (permitrequestsubareas != null)
            {
                foreach (var permitrequestsubarea in permitrequestsubareas)
                {
                    PermitRequestSubAreas.Add(permitrequestsubarea.PermitRequestSubAreaCode+'_'+permitrequestsubarea.PermitRequestAreaCode);
                }
            }
            return PermitRequestSubAreas;
        }

        /// <summary>
        /// Data List Transfer from Array to TerminalOperatorCargoHandling List 
        /// </summary>
        /// <param name="CargoKeys"></param>
        /// <param name="terminalOperatorId"></param>
        /// <returns></returns>
        public static List<PermitRequestSubArea> MapToEntityPermitRequestSubArea(this List<string> PermitRequestSubAreas, int PermitRequestID)
        {


            List<PermitRequestSubArea> PermitRequestSubAreaslist = new List<PermitRequestSubArea>();
            if (PermitRequestSubAreas != null)
            {
                foreach (var PermitRequestSubArea in PermitRequestSubAreas)
                {                   
                    PermitRequestSubArea PermitRequestSubArea1 = new PermitRequestSubArea();
                    PermitRequestSubArea1.PermitRequestID = PermitRequestID;
                    if (PermitRequestSubArea.Contains('_'))
                    {
                        var areacode = PermitRequestSubArea.Split('_')[1];
                        var subareacode = PermitRequestSubArea.Split('_')[0];
                        PermitRequestSubArea1.PermitRequestAreaCode = areacode;
                        PermitRequestSubArea1.PermitRequestSubAreaCode = subareacode;
                    }
                    else
                    {
                        PermitRequestSubArea1.PermitRequestSubAreaCode = PermitRequestSubArea;
                    }
                    PermitRequestSubAreaslist.Add(PermitRequestSubArea1);
                }
            }
            return PermitRequestSubAreaslist;
            //return berthKeyArray;
        }

        ///// <summary>
        ///// Data List Transfer from Array to TerminalOperatorCargoHandling List 
        ///// </summary>
        ///// <param name="CargoKeys"></param>
        ///// <param name="terminalOperatorId"></param>
        ///// <returns></returns>
        //public static List<PermitRequestSubArea> MapToEntityPermitRequestSubArea_view(this List<string> PermitRequestSubAreas, int PermitRequestID)
        //{


        //    List<PermitRequestSubArea> PermitRequestSubAreaslist = new List<PermitRequestSubArea>();
        //    if (PermitRequestSubAreas != null)
        //    {
        //        foreach (var PermitRequestSubArea in PermitRequestSubAreas)
        //        {
        //            PermitRequestSubArea PermitRequestSubArea1 = new PermitRequestSubArea();
        //            PermitRequestSubArea1.PermitRequestID = PermitRequestID;
        //            //var areacode = PermitRequestSubArea.Split('_')[1];
        //            //var subareacode = PermitRequestSubArea.Split('_')[0];
        //           // PermitRequestSubArea1.PermitRequestAreaCode = areacode;
        //            PermitRequestSubArea1.PermitRequestSubAreaCode = PermitRequestSubArea;
        //            PermitRequestSubAreaslist.Add(PermitRequestSubArea1);
        //        }
        //    }
        //    return PermitRequestSubAreaslist;
        //    //return berthKeyArray;
        //}


    }
}

