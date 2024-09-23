using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IPMS.Domain.DTOS
{
    public static class DivingRequestMapExtension
    {
        #region MapToDTO
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="divingrequests"></param>
        /// <returns></returns>
        public static List<DivingRequestVO> MapToDTO(this List<DivingRequest> divingrequests)
        {
            List<DivingRequestVO> divingrequestvos = new List<DivingRequestVO>();
            if (divingrequests != null)
            {
                foreach (var data in divingrequests)
                {
                    divingrequestvos.Add(data.MapToDTO());
                }
            }
            return divingrequestvos;
        }

        /// <summary>
        /// Data List Transfer from Entity DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DivingRequestVO MapToDTO(this DivingRequest data)
        {
            DivingRequestVO DivingRequestVO = new DivingRequestVO();
            if (data != null)
            {
            CultureInfo Culture = new CultureInfo("en-US");

            DivingRequestVO.Port = data.FromPortCode;

            if (data.LocationType == "O")
            {
                DivingRequestVO.LocationorQuay = data.OtherLocation.ToString();
            }
            else
            {
                DivingRequestVO.LocationorQuay = data.FromQuayCode.ToString();
            }

            DivingRequestVO.Raisedby = data.Raisedby;
            DivingRequestVO.Berth = data.FromBerthCode;
            DivingRequestVO.DRN = data.DRN;
            DivingRequestVO.OccupationReason = data.OccupationReason;
            DivingRequestVO.OccupationReasonName = data.SubCategory != null ? data.SubCategory.SubCatName : null;

            DivingRequestVO.DivingRequestID = data.DivingRequestID;
            DivingRequestVO.FromPortCode = data.FromPortCode;
            DivingRequestVO.FromQuayCode = data.FromQuayCode;
            DivingRequestVO.FromBerthCode = data.FromBerthCode;

            if (data.Bollard != null)
            {
                if (data.Bollard.Berth != null)
                {
                    DivingRequestVO.FromBerthName = data.Bollard.Berth.BerthName;
                }
                DivingRequestVO.FromBollardName = data.Bollard.BollardName;
            }
            if (data.Bollard1 != null)
            {
                DivingRequestVO.ToBollardName = data.Bollard1.BollardName;
            }
            DivingRequestVO.FromBollardCode = data.FromBollardCode;
            DivingRequestVO.ToPortCode = data.ToPortCode;
            DivingRequestVO.ToQuayCode = data.ToQuayCode;
            DivingRequestVO.ToBerthCode = data.ToBerthCode;
            DivingRequestVO.ToBollardCode = data.ToBollardCode;

            DivingRequestVO.RequiredByDate = data.RequiredByDate.ToString();
            DivingRequestVO.Remarks = data.Remarks;
            DivingRequestVO.RecordStatus = data.RecordStatus;
            DivingRequestVO.CreatedBy = data.CreatedBy;
            DivingRequestVO.CreatedDate = data.CreatedDate;
            DivingRequestVO.ModifiedBy = data.ModifiedBy;
            DivingRequestVO.ModifiedDate = data.ModifiedDate;
            DivingRequestVO.OcupationFromDate = data.OcupationFromDate == null ? null : Convert.ToString(data.OcupationFromDate, CultureInfo.InvariantCulture);
            DivingRequestVO.OcupationToDate = data.OcupationToDate == null ? null : Convert.ToString(data.OcupationToDate, CultureInfo.InvariantCulture);
            DivingRequestVO.HoursOfOccupation1 = data.HoursOfOccupation1;
            DivingRequestVO.StartTime = data.StartTime == null ? null : Convert.ToString(data.StartTime, CultureInfo.InvariantCulture);
            DivingRequestVO.StopTime = data.StopTime == null ? null : Convert.ToString(data.StopTime, CultureInfo.InvariantCulture);

            var convertedHoursOfOccupation2 = Convert.ToString(Convert.ToString(data.HoursOfOccupation2, CultureInfo.InvariantCulture).Replace('.', ':'), CultureInfo.InvariantCulture);
            DivingRequestVO.HoursOfOccupation2 = convertedHoursOfOccupation2;

            DivingRequestVO.DivingReferenceNo = data.DivingReferenceNo;
            DivingRequestVO.QuayLocation = data.QuayLocation;
            DivingRequestVO.SupervisorName = data.SupervisorName;
            DivingRequestVO.DiveTenders = data.DiveTenders;
            DivingRequestVO.LoggedDiveTimeFrom = data.LoggedDiveTimeFrom == null ? null : Convert.ToString(data.LoggedDiveTimeFrom, CultureInfo.InvariantCulture);
            DivingRequestVO.LoggedDiveTimeTo = data.LoggedDiveTimeTo == null ? null : Convert.ToString(data.LoggedDiveTimeTo, CultureInfo.InvariantCulture);
            DivingRequestVO.TimeDiveOperationCancelled = data.TimeDiveOperationCancelled == null ? null : Convert.ToString(data.TimeDiveOperationCancelled, CultureInfo.InvariantCulture);
            DivingRequestVO.DiveNature = data.DiveNature;
            DivingRequestVO.DiverDepth = data.DiverDepth;
            DivingRequestVO.BreathingMixture = data.BreathingMixture == "Y" ? true : false;
            DivingRequestVO.CompressedAir = data.CompressedAir == "Y" ? true : false;
            DivingRequestVO.DivingEquipmentUsed1 = data.DivingEquipmentUsed1;
            DivingRequestVO.DivingEquipmentUsed2 = data.DivingEquipmentUsed2;
            DivingRequestVO.TimeLeftWorkshop = data.TimeLeftWorkshop == null ? null : Convert.ToString(data.TimeLeftWorkshop, CultureInfo.InvariantCulture);
            DivingRequestVO.TimeLeftSite = data.TimeLeftSite == null ? null : Convert.ToString(data.TimeLeftSite, CultureInfo.InvariantCulture);
            DivingRequestVO.TimeArrivedWorkshop = data.TimeArrivedWorkshop == null ? null : Convert.ToString(data.TimeArrivedWorkshop, CultureInfo.InvariantCulture);
            DivingRequestVO.TimeArrivedSite = data.TimeArrivedSite == null ? null : Convert.ToString(data.TimeArrivedSite, CultureInfo.InvariantCulture);
            DivingRequestVO.DecompressionTables = data.DecompressionTables;
            DivingRequestVO.CommsCheck = data.CommsCheck == "Y" ? true : false;
            DivingRequestVO.BoilOut = data.BoilOut == "Y" ? true : false;
            DivingRequestVO.MainGas = data.MainGas == "Y" ? true : false;
            DivingRequestVO.Schedule = data.Schedule;
            DivingRequestVO.Visibility = data.Visibility;
            DivingRequestVO.SeaCondition = data.SeaCondition;
            DivingRequestVO.UnderWaterCurrents = data.UnderWaterCurrents;
            DivingRequestVO.ContaminatedWater = data.ContaminatedWater;
            DivingRequestVO.WaterTemperature = data.WaterTemperature;
            DivingRequestVO.LostDiveTime = data.LostDiveTime == null ? null : Convert.ToString(data.LostDiveTime, CultureInfo.InvariantCulture);
            DivingRequestVO.RepetiveDiveDesignation = data.RepetiveDiveDesignation;
            DivingRequestVO.SkiBoat = data.SkiBoat == "Y" ? true : false;
            DivingRequestVO.LDV = data.LDV == "Y" ? true : false;
            DivingRequestVO.Trailer = data.Trailer == "Y" ? true : false;
            DivingRequestVO.OtherLocation = Convert.ToInt32(data.OtherLocation, CultureInfo.InvariantCulture);
            DivingRequestVO.ChangeLocation = Convert.ToInt32(data.ChangeLocation, CultureInfo.InvariantCulture);
            DivingRequestVO.QuayLocation = data.QuayLocation;
            DivingRequestVO.LocationType = data.LocationType;
            DivingRequestVO.LocationName = data.Location1 != null ? data.Location1.LocationName : null;
            List<DivingRequestDiverVO> divingrequestdrivers1 = new List<DivingRequestDiverVO>();
            List<DivingRequestDiverVO> divingrequestdrivers2 = new List<DivingRequestDiverVO>();
            List<DivingRequestDiverVO> divingrequestdrivers3 = new List<DivingRequestDiverVO>();

            if (data.DivingRequestDivers.Count > 0)
            {
                List<DivingRequestDiver> lstdivingrequestdrivers = data.DivingRequestDivers.ToList();

                foreach (DivingRequestDiver diver in lstdivingrequestdrivers.ToList())
                {
                    if (diver.DiverType == "New")
                    {
                        divingrequestdrivers1.Add(diver.MapToDto());
                    }
                    else if (diver.DiverType == "StandBy")
                    {
                        divingrequestdrivers2.Add(diver.MapToDto());
                    }
                    else if (diver.DiverType == "Add")
                    {
                        divingrequestdrivers3.Add(diver.MapToDto());
                    }
                }
            }

            DivingRequestVO.DivingRequestDivers1 = divingrequestdrivers1;
            DivingRequestVO.DivingRequestDivers2 = divingrequestdrivers2;
            DivingRequestVO.DivingRequestDivers3 = divingrequestdrivers3;

            if (data.DivingCheckLists.Count > 0)
            {
                List<DivingCheckList> divingchecklist = data.DivingCheckLists.ToList();
                DivingRequestVO.DivingCheckList = divingchecklist[0].MapToDto();
            }
            else
            {
                DivingCheckListVO divingchecklist = new DivingCheckListVO();
                DivingRequestVO.DivingCheckList = divingchecklist;
                DivingRequestVO.DivingCheckList.DiveReferenceNo = data.DRN;
            }

            DivingRequestVO.ClearanceNo = data.ClearanceNo;
            }
            return DivingRequestVO;
        }
        #endregion

        #region MapToEntity
        /// <summary>
        /// Data List Transfer from DTO to Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static DivingRequest MapToEntity(this DivingRequestVO vo)
        {
            DivingRequest DivingRequest = new DivingRequest();
            CultureInfo Culture = new CultureInfo("en-US");
            if (vo != null)
            {
            DivingRequest.Port = vo.FromPortCode;
            if (vo.LocationType == "O")
            {
                DivingRequest.LocationorQuay = vo.OtherLocation.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                DivingRequest.LocationorQuay = vo.LocationorQuay;
                DivingRequest.Berth = vo.Berth;
                DivingRequest.FromBollard = vo.FromBollardName;
                DivingRequest.ToBollard = vo.ToBollardName;
                DivingRequest.FromQuayCode = vo.FromQuayCode;
                DivingRequest.FromBerthCode = vo.FromBerthCode;
                DivingRequest.FromBollardCode = vo.FromBollardCode;
                DivingRequest.ToQuayCode = vo.ToQuayCode;
                DivingRequest.ToBerthCode = vo.ToBerthCode;
                DivingRequest.ToBollardCode = vo.ToBollardCode;
                DivingRequest.QuayLocation = vo.QuayLocation;
            }

            DivingRequest.Raisedby = vo.Raisedby;
            DivingRequest.DRN = vo.DRN;
            DivingRequest.OccupationReason = vo.OccupationReason;
            DivingRequest.DivingRequestID = vo.DivingRequestID;
            DivingRequest.FromPortCode = vo.FromPortCode;
            DivingRequest.ToPortCode = vo.ToPortCode;
            DivingRequest.RequiredByDate = DateTime.Parse(vo.RequiredByDate, CultureInfo.InvariantCulture);
            DivingRequest.Remarks = vo.Remarks;
            DivingRequest.RecordStatus = vo.RecordStatus;
            DivingRequest.CreatedBy = vo.CreatedBy;
            DivingRequest.CreatedDate = vo.CreatedDate;
            DivingRequest.ModifiedBy = vo.ModifiedBy;
            DivingRequest.ModifiedDate = vo.ModifiedDate;

            if (vo.OcupationFromDate != "")
            {
                DivingRequest.OcupationFromDate = DateTime.Parse(vo.OcupationFromDate, CultureInfo.InvariantCulture);
            }

            if (vo.OcupationToDate != "")
            {
                DivingRequest.OcupationToDate = DateTime.Parse(vo.OcupationToDate, CultureInfo.InvariantCulture);
            }

            DivingRequest.HoursOfOccupation1 = vo.HoursOfOccupation1;

            if (vo.StartTime != "")
            {
                DivingRequest.StartTime = DateTime.Parse(vo.StartTime, CultureInfo.InvariantCulture);
            }

            if (vo.StopTime != "")
            {
                DivingRequest.StopTime = DateTime.Parse(vo.StopTime, CultureInfo.InvariantCulture);
            }

            if (vo.HoursOfOccupation2 != "")
            {
                string hours = Convert.ToString(vo.HoursOfOccupation2.Replace(':', '.'), Culture);
                DivingRequest.HoursOfOccupation2 = Convert.ToDecimal(hours, Culture);
            }
            DivingRequest.DivingReferenceNo = vo.DivingReferenceNo;

            DivingRequest.SupervisorName = vo.SupervisorName;
            DivingRequest.DiveTenders = vo.DiveTenders;

            if (vo.LoggedDiveTimeFrom != "")
            {
                DivingRequest.LoggedDiveTimeFrom = DateTime.Parse(vo.LoggedDiveTimeFrom, CultureInfo.InvariantCulture);
            }

            if (vo.LoggedDiveTimeTo != "")
            {
                DivingRequest.LoggedDiveTimeTo = DateTime.Parse(vo.LoggedDiveTimeTo, CultureInfo.InvariantCulture);
            }

            if (vo.TimeDiveOperationCancelled != "")
            {
                DivingRequest.TimeDiveOperationCancelled = DateTime.Parse(vo.TimeDiveOperationCancelled, CultureInfo.InvariantCulture);
            }

            DivingRequest.DiveNature = vo.DiveNature;
            DivingRequest.DiverDepth = vo.DiverDepth;
            DivingRequest.BreathingMixture = vo.BreathingMixture == true ? "Y" : "N";
            DivingRequest.CompressedAir = vo.CompressedAir == true ? "Y" : "N";
            DivingRequest.DivingEquipmentUsed1 = vo.DivingEquipmentUsed1;
            DivingRequest.DivingEquipmentUsed2 = vo.DivingEquipmentUsed2;

            if (vo.TimeLeftWorkshop != "")
            {
                DivingRequest.TimeLeftWorkshop = DateTime.Parse(vo.TimeLeftWorkshop, CultureInfo.InvariantCulture);
            }

            if (vo.TimeLeftSite != "")
            {
                DivingRequest.TimeLeftSite = DateTime.Parse(vo.TimeLeftSite, CultureInfo.InvariantCulture);
            }

            if (vo.TimeArrivedWorkshop != "")
            {
                DivingRequest.TimeArrivedWorkshop = DateTime.Parse(vo.TimeArrivedWorkshop, CultureInfo.InvariantCulture);
            }

            if (vo.TimeArrivedSite != "")
            {
                DivingRequest.TimeArrivedSite = DateTime.Parse(vo.TimeArrivedSite, CultureInfo.InvariantCulture);
            }

            DivingRequest.DecompressionTables = vo.DecompressionTables;
            DivingRequest.CommsCheck = vo.CommsCheck == true ? "Y" : "N";
            DivingRequest.BoilOut = vo.BoilOut == true ? "Y" : "N";
            DivingRequest.MainGas = vo.MainGas == true ? "Y" : "N";
            DivingRequest.Schedule = vo.Schedule;
            DivingRequest.Visibility = vo.Visibility;
            DivingRequest.SeaCondition = vo.SeaCondition;
            DivingRequest.UnderWaterCurrents = vo.UnderWaterCurrents;
            DivingRequest.ContaminatedWater = vo.ContaminatedWater;
            DivingRequest.WaterTemperature = vo.WaterTemperature;

            if (vo.LostDiveTime != "")
            {
                DivingRequest.LostDiveTime = DateTime.Parse(vo.LostDiveTime, CultureInfo.InvariantCulture);
            }

            DivingRequest.RepetiveDiveDesignation = vo.RepetiveDiveDesignation;
            DivingRequest.SkiBoat = vo.SkiBoat == true ? "Y" : "N";
            DivingRequest.LDV = vo.LDV == true ? "Y" : "N";
            DivingRequest.Trailer = vo.Trailer == true ? "Y" : "N";

            if (vo.OtherLocation > 0)
            {
                DivingRequest.OtherLocation = vo.OtherLocation;
            }

            if (vo.ChangeLocation > 0)
            {
                DivingRequest.ChangeLocation = vo.ChangeLocation;
            }

            DivingRequest.LocationType = vo.LocationType;
            DivingRequest.ClearanceNo = vo.ClearanceNo;
            }
            return DivingRequest;
        }
        #endregion
    }
}
