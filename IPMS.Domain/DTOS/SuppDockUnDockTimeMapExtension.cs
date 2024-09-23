using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPMS.Domain.DTOS
{
    public static class SuppDockUnDockTimeMapExtension
    {
        public static List<SuppDockUnDockTimeVO> MapToDto(this List<SuppDockUnDockTime> suppDockUndockTimes)
        {
            List<SuppDockUnDockTimeVO> SuppDockUnDockTimevoList = new List<SuppDockUnDockTimeVO>();

            if (suppDockUndockTimes != null)
            {
                foreach (SuppDockUnDockTime suppDockUndockTime in suppDockUndockTimes)
                {
                    SuppDockUnDockTimevoList.Add(suppDockUndockTime.MapToDto());
                }
            }

            return SuppDockUnDockTimevoList;

        }
        public static SuppDockUnDockTimeVO MapToDto(this SuppDockUnDockTime data)
        {
            SuppDockUnDockTimeVO SuppDockUnDockTimeVo = new SuppDockUnDockTimeVO();
            if (data != null)
            {
                SuppDockUnDockTimeVo.SuppDockUnDockTimeID = data.SuppDockUnDockTimeID;
                SuppDockUnDockTimeVo.SuppDryDockID = data.SuppDryDockID;
                SuppDockUnDockTimeVo.Chamber = data.Chamber;
                //SuppDockUnDockTimeVo.EnteredDockDateTime = data.EnteredDockDateTime;
                //SuppDockUnDockTimeVo.OnBlocksDateTime = data.OnBlocksDateTime;
                //SuppDockUnDockTimeVo.DryDockDateTime = data.DryDockDateTime;
                //SuppDockUnDockTimeVo.FinishedDockDateTime = data.FinishedDockDateTime;
                //SuppDockUnDockTimeVo.OffBlocksDateTime = data.OffBlocksDateTime;
                //SuppDockUnDockTimeVo.LeftDockDateTime = data.LeftDockDateTime;

                SuppDockUnDockTimeVo.RecordStatus = data.RecordStatus;

                SuppDockUnDockTimeVo.CreatedBy = data.CreatedBy;
                SuppDockUnDockTimeVo.CreatedDate = data.CreatedDate;
                SuppDockUnDockTimeVo.ModifiedBy = data.ModifiedBy;
                SuppDockUnDockTimeVo.ModifiedDate = data.ModifiedDate;
            }
           // SuppHotWorkInspectionVo.ArrivalNotificationVo = data.SuppServiceRequest.ArrivalNotification.MapToDTO();
            
            return SuppDockUnDockTimeVo;
        }
        public static SuppDockUnDockTime MapToEntity(this SuppDockUnDockTimeVO vo)
        {

            SuppDockUnDockTime suppDockUnDockTime = new SuppDockUnDockTime();
            if (vo != null)
            {
                suppDockUnDockTime.SuppDockUnDockTimeID = vo.SuppDockUnDockTimeID;
                suppDockUnDockTime.SuppDryDockID = vo.SuppDryDockID;
                suppDockUnDockTime.Chamber = vo.Chamber;
                //suppDockUnDockTime.EnteredDockDateTime = suppDockUnDockTimeVO.EnteredDockDateTime;
                //suppDockUnDockTime.OnBlocksDateTime = suppDockUnDockTimeVO.OnBlocksDateTime;
                //suppDockUnDockTime.DryDockDateTime = suppDockUnDockTimeVO.DryDockDateTime;
                //suppDockUnDockTime.FinishedDockDateTime = suppDockUnDockTimeVO.FinishedDockDateTime;
                //suppDockUnDockTime.OffBlocksDateTime = suppDockUnDockTimeVO.OffBlocksDateTime;
                //suppDockUnDockTime.LeftDockDateTime = suppDockUnDockTimeVO.LeftDockDateTime;

                suppDockUnDockTime.RecordStatus = vo.RecordStatus;

                suppDockUnDockTime.CreatedBy = vo.CreatedBy;
                suppDockUnDockTime.CreatedDate = vo.CreatedDate;
                suppDockUnDockTime.ModifiedBy = vo.ModifiedBy;
                suppDockUnDockTime.ModifiedDate = vo.ModifiedDate;
            }
            return suppDockUnDockTime;

        }

    }
}
