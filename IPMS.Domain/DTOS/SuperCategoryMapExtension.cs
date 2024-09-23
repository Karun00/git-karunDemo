using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;

namespace IPMS.Domain.DTOS
{
    public static class SuperCategoryMapExtension
    {


        public static SuperCategoryVO MapToDto(this SuperCategory data)
        {
            SuperCategoryVO VO = new SuperCategoryVO();
            if (data != null)
            {

                VO.SupCatCode = data.SupCatCode;
                VO.SupCatName = data.SupCatName;
                VO.RecordStatus = data.RecordStatus;
                VO.CreatedBy = data.CreatedBy;
                VO.CreatedDate = data.CreatedDate;
                VO.ModifiedBy = data.ModifiedBy;
                VO.ModifiedDate = data.ModifiedDate;
            }

            return VO;
        }
        public static SuperCategory MapToEntity(this SuperCategoryVO vo)
        {
            SuperCategory data = new SuperCategory();
            if (vo != null)
            {
                data.SupCatCode = vo.SupCatCode;
                data.SupCatName = vo.SupCatName;
                data.RecordStatus = vo.RecordStatus;
                data.CreatedBy = vo.CreatedBy;
                data.CreatedDate = vo.CreatedDate;
                data.ModifiedBy = vo.ModifiedBy;
                data.ModifiedDate = vo.ModifiedDate;
            }
            return data;
        }



    }
}
