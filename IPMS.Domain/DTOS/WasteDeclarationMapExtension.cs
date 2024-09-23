using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class WasteDeclarationMapExtension
    {
        public static WasteDeclarationVO MapToDTO(this WasteDeclaration data)
        {
            WasteDeclarationVO wasteDeclarationVo = new WasteDeclarationVO();
            if (data != null)
            {
                wasteDeclarationVo.WasteDeclarationID = data.WasteDeclarationID;
                wasteDeclarationVo.VCN = data.VCN;
                wasteDeclarationVo.MarpolCode = data.MarpolCode;
                wasteDeclarationVo.ClassCode = data.ClassCode;
                wasteDeclarationVo.LicenseRequestID = data.LicenseRequestID;
                wasteDeclarationVo.Quantity = data.Quantity;
                wasteDeclarationVo.DeclarationName = data.DeclarationName;                
                wasteDeclarationVo.Others = data.Others;
                wasteDeclarationVo.RecordStatus = data.RecordStatus;
                wasteDeclarationVo.CreatedBy = data.CreatedBy;
                wasteDeclarationVo.CreatedDate = data.CreatedDate;
                wasteDeclarationVo.ModifiedBy = data.ModifiedBy;
                wasteDeclarationVo.ModifiedDate = data.ModifiedDate;
            }
            return wasteDeclarationVo;
        }
        public static WasteDeclaration MapToEntity(this WasteDeclarationVO vo)
        {
            WasteDeclaration wasteDeclaration = new WasteDeclaration();
            if (vo != null)
            {
                wasteDeclaration.WasteDeclarationID = vo.WasteDeclarationID;
                wasteDeclaration.VCN = vo.VCN;
                wasteDeclaration.MarpolCode = vo.MarpolCode;
                wasteDeclaration.ClassCode = vo.ClassCode;
                wasteDeclaration.LicenseRequestID = vo.LicenseRequestID;
                wasteDeclaration.Quantity = vo.Quantity;
                wasteDeclaration.DeclarationName = vo.DeclarationName;
                wasteDeclaration.Others = vo.Others;
                wasteDeclaration.RecordStatus = vo.RecordStatus;
                wasteDeclaration.CreatedBy = vo.CreatedBy;
                wasteDeclaration.CreatedDate = vo.CreatedDate;
                wasteDeclaration.ModifiedBy = vo.ModifiedBy;
                wasteDeclaration.ModifiedDate = vo.ModifiedDate;
            }
            return wasteDeclaration;
        }

        public static List<WasteDeclaration> MapToEntity(this IEnumerable<WasteDeclarationVO> vos)
        {
            var wasteDeclarationList = new List<WasteDeclaration>();
            if (vos != null)
            {
                foreach (var item in vos)
                {
                    wasteDeclarationList.Add(item.MapToEntity());
                }
            }
            return wasteDeclarationList;


        }
        public static List<WasteDeclarationVO> MapToDto(this IEnumerable<WasteDeclaration> Entities)
        {
            var wasteDeclarationVOList = new List<WasteDeclarationVO>();
            if (Entities != null)
            {
                foreach (var item in Entities)
                {
                    wasteDeclarationVOList.Add(item.MapToDTO());
                }
            }
            return wasteDeclarationVOList;
        }
    }
}
