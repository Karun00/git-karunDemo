using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class SuppFloatingCraneMapExtension
    {
        public static List<SuppFloatingCraneVO> MapToDTO(this List<SuppFloatingCrane> suppFloatingCranes)
        {
            List<SuppFloatingCraneVO> lstSuppFloatingCraneVO = new List<SuppFloatingCraneVO>();

            foreach (SuppFloatingCrane suppFloatingCrane in suppFloatingCranes)
            {
                lstSuppFloatingCraneVO.Add(suppFloatingCrane.MapToDTO());
            }

            return lstSuppFloatingCraneVO;
        }

        public static List<SuppFloatingCrane> MapToEntity(this List<SuppFloatingCraneVO> suppFloatingCranesVO)
        {
            List<SuppFloatingCrane> lstSuppFloatingCrane = new List<SuppFloatingCrane>();

            foreach (SuppFloatingCraneVO suppFloatingCraneVO in suppFloatingCranesVO)
            {
                lstSuppFloatingCrane.Add(suppFloatingCraneVO.MapToEntity());
            }

            return lstSuppFloatingCrane;
        }

        public static SuppFloatingCraneVO MapToDTO(this SuppFloatingCrane data)
        {
            return new SuppFloatingCraneVO
             {
                 //suppFloatingCraneVO = new SuppFloatingCraneVO();

                 SuppFloatingCraneID = data.SuppFloatingCraneID,
                 SuppServiceRequestID = data.SuppServiceRequestID,
                 MassMT = data.MassMT,
                 Quantity = data.Quantity,
                 Description = data.Description,
                 RecordStatus = data.RecordStatus,
                 CreatedBy = data.CreatedBy,
                 CreatedDate = data.CreatedDate,
                 ModifiedBy = data.ModifiedBy,
                 ModifiedDate = data.ModifiedDate

                 //return suppFloatingCraneVO;
             };
        }

        public static SuppFloatingCrane MapToEntity(this SuppFloatingCraneVO vo)
        {
            return new SuppFloatingCrane
             {
                 //suppFloatingCrane = new SuppFloatingCrane();

                 SuppFloatingCraneID = vo.SuppFloatingCraneID,
                 SuppServiceRequestID = vo.SuppServiceRequestID,
                 MassMT = vo.MassMT,
                 Quantity = vo.Quantity,
                 Description = vo.Description,
                 RecordStatus = vo.RecordStatus,
                 CreatedBy = vo.CreatedBy,
                 CreatedDate = vo.CreatedDate,
                 ModifiedBy = vo.ModifiedBy,
                 ModifiedDate = vo.ModifiedDate

                 //return suppFloatingCrane;
             };
        }
    }
}
