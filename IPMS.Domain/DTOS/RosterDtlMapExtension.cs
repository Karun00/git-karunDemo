using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class RosterDtlMapExtension
    {
        public static List<RosterDtlVO> MapToListDTO(this IEnumerable<RosterDtl> RosterDtlList)
        {
            List<RosterDtlVO> rosterdtlvoList = new List<RosterDtlVO>();
            if (RosterDtlList != null)
                foreach (var data in RosterDtlList)
                {
                    rosterdtlvoList.Add(data.MapToDTO());

                }
            return rosterdtlvoList;
        }      
        public static List<RosterDtl> MapToListEntity(this IEnumerable<RosterDtlVO> RosterDtlVoList)
        {
            List<RosterDtl> rosterdtlList = new List<RosterDtl>();
            if (RosterDtlVoList != null)
                foreach (var data in RosterDtlVoList)
                {
                    rosterdtlList.Add(data.MapToEntity());

                }
            return rosterdtlList;
        }

        public static RosterDtl MapToEntity(this RosterDtlVO RosterDtlVo)
        {
            RosterDtl rosterdtl = new RosterDtl();
            if (RosterDtlVo != null)
            {
                rosterdtl.ResourceGroupID = RosterDtlVo.ResourceGroupID;
                rosterdtl.ShiftID = RosterDtlVo.ShiftID;
                rosterdtl.RosterDate = RosterDtlVo.RosterDate;
                rosterdtl.RecordStatus = RosterDtlVo.RecordStatus;
                //rosterdtl.Shift = RosterDtlVo.Shift.MapToEntity();
                //rosterdtl.ResourceGroup = RosterDtlVo.ResourceGroup.MapToEntity();
            }
            return rosterdtl;

        }
        public static RosterDtlVO MapToDTO(this RosterDtl RosterDtl)
        {
            RosterDtlVO rosterdtlVO = new RosterDtlVO();
            if (RosterDtl != null)
            {
                rosterdtlVO.ResourceGroupID = RosterDtl.ResourceGroupID;
                rosterdtlVO.ShiftID = RosterDtl.ShiftID;
                rosterdtlVO.RosterDate = RosterDtl.RosterDate;
                rosterdtlVO.RecordStatus = RosterDtl.RecordStatus;
                //rosterdtlVO.Shift = RosterDtl.Shift.MapToDTO();
                //rosterdtlVO.ResourceGroup = RosterDtl.ResourceGroup.MapToDTO();
            }
            return rosterdtlVO;




        }
    }
}
