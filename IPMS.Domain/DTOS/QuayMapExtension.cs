using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class QuayMapExtension
    {

        public static List<QuayVO> MapToListDtoMobile(this IEnumerable<Quay> quayList)
        {
            List<QuayVO> quayvoList = new List<QuayVO>();
            if (quayList != null)
            {
                foreach (var data in quayList)
                {
                    quayvoList.Add(data.MapToDtoMobile());

                }
            }
            return quayvoList;
        }

        public static List<QuayVO> MapToListDto(this IEnumerable<Quay> quayList)
        {
            List<QuayVO> quayvoList = new List<QuayVO>();
            if (quayList != null)
            {
                foreach (var data in quayList)
                {
                    quayvoList.Add(data.MapToDto());

                }
            }
            return quayvoList;
        }
        public static List<Quay> MapToListEntity(this IEnumerable<QuayVO> quayVoList)
        {
            List<Quay> quayList = new List<Quay>();
            if (quayVoList != null)
            {
                foreach (var data in quayVoList)
                {
                    quayList.Add(data.MapToEntity());

                }
            }
            return quayList;
        }

        public static QuayVO MapToDtoMobile(this Quay data)
        {
            QuayVO quayvo = new QuayVO();
            if (data != null)
            {
                quayvo.PortCode = data.PortCode;
                quayvo.QuayCode = data.QuayCode;
                quayvo.ShortName = data.ShortName;
                quayvo.QuayName = data.QuayName;
                quayvo.QuayLength = data.QuayLength;
                quayvo.Description = data.Description;
                quayvo.RecordStatus = data.RecordStatus;
                quayvo.CreatedBy = data.CreatedBy;
                quayvo.CreatedDate = data.CreatedDate;
                quayvo.ModifiedBy = data.ModifiedBy;
                quayvo.ModifiedDate = data.ModifiedDate;
                quayvo.QuayKey = data.PortCode + '.' + data.QuayCode;
                quayvo.berthlist = data.Berths.MapToListDtoMobile();
            }
            return quayvo;
        }

        public static QuayVO MapToDto(this Quay data)
        {
            QuayVO quayvo = new QuayVO();
            if (data != null)
            {
                quayvo.PortCode = data.PortCode;
                quayvo.QuayCode = data.QuayCode;
                quayvo.ShortName = data.ShortName;
                quayvo.QuayName = data.QuayName;
                quayvo.QuayLength = data.QuayLength;
                quayvo.Description = data.Description;
                quayvo.RecordStatus = data.RecordStatus;
                quayvo.CreatedBy = data.CreatedBy;
                quayvo.CreatedDate = data.CreatedDate;
                quayvo.ModifiedBy = data.ModifiedBy;
                quayvo.ModifiedDate = data.ModifiedDate;
                quayvo.QuayKey = data.PortCode + '.' + data.QuayCode;
                quayvo.berthlist = data.Berths.MapToListDto();
            }
            return quayvo;
        }
        public static Quay MapToEntity(this QuayVO quayVo)
        {
            Quay quay = new Quay();
            if (quayVo != null)
            {
                quay.PortCode = quayVo.PortCode;
                quay.QuayCode = quayVo.QuayCode;
                quay.ShortName = quayVo.ShortName;
                quay.QuayName = quayVo.QuayName;
                quay.QuayLength = quayVo.QuayLength;
                quay.Description = quayVo.Description;
                quay.RecordStatus = quayVo.RecordStatus;
                quay.CreatedBy = quayVo.CreatedBy;
                quay.CreatedDate = quayVo.CreatedDate;
                quay.ModifiedBy = quayVo.ModifiedBy;
                quay.ModifiedDate = quayVo.ModifiedDate;

                quay.Berths = quayVo.berthlist.MapToListEntity();
            }
            return quay;
        }
    }
}
