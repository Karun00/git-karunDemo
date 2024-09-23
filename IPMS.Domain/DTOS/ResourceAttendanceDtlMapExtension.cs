using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class ResourceAttendanceDtlMapExtension
    {
        /// <summary>
        /// Data Transfer from DTO to Entity 
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static ResourceAttendanceDtl MapToEntity(this ResourceAttendanceDtlVO vo)
        {
            ResourceAttendanceDtl ratDetails = new ResourceAttendanceDtl();
            if (vo != null)
            {
                ratDetails.AttendanceStatus = vo.AttendanceStatus;
                ratDetails.CreatedBy = vo.CreatedBy;
                ratDetails.CreatedDate = vo.CreatedDate;
                ratDetails.EmployeeID = vo.EmployeeID;
                ratDetails.ModifiedBy = vo.ModifiedBy;
                ratDetails.ModifiedDate = vo.ModifiedDate;
                ratDetails.RecordStatus = vo.RecordStatus;
                ratDetails.ResourceAttendanceDtlID = vo.ResourceAttendanceDtlID;
                ratDetails.ResourceAttendanceID = vo.ResourceAttendanceID;
                ratDetails.AttendanceDate = vo.JoiningDate; //DateTime.Parse(vo.JoiningDate, CultureInfo.InvariantCulture); ;
                ratDetails.ShiftID = vo.ShiftID;


            }

            return ratDetails;
        }

        /// <summary>
        ///  Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResourceAttendanceDtlVO MapToDTO(this ResourceAttendanceDtl data)
        {
            ResourceAttendanceDtlVO ratDtVO = new ResourceAttendanceDtlVO();

            if (data != null)
            {
                ratDtVO.AttendanceStatus = data.AttendanceStatus;
                ratDtVO.CreatedBy = data.CreatedBy;
                ratDtVO.CreatedDate = data.CreatedDate;
                ratDtVO.EmployeeID = data.EmployeeID;
                ratDtVO.ModifiedBy = data.ModifiedBy;
                ratDtVO.ModifiedDate = data.ModifiedDate;
                ratDtVO.RecordStatus = data.RecordStatus;
                ratDtVO.ResourceAttendanceDtlID = data.ResourceAttendanceDtlID;
                ratDtVO.ResourceAttendanceID = data.ResourceAttendanceID;
            }

            return ratDtVO;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity 
        /// </summary>
        /// <param name="vos"></param> 
        /// <returns></returns>
        public static List<ResourceAttendanceDtl> MapToEntity(this List<ResourceAttendanceDtlVO> vos)
        {
            List<ResourceAttendanceDtl> resAttendanceDetails = new List<ResourceAttendanceDtl>();
            if (vos != null)
            {
                foreach (var ratDtVO in vos)
                {
                    resAttendanceDetails.Add(ratDtVO.MapToEntity());
                }
            }
            return resAttendanceDetails;
        }

        /// <summary>
        ///  Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<ResourceAttendanceDtlVO> MapToDTO(this List<ResourceAttendanceDtl> data)
        {
            List<ResourceAttendanceDtlVO> ratDtVO = new List<ResourceAttendanceDtlVO>();
            if (data != null)
            {
                foreach (var rat in data)
                {
                    ratDtVO.Add(rat.MapToDTO());
                }
            }
            return ratDtVO;
        }
    }
}
