using System;
using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class CargoManifestMapExtension
    {
        public static CargoManifestVO MapToDto(this CargoManifest data)
        {
            CargoManifestVO CMVO = new CargoManifestVO();
            if (data != null)
            {
                CMVO.CargoManifestID = data.CargoManifestID;
                CMVO.FirstMoveDateTime = data.FirstMoveDateTime.ToString();
                CMVO.LastMoveDateTime = data.LastMoveDateTime.ToString();
                CMVO.VCN = data.VCN;
                CMVO.UOMCode = data.UOMCode;
                CMVO.RecordStatus = data.RecordStatus;
                CMVO.CreatedBy = data.CreatedBy;
                CMVO.CreatedDate = data.CreatedDate;
                CMVO.ModifiedBy = data.ModifiedBy;
                CMVO.ModifiedDate = data.ModifiedDate;
            }
            return CMVO;
        }

        public static CargoManifest MapToEntity(this CargoManifestVO cmvo)
        {
            CargoManifest data = new CargoManifest();
            if (cmvo != null)
            {
                data.CargoManifestID = cmvo.CargoManifestID;
                data.FirstMoveDateTime = DateTime.Parse(cmvo.FirstMoveDateTime, CultureInfo.InvariantCulture);
                data.LastMoveDateTime = DateTime.Parse(cmvo.LastMoveDateTime, CultureInfo.InvariantCulture);
                data.VCN = cmvo.VCN;
                data.UOMCode = cmvo.UOMCode;
                data.RecordStatus = cmvo.RecordStatus;
                data.CreatedBy = cmvo.CreatedBy;
                data.CreatedDate = cmvo.CreatedDate;
                data.ModifiedBy = cmvo.ModifiedBy;
                data.ModifiedDate = cmvo.ModifiedDate;
            }
            return data;
        }

        public static CargoManifestDtl MapToEntity(this CargoManifestDtlVO data)
        {
            
            CargoManifestDtl result = new CargoManifestDtl
            {
                CargoManifestDtlID = data.CargoManifestDtlID,
                CargoManifestID = data.CargoManifestID,
                CargoTypeCode = data.CargoTypeCode,
                Quantity = data.Quantity.GetValueOrDefault(),
                UOMCode = data.UOMCode,
                OutTurn = data.OutTurn.GetValueOrDefault(),
                RecordStatus = data.RecordStatus,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate
            };
            return result;
        }

        public static List<CargoManifestDtl> MapToEntity(this IEnumerable<CargoManifestDtlVO> cargoManifestDtlVOList)
        {
            List<CargoManifestDtl> cargomanifestdtls = new List<CargoManifestDtl>();
            if (cargoManifestDtlVOList != null)
            {
                foreach (var item in cargoManifestDtlVOList)
                {
                    cargomanifestdtls.Add(item.MapToEntity());
                }
            }
            return cargomanifestdtls;
        }
    }
}
