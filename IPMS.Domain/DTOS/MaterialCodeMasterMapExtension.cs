using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;


namespace IPMS.Domain.DTOS
{
    public static class MaterialCodeMasterMapExtension
    {
        public static List<MaterialCodeMasterVO> MapToDTO(this List<MaterialCodeMaster> materialcodes)
        {
            List<MaterialCodeMasterVO> materialcodeVos = new List<MaterialCodeMasterVO>();
            if (materialcodes != null)
            {
                foreach (var materialcode in materialcodes)
                {
                    materialcodeVos.Add(materialcode.MapToDTO());

                }
            }
            return materialcodeVos;
        }

        public static MaterialCodeMasterVO MapToDTO(this MaterialCodeMaster data)
        {
            MaterialCodeMasterVO VO = new MaterialCodeMasterVO();
            if (data != null)
            {
                VO.GroupCode = data.GroupCode;
                VO.MaterialCode = data.MaterialCode;
                VO.Remarks = data.Remarks;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
            }
            return VO;
        }
    }
}
