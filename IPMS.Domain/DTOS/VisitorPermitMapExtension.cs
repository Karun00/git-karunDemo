using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class VisitorPermitMapExtension
    {
        public static List<VisitorPermit> MapToEntity(this IEnumerable<VisitorPermitVO> vos)
        {
            List<VisitorPermit> entities = new List<VisitorPermit>();
            foreach (var vo in vos)
            {
                entities.Add(vo.MapToEntity());
            }
            return entities;
        }
        public static List<VisitorPermitVO> MapToDTO(this IEnumerable<VisitorPermit> entities)
        {
            List<VisitorPermitVO> vos = new List<VisitorPermitVO>();
            foreach (var entity in entities)
            {
                vos.Add(entity.MapToDTO());
            }
            return vos;

        }
        public static VisitorPermitVO MapToDTO(this VisitorPermit data)
        {
            VisitorPermitVO Vo = new VisitorPermitVO();
            Vo.VisitorPermitID = data.VisitorPermitID;
            Vo.PermitRequestID = data.PermitRequestID;
            Vo.AuthorizedPersonName = data.AuthorizedPersonName;
            Vo.Division = data.Division;
            Vo.PositionHeld = data.PositionHeld;
            Vo.CompanyName = data.CompanyName;
            Vo.Reason = data.Reason;
            Vo.EscortName = data.EscortName;
            Vo.TelephoneNo = data.TelephoneNo;
            Vo.PermitNo = data.PermitNo;
            Vo.PermitCode = data.PermitCode;
            Vo.RecordStatus = data.RecordStatus;
            Vo.CreatedBy = data.CreatedBy;
            Vo.CreatedDate = data.CreatedDate;
            Vo.ModifiedBy = data.ModifiedBy;
            Vo.CreatedBy = data.CreatedBy;
            Vo.ModifiedDate = data.ModifiedDate;
            return Vo;
        }
        public static VisitorPermit MapToEntity(this VisitorPermitVO VO)
        {
            VisitorPermit data = new VisitorPermit();
            data.VisitorPermitID = VO.VisitorPermitID;
            data.PermitRequestID = VO.PermitRequestID;
            data.AuthorizedPersonName = VO.AuthorizedPersonName;
            data.CompanyName = VO.CompanyName;
            data.Division = VO.Division;
            data.PositionHeld = VO.PositionHeld;
            data.Reason = VO.Reason;
            data.EscortName = VO.EscortName;
            data.TelephoneNo = VO.TelephoneNo;
            data.PermitNo = VO.PermitNo;
            data.PermitCode = VO.PermitCode;
            data.RecordStatus = VO.RecordStatus;
            data.CreatedBy = VO.CreatedBy;
            data.CreatedDate = VO.CreatedDate;
            data.ModifiedBy = VO.ModifiedBy;
            data.CreatedBy = VO.CreatedBy;
            data.ModifiedDate = VO.ModifiedDate;
            return data;
        }
        public static VisitorPermitVO MapToDTOObj(this IEnumerable<VisitorPermit> VisitorPermit)
        {
            var VisitorPermitVOList = new VisitorPermitVO();
            foreach (var data in VisitorPermit)
            {
                VisitorPermitVOList.VisitorPermitID = data.VisitorPermitID;
                VisitorPermitVOList.PermitRequestID = data.PermitRequestID;
                VisitorPermitVOList.CompanyName = data.CompanyName;
                VisitorPermitVOList.AuthorizedPersonName = data.AuthorizedPersonName;
                VisitorPermitVOList.Division = data.Division;
                VisitorPermitVOList.PositionHeld = data.PositionHeld;
                VisitorPermitVOList.Reason = data.Reason;
                VisitorPermitVOList.EscortName = data.EscortName;
                VisitorPermitVOList.TelephoneNo = data.TelephoneNo;
                VisitorPermitVOList.PermitNo = data.PermitNo;
                VisitorPermitVOList.PermitCode = data.PermitCode;
                VisitorPermitVOList.RecordStatus = data.RecordStatus;
                VisitorPermitVOList.CreatedBy = data.CreatedBy;
                VisitorPermitVOList.CreatedDate = data.CreatedDate;
                VisitorPermitVOList.ModifiedBy = data.ModifiedBy;
                VisitorPermitVOList.CreatedBy = data.CreatedBy;
                VisitorPermitVOList.ModifiedDate = data.ModifiedDate;
            }
            return VisitorPermitVOList;
        }
    
    }
}
