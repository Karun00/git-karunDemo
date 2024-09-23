using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class ShiftMapExtension
    {
        public static ShiftVO MapToDTO(this Shift data)
        {
            ShiftVO shiftvo = new ShiftVO();
            if (data != null)
            {
                shiftvo.ShiftID = data.ShiftID;
                shiftvo.PortCode = data.PortCode;
                shiftvo.ShiftName = data.ShiftName;
                shiftvo.StartTime = data.StartTime != null ? Convert.ToDateTime(data.StartTime, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture) : null;
                shiftvo.EndTime = data.EndTime != null ? Convert.ToDateTime(data.EndTime, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture) : null;
                shiftvo.IsShiftOff = data.IsShiftOff;
                shiftvo.RecordStatus = data.RecordStatus;
                shiftvo.CreatedBy = data.CreatedBy;
                shiftvo.CreatedDate = data.CreatedDate;
                shiftvo.ModifiedBy = data.ModifiedBy;
                shiftvo.ModifiedDate = data.ModifiedDate;
                shiftvo.RollOverOn = data.RollOverOn;
                
                shiftvo.ShiftFormat = data.IsContinuousShift == "N" ? shiftvo.ShiftName + " (" + shiftvo.StartTime + " - " + shiftvo.EndTime + ")" : data.ShiftName;

                // -- Added by sandeep on 07-01-2015
                shiftvo.FirstShiftID = data.FirstShiftID;
                shiftvo.SecondShiftID = data.SecondShiftID;
                shiftvo.IsContinuousShift = data.IsContinuousShift;
                // -- end
            }

            return shiftvo;
        }
        public static Shift MapToEntity(this ShiftVO shiftvo)
        {
            Shift shift = new Shift();
            if (shiftvo != null)
            {
                shift.ShiftID = shiftvo.ShiftID;
                shift.PortCode = shiftvo.PortCode;
                shift.ShiftName = shiftvo.ShiftName;
                shift.StartTime = !string.IsNullOrEmpty(shiftvo.StartTime) ? DateTime.Parse(Convert.ToDateTime(shiftvo.StartTime, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) : default(DateTime?);
                shift.EndTime = !string.IsNullOrEmpty(shiftvo.EndTime) ? DateTime.Parse(Convert.ToDateTime(shiftvo.EndTime, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) : default(DateTime?);
                shift.IsShiftOff = shiftvo.IsShiftOff;
                shift.RecordStatus = shiftvo.RecordStatus;
                shift.CreatedBy = shiftvo.CreatedBy;
                shift.CreatedDate = shiftvo.CreatedDate;
                shift.ModifiedBy = shiftvo.ModifiedBy;
                shift.ModifiedDate = shiftvo.ModifiedDate;
                shift.RollOverOn = shiftvo.RollOverOn;
                

                // -- Added by sandeep on 07-01-2015
                shift.FirstShiftID = shiftvo.FirstShiftID;
                shift.SecondShiftID = shiftvo.SecondShiftID;
                shift.IsContinuousShift = shiftvo.IsContinuousShift;
                // -- end
            }

            return shift;
        }

        public static List<ShiftVO> MapToDTO(this List<Shift> shiftList)
        {
            List<ShiftVO> shiftvoList = new List<ShiftVO>();
            if (shiftList != null)
                foreach (var data in shiftList)
                {
                    shiftvoList.Add(data.MapToDTO());

                }
            return shiftvoList;
        }
        public static List<Shift> MapToEntity(this List<ShiftVO> shiftListVO)
        {
            List<Shift> ShiftlList = new List<Shift>();
            if (shiftListVO != null)
                foreach (var data in shiftListVO)
                {
                    ShiftlList.Add(data.MapToEntity());

                }
            return ShiftlList;
        }
    }
}
