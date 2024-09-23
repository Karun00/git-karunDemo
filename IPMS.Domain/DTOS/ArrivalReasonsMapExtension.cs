using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Domain.DTOS
{
    public static class ArrivalReasonsMapExtension
    {
        public static ArrivalReasonVO MapToDTO(this ArrivalReason data)
        {
            ArrivalReasonVO reasonVo = new ArrivalReasonVO();
            if (data != null)
            {
                reasonVo.ArrivalReasonID = data.ArrivalReasonID;
                reasonVo.VCN = data.VCN;
                reasonVo.Reason = data.Reason;
                reasonVo.RecordStatus = data.RecordStatus;
                reasonVo.CreatedBy = data.CreatedBy;
                reasonVo.CreatedDate = data.CreatedDate;
                reasonVo.ModifiedBy = data.ModifiedBy;
                reasonVo.ModifiedDate = data.ModifiedDate;
            }
            return reasonVo;
        }
        public static ArrivalReason MapToEntity(this ArrivalReasonVO vo)
        {
            ArrivalReason reason = new ArrivalReason();
            if (vo != null)
            {
                reason.ArrivalReasonID = vo.ArrivalReasonID;
                reason.VCN = vo.VCN;
                reason.Reason = vo.Reason;
                reason.RecordStatus = vo.RecordStatus;
                reason.CreatedBy = vo.CreatedBy;
                reason.CreatedDate = vo.CreatedDate;
                reason.ModifiedBy = vo.ModifiedBy;
                reason.ModifiedDate = vo.ModifiedDate;
            }
            return reason;
        }

        public static List<ArrivalReason> MapToEntity(this List<ArrivalReasonVO> vos)
        {
            List<ArrivalReason> reasonEntities = new List<ArrivalReason>();
            if (vos != null)
            {
                foreach (var addressvo in vos)
                {
                    reasonEntities.Add(addressvo.MapToEntity());
                }
            }
            return reasonEntities;
        }

        public static List<ArrivalReason> MapToEntityArray(this List<string> reasonarray)
        {
            List<ArrivalReason> arrivalReasons = new List<ArrivalReason>();
            if (reasonarray != null)
            {
                foreach (var berthKey in reasonarray)
                {
                    ArrivalReason reason = new ArrivalReason();
                    reason.Reason = berthKey;
                    arrivalReasons.Add(reason);
                }
            }
            return arrivalReasons;
        }



        //public static List<ArrivalReason> MapToEntityArray(this List<string> vos)
        //{
        //    List<ArrivalReason> reasonEntities = new List<ArrivalReason>();
        //    foreach (var addressvo in vos)
        //    {
        //        reasonEntities.Add(addressvo.MapToEntityString());
        //    }
        //    return reasonEntities;
        //}

        //public static ArrivalReason MapToEntityString(string voReason)
        //{
        //    ArrivalReason reason = new ArrivalReason();
        //    //reason.ArrivalReasonID = vo.ArrivalReasonID;
        //    //reason.VCN = vo.VCN;
        //    reason.Reason = voReason;
        //   // reason.RecordStatus = vo.RecordStatus;
        //    //reason.CreatedBy = vo.CreatedBy;
        //    //reason.CreatedDate = vo.CreatedDate;
        //    //reason.ModifiedBy = vo.ModifiedBy;
        //    //reason.ModifiedDate = vo.ModifiedDate;
        //    return reason;
        //}

    }
}

