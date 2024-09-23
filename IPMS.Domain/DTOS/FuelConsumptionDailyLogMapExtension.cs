using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class FuelConsumptionDailyLogMapExtension
    {

        public static FuelConsumptionDailyLogVO MapToDto(this FuelConsumptionDailyLog data)
        {
            FuelConsumptionDailyLogVO fuelconsumptiondailylogvo = new FuelConsumptionDailyLogVO();
            if (data != null)
            {
                fuelconsumptiondailylogvo.FuelConsumptionDailyLogID = data.FuelConsumptionDailyLogID;
                fuelconsumptiondailylogvo.PortCode = data.PortCode;
                fuelconsumptiondailylogvo.CraftID = data.CraftID;
                fuelconsumptiondailylogvo.PreviousROB = Convert.ToString(data.PreviousROB,CultureInfo.InvariantCulture);
                fuelconsumptiondailylogvo.PresentROB = Convert.ToString(data.PresentROB, CultureInfo.InvariantCulture);
                fuelconsumptiondailylogvo.StartDateTime = Convert.ToDateTime(data.StartDateTime).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                fuelconsumptiondailylogvo.EndDateTime = Convert.ToDateTime(data.EndDateTime).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                fuelconsumptiondailylogvo.RunningHours = Convert.ToString(data.RunningHours, CultureInfo.InvariantCulture);
                fuelconsumptiondailylogvo.AvgFuelConsumed = Convert.ToString(data.AvgFuelConsumed, CultureInfo.InvariantCulture);
                fuelconsumptiondailylogvo.Remarks = data.Remarks;
                fuelconsumptiondailylogvo.RecordStatus = data.RecordStatus;
                fuelconsumptiondailylogvo.CreatedBy = data.CreatedBy;
                fuelconsumptiondailylogvo.CreatedDate = data.CreatedDate;
                fuelconsumptiondailylogvo.ModifiedBy = data.ModifiedBy;
                fuelconsumptiondailylogvo.ModifiedDate = data.ModifiedDate;
                fuelconsumptiondailylogvo.FuelReceived = Convert.ToString(data.FuelReceived, CultureInfo.InvariantCulture);
                fuelconsumptiondailylogvo.StartRunningHrs = Convert.ToString(data.StartRunningHrs, CultureInfo.InvariantCulture);
                fuelconsumptiondailylogvo.EndRunningHrs = Convert.ToString(data.EndRunningHrs, CultureInfo.InvariantCulture);
                if (data.Craft != null)
                {
                    fuelconsumptiondailylogvo.Crafts = data.Craft.MapToDto();
                }
            }
            return fuelconsumptiondailylogvo;
        }
        public static FuelConsumptionDailyLog MapToEntity(this FuelConsumptionDailyLogVO vo)
        {
            FuelConsumptionDailyLog fuelconsumptiondailylog = new FuelConsumptionDailyLog();
            if (vo != null)
            {
                fuelconsumptiondailylog.FuelConsumptionDailyLogID = vo.FuelConsumptionDailyLogID;
                fuelconsumptiondailylog.PortCode = vo.PortCode;
                fuelconsumptiondailylog.CraftID = vo.CraftID;
                fuelconsumptiondailylog.PreviousROB = Convert.ToDecimal(vo.PreviousROB, CultureInfo.InvariantCulture);
                fuelconsumptiondailylog.PresentROB = Convert.ToDecimal(vo.PresentROB, CultureInfo.InvariantCulture);
                fuelconsumptiondailylog.StartDateTime = DateTime.Parse(Convert.ToDateTime(vo.StartDateTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                fuelconsumptiondailylog.EndDateTime = DateTime.Parse(Convert.ToDateTime(vo.EndDateTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                fuelconsumptiondailylog.RunningHours = Convert.ToDecimal(vo.RunningHours, CultureInfo.InvariantCulture);
                fuelconsumptiondailylog.AvgFuelConsumed = Convert.ToDecimal(vo.AvgFuelConsumed, CultureInfo.InvariantCulture);
                fuelconsumptiondailylog.Remarks = vo.Remarks;
                fuelconsumptiondailylog.RecordStatus = vo.RecordStatus;
                fuelconsumptiondailylog.CreatedBy = vo.CreatedBy;
                fuelconsumptiondailylog.CreatedDate = vo.CreatedDate;
                fuelconsumptiondailylog.ModifiedBy = vo.ModifiedBy;
                fuelconsumptiondailylog.ModifiedDate = vo.ModifiedDate;
                fuelconsumptiondailylog.FuelReceived = Convert.ToDecimal(vo.FuelReceived, CultureInfo.InvariantCulture);
                fuelconsumptiondailylog.StartRunningHrs = Convert.ToDecimal(vo.StartRunningHrs, CultureInfo.InvariantCulture);
                fuelconsumptiondailylog.EndRunningHrs = Convert.ToDecimal(vo.EndRunningHrs, CultureInfo.InvariantCulture);
                if (vo.Crafts != null)
                {
                    fuelconsumptiondailylog.Craft = vo.Crafts.MapToEntity();
                }
            }
            return fuelconsumptiondailylog;
        }

        public static List<CraftVO> CraftMapToDto(this List<Craft> data)
        {
            List<CraftVO> craftVo = new List<CraftVO>();
            if (data != null)
            {
                foreach (var cratf in data)
                {
                    craftVo.Add(cratf.MapToDto());
                }
            }

            return craftVo;
        }
        public static List<FuelConsumptionDailyLogVO> MapToDTO(this List<FuelConsumptionDailyLog> fuelconsumptiondailyloglist)
        {
            List<FuelConsumptionDailyLogVO> fuelconsumptiondailylogvoList = new List<FuelConsumptionDailyLogVO>();
            if (fuelconsumptiondailyloglist != null)
                foreach (var fuelcons in fuelconsumptiondailyloglist)
                {
                    fuelconsumptiondailylogvoList.Add(fuelcons.MapToDto());

                }
            return fuelconsumptiondailylogvoList;
        }

    }
}
