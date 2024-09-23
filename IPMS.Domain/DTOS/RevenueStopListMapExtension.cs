using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class RevenueStopListMapExtension
    {
        /// <summary>
        /// Data Transfer from DTO to Entity 
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static RevenueStopList MapToEntity(this RevenueStopListVO vo)
        {
            RevenueStopList revenueSlDetails = new RevenueStopList();
            revenueSlDetails.AgentAccountID = vo.AgentAccountID;
            revenueSlDetails.AgentID = vo.AgentID;
            revenueSlDetails.CreatedBy = vo.CreatedBy;
            revenueSlDetails.CreatedDate = vo.CreatedDate;
            revenueSlDetails.ModifiedBy = vo.ModifiedBy;
            revenueSlDetails.ModifiedDate = vo.ModifiedDate;
            revenueSlDetails.PortCode = vo.PortCode;
            revenueSlDetails.RecordStatus = vo.RecordStatus;
            revenueSlDetails.RevenueStopListID = vo.RevenueStopListID;
            revenueSlDetails.StopDate = DateTime.Parse(vo.StopDate, CultureInfo.InvariantCulture);
            // revenueSlDetails.RevenueAccountStatus = vo.RevenueAccountStatus.MapToEntity();
            return revenueSlDetails;
        }

        /// <summary>
        ///  Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RevenueStopListVO MapToDTO(this RevenueStopList data)
        {
            RevenueStopListVO revenueSlVO = new RevenueStopListVO();
            revenueSlVO.AgentAccountID = data.AgentAccountID;
            revenueSlVO.AgentID = data.AgentID;
            revenueSlVO.CreatedBy = data.CreatedBy;
            revenueSlVO.CreatedDate = data.CreatedDate;
            revenueSlVO.ModifiedBy = data.ModifiedBy;
            revenueSlVO.ModifiedDate = data.ModifiedDate;
            revenueSlVO.PortCode = data.PortCode;
            revenueSlVO.RecordStatus = data.RecordStatus;
            revenueSlVO.RevenueStopListID = data.RevenueStopListID;
            revenueSlVO.StopDate = data.StopDate != null ? data.StopDate.ToString() : "";
            // revenueSlVO.AccountStatus=data.RevenueAccountStatus.First() .MapToDTO();

            return revenueSlVO;
        }

        /// <summary>
        /// Data List Transfer from DTO to Entity 
        /// </summary>
        /// <param name="vos"></param>
        /// <returns></returns>
        public static List<RevenueStopList> MapToEntity(this List<RevenueStopListVO> vos)
        {
            List<RevenueStopList> revenueStopList = new List<RevenueStopList>();
            foreach (var rvnSlVO in vos)
            {
                revenueStopList.Add(rvnSlVO.MapToEntity());
            }
            return revenueStopList;
        }

        /// <summary>
        ///  Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<RevenueStopListVO> MapToDTO(this List<RevenueStopList> data)
        {
            List<RevenueStopListVO> rslVO = new List<RevenueStopListVO>();
            foreach (var rsl in data)
            {
                rslVO.Add(rsl.MapToDTO());
            }
            return rslVO;
        }

    }
}

