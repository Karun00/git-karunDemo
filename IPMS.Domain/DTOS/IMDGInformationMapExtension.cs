using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class IMDGInformationMapExtension
    {
        public static IMDGInformationVO MapToDTO(this IMDGInformation data)
        {
            IMDGInformationVO imdginformationVo = new IMDGInformationVO();
            if (data != null)
            {
                imdginformationVo.IMDGInformationID = data.IMDGInformationID;
                imdginformationVo.VCN = data.VCN;
                imdginformationVo.ClassCode = data.ClassCode;
                imdginformationVo.CargoCode = data.CargoCode;
                imdginformationVo.UNNo = data.UNNo;
                imdginformationVo.Purpose = data.Purpose;
                imdginformationVo.Others = data.Others;

                imdginformationVo.NoofContainer = data.NoofContainer;
                imdginformationVo.Quantity = data.Quantity;
                imdginformationVo.RecordStatus = data.RecordStatus;
                imdginformationVo.CreatedBy = data.CreatedBy;
                imdginformationVo.CreatedDate = data.CreatedDate;
                imdginformationVo.ModifiedBy = data.ModifiedBy;
                imdginformationVo.ModifiedDate = data.ModifiedDate;
            }
            return imdginformationVo;
        }
        public static IMDGInformation MapToEntity(this IMDGInformationVO vo)
        {
            IMDGInformation imdginformation = new IMDGInformation();
            if (vo != null)
            {
                imdginformation.IMDGInformationID = vo.IMDGInformationID;
                imdginformation.VCN = vo.VCN;
                imdginformation.ClassCode = vo.ClassCode;
                imdginformation.CargoCode = vo.CargoCode;
                imdginformation.UNNo = vo.UNNo;
                imdginformation.Purpose = vo.Purpose;
                imdginformation.Others = vo.Others;
                imdginformation.NoofContainer = vo.NoofContainer;
                imdginformation.Quantity = vo.Quantity;
                imdginformation.RecordStatus = vo.RecordStatus;
                imdginformation.CreatedBy = vo.CreatedBy;
                imdginformation.CreatedDate = vo.CreatedDate;
                imdginformation.ModifiedBy = vo.ModifiedBy;
                imdginformation.ModifiedDate = vo.ModifiedDate;
            }
            return imdginformation;
        }

        public static List<IMDGInformation> MapToEntity(this IEnumerable<IMDGInformationVO> vos)
        {
            var imdginformationList = new List<IMDGInformation>();
            if (vos != null)
            {
                foreach (var item in vos)
                {
                    imdginformationList.Add(item.MapToEntity());
                }
            }
            return imdginformationList;


        }
        public static List<IMDGInformationVO> MapToDto(this IEnumerable<IMDGInformation> Entities)
        {
            var imdginformationVOList = new List<IMDGInformationVO>();
            if (Entities != null)
            {
                foreach (var item in Entities)
                {
                    imdginformationVOList.Add(item.MapToDTO());
                }
            }
            return imdginformationVOList;
        }
    }
}
