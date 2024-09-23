using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class ResourceAttendanceMapExtension
    {
        /// <summary>
        /// Data Transfer from DTO to Entity 
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns> 
        public static ResourceAttendance MapToEntity(this ResourceAttendanceVO vo)
        {
            ResourceAttendance resAttendance = new ResourceAttendance();

            if (vo != null)
            {
                resAttendance.AttendanceDate = DateTime.Parse(vo.AttendanceDate, CultureInfo.InvariantCulture);
                resAttendance.CreatedBy = vo.CreatedBy;
                resAttendance.CreatedDate = vo.CreatedDate;
                resAttendance.ModifiedBy = vo.ModifiedBy;
                resAttendance.ModifiedDate = vo.ModifiedDate;
                resAttendance.Position = vo.Position;
                resAttendance.RecordStatus = vo.RecordStatus;
                resAttendance.ResourceAttendanceID = vo.ResourceAttendanceID;
                resAttendance.ShiftID = vo.ShiftID;
                if (vo.ResourceAttendanceDtls.Count > 0)
                {
                    resAttendance.ResourceAttendanceDtls = vo.ResourceAttendanceDtls.MapToEntity();
                }
            }

            return resAttendance;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResourceAttendanceVO MapToDTO(this ResourceAttendance data)
        {
            ResourceAttendanceVO ratVO = new ResourceAttendanceVO();

            if (data != null)
            {
                ratVO.AttendanceDate = data.AttendanceDate != null ? data.AttendanceDate.ToString() : "";
                ratVO.CreatedBy = data.CreatedBy;
                ratVO.CreatedDate = data.CreatedDate;
                ratVO.ModifiedBy = data.ModifiedBy;
                ratVO.ModifiedDate = data.ModifiedDate;
                ratVO.Position = data.Position;
                ratVO.RecordStatus = data.RecordStatus;
                ratVO.ResourceAttendanceID = data.ResourceAttendanceID;
                ratVO.ShiftID = data.ShiftID;
            }

            return ratVO;
        }

        /// <summary>
        /// Data list Transfer from DTO to Entity 
        /// </summary>
        /// <param name="vos"></param>
        /// <returns></returns>
        public static List<ResourceAttendance> MapToEntity(this List<ResourceAttendanceVO> vos)
        {
            List<ResourceAttendance> rat = new List<ResourceAttendance>();
            if (vos != null)
            {
                foreach (var value in vos)
                {
                    rat.Add(value.MapToEntity());
                }
            }
            return rat;
        }

        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<ResourceAttendanceVO> MapToDTO(this List<ResourceAttendance> data)
        {
            List<ResourceAttendanceVO> ratVO = new List<ResourceAttendanceVO>();
            if (data != null)
            {
                foreach (var value in data)
                {
                    ratVO.Add(value.MapToDTO());
                }
            }
            return ratVO;
        }
    }
}
