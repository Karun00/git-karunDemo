using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class DivingRequestDiverMapExtension
    {
        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="divingRequestDriverVos"></param>
        /// <param name="diverType"></param>
        /// <returns></returns>
        public static List<DivingRequestDiver> MapToEntity(this List<DivingRequestDiverVO> divingRequestDriverVos, string diverType)
        {
            List<DivingRequestDiver> DivingRequestDivers = new List<DivingRequestDiver>();
            if (divingRequestDriverVos != null)
            {
            foreach (DivingRequestDiverVO vo in divingRequestDriverVos)
            {
                vo.DiverType = diverType;
                DivingRequestDivers.Add(vo.MapToEntity());
            }
            }

            return DivingRequestDivers;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DivingRequestDiverVO MapToDto(this DivingRequestDiver data)
        {
            return new DivingRequestDiverVO
            {
                
                DivingRequestID = data.DivingRequestID,
                DivingRequestDiverID = data.DivingRequestDiverID,
                DiverName = data.DiverName,
                TimeLeftSurface = data.TimeArrivedSurface == null ? "" : Convert.ToString(data.TimeLeftSurface, CultureInfo.InvariantCulture),
                TimeArrivedSurface = data.TimeLeftSurface == null ? "" : Convert.ToString(data.TimeArrivedSurface, CultureInfo.InvariantCulture),
                RecordStatus = data.RecordStatus,
                CreatedBy = data.CreatedBy,
                CreatedDate = data.CreatedDate,
                ModifiedBy = data.ModifiedBy,
                ModifiedDate = data.ModifiedDate,
                DiverType = data.DiverType
            };
        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static DivingRequestDiver MapToEntity(this DivingRequestDiverVO vo)
        {
            DivingRequestDiver DivingRequestDiver = new DivingRequestDiver();
            if (vo != null)
            {
            DivingRequestDiver.DivingRequestID = vo.DivingRequestID;
            DivingRequestDiver.DivingRequestDiverID = vo.DivingRequestDiverID;
            DivingRequestDiver.DiverName = vo.DiverName;
            if (vo.TimeLeftSurface != null)
            {
                DivingRequestDiver.TimeLeftSurface = DateTime.Parse(vo.TimeLeftSurface, CultureInfo.InvariantCulture);
            }
            if (vo.TimeArrivedSurface != null)
            {
                DivingRequestDiver.TimeArrivedSurface = DateTime.Parse(vo.TimeArrivedSurface,CultureInfo.InvariantCulture);
            }
            DivingRequestDiver.RecordStatus = vo.RecordStatus;
            DivingRequestDiver.CreatedBy = vo.CreatedBy;
            DivingRequestDiver.CreatedDate = vo.CreatedDate;
            DivingRequestDiver.ModifiedBy = vo.ModifiedBy;
            DivingRequestDiver.ModifiedDate = vo.ModifiedDate;
            DivingRequestDiver.DiverType = vo.DiverType;
            }
            return DivingRequestDiver;
        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="diverType"></param>
        /// <returns></returns>
        public static DivingRequestDiver MapToEntity1(this DivingRequestDiverVO vo, string diverType)
        {
            DivingRequestDiver DivingRequestDiver = new DivingRequestDiver();
            if (diverType != null)
            {
            DivingRequestDiver.DivingRequestID = vo.DivingRequestID;
            DivingRequestDiver.DivingRequestDiverID = vo.DivingRequestDiverID;
            DivingRequestDiver.DiverName = vo.DiverName;
            if (vo.TimeLeftSurface != "")
            {
                DivingRequestDiver.TimeLeftSurface = DateTime.Parse(vo.TimeLeftSurface, CultureInfo.InvariantCulture);
            }
            if (vo.TimeArrivedSurface != "")
            {
                DivingRequestDiver.TimeArrivedSurface = DateTime.Parse(vo.TimeArrivedSurface, CultureInfo.InvariantCulture);
            }
            DivingRequestDiver.RecordStatus = vo.RecordStatus;
            DivingRequestDiver.CreatedBy = vo.CreatedBy;
            DivingRequestDiver.CreatedDate = vo.CreatedDate;
            DivingRequestDiver.ModifiedBy = vo.ModifiedBy;
            DivingRequestDiver.ModifiedDate = vo.ModifiedDate;
            DivingRequestDiver.DiverType = diverType;
            }
            return DivingRequestDiver;
        }
    }
}
