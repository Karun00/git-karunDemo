using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class OtherServiceRecordingMapExtension
    {
        public static OtherServiceRecordingVO MapToDTO(this OtherServiceRecording data)
        {
            OtherServiceRecordingVO OSRecordingvo = new OtherServiceRecordingVO();
            if (data != null)
            {
                OSRecordingvo.OtherServiceRecordingID = data.OtherServiceRecordingID;
                OSRecordingvo.ResourceAllocationID = data.ResourceAllocationID;
                OSRecordingvo.StartTime = data.StartTime;
                OSRecordingvo.EndTime = data.EndTime;
                OSRecordingvo.LineUp = data.LineUp;
                OSRecordingvo.LineDown = data.LineDown;
                OSRecordingvo.PilotOn = data.PilotOn;
                OSRecordingvo.MeterSerialNo = data.MeterSerialNo;
                OSRecordingvo.OpeningMeterReading = data.OpeningMeterReading;
                OSRecordingvo.ClosingMeterReading = data.ClosingMeterReading;
                OSRecordingvo.TotalDispensed = data.TotalDispensed;
                OSRecordingvo.FirstSwing = data.FirstSwing;
                OSRecordingvo.LastSwing = data.LastSwing;
                OSRecordingvo.TimeAlongSide = data.TimeAlongSide;
                OSRecordingvo.RecordStatus = data.RecordStatus;
                OSRecordingvo.CreatedBy = data.CreatedBy;
                OSRecordingvo.CreatedDate = data.CreatedDate;
                OSRecordingvo.ModifiedBy = data.ModifiedBy;
                OSRecordingvo.ModifiedDate = data.ModifiedDate;
                OSRecordingvo.Remarks = data.Remarks;
                OSRecordingvo.Deficiencies = data.Deficiencies;
                OSRecordingvo.WaitingStartTime = data.WaitingStartTime;
                OSRecordingvo.WaitingEndTime = data.WaitingEndTime;
                OSRecordingvo.DelayReason = data.DelayReason != null ? data.DelayReason : "";
                OSRecordingvo.DelayOtherReason = data.DelayOtherReason != null ? data.DelayOtherReason : "";
                OSRecordingvo.BackToQuay = data.BackToQuay;
                OSRecordingvo.Extend = data.Extend == "Y" ? true : false;
                OSRecordingvo.BerthKey = data.BerthCode != null
                    ? data.PortCode + "." + data.QuayCode + "." + data.BerthCode
                    : null;
                OSRecordingvo.IsCompleted = data.IsCompleted == "Y" ? true : false;
                OSRecordingvo.MeterNo = data.MeterNo !=null?data.MeterNo:"";
               
            }
            return OSRecordingvo;
        }

        public static OtherServiceRecording MapToEntity(this OtherServiceRecordingVO VO)
        {
            OtherServiceRecording OSRecording = new OtherServiceRecording();
            if (VO != null)
            {
                OSRecording.OtherServiceRecordingID = VO.OtherServiceRecordingID;
                OSRecording.ResourceAllocationID = VO.ResourceAllocationID;
                OSRecording.StartTime = VO.StartTime;
                OSRecording.EndTime = VO.EndTime;
                OSRecording.LineUp = VO.LineUp;
                OSRecording.LineDown = VO.LineDown;
                if (VO.PilotOn != null)
                {
                    OSRecording.PilotOn = Convert.ToDateTime(VO.PilotOn, CultureInfo.InvariantCulture);
                }
                OSRecording.OpeningMeterReading = VO.OpeningMeterReading;
                OSRecording.ClosingMeterReading = VO.ClosingMeterReading;
                OSRecording.MeterSerialNo = VO.MeterSerialNo;
                OSRecording.TotalDispensed = VO.TotalDispensed;
                OSRecording.FirstSwing = VO.FirstSwing;
                OSRecording.LastSwing = VO.LastSwing;
                OSRecording.TimeAlongSide = VO.TimeAlongSide;
                OSRecording.RecordStatus = VO.RecordStatus;
                OSRecording.CreatedBy = VO.CreatedBy;
                OSRecording.CreatedDate = VO.CreatedDate;
                OSRecording.ModifiedBy = VO.ModifiedBy;
                OSRecording.ModifiedDate = VO.ModifiedDate;
                OSRecording.Remarks = VO.Remarks;
                OSRecording.Deficiencies = VO.Deficiencies;
                OSRecording.WaitingStartTime = VO.WaitingStartTime;
                OSRecording.WaitingEndTime = VO.WaitingEndTime;
                OSRecording.DelayReason = VO.DelayReason != null ? VO.DelayReason : "";
                OSRecording.DelayOtherReason = VO.DelayOtherReason != null ? VO.DelayOtherReason : "";
                OSRecording.BackToQuay = VO.BackToQuay;
                OSRecording.Extend = VO.Extend == true ? "Y" : "N";
                OSRecording.IsCompleted = VO.IsCompleted == true ? "Y" : "N";
                OSRecording.MeterNo = VO.MeterNo !=null?VO.MeterNo:"";
                if (VO.BerthKey != "" && VO.BerthKey != null)
                {
                    string[] key = VO.BerthKey.Split('.');

                    OSRecording.PortCode = key[0];
                    OSRecording.QuayCode = key[1];
                    OSRecording.BerthCode = key[2];
                }
            }
            return OSRecording;
        }

        public static List<OtherServiceRecording> MapToEntity(this List<OtherServiceRecordingVO> OSRvos)
        {
            List<OtherServiceRecording> OSRecordingEntities = new List<OtherServiceRecording>();
            if (OSRvos != null)
            {
                foreach (var OSRvo in OSRvos)
                {
                    OSRecordingEntities.Add(OSRvo.MapToEntity());
                }
            }
            return OSRecordingEntities;
        }

        public static List<OtherServiceRecordingVO> MapToDTO(this IEnumerable<OtherServiceRecording> OSRentities)
        {
            List<OtherServiceRecordingVO> OSRvos = new List<OtherServiceRecordingVO>();
            if (OSRentities != null)
            {
                foreach (var OSRentitie in OSRentities)
                {
                    OSRvos.Add(OSRentitie.MapToDTO());
                }
            }
            return OSRvos;
        }

        public static OtherServiceRecordingVO MapToDTOObj(this IEnumerable<OtherServiceRecording> entities)
        {
            OtherServiceRecordingVO OSRecordingvo = new OtherServiceRecordingVO();
            if (entities != null)
            {
                foreach (var data in entities)
                {
                    OSRecordingvo.OtherServiceRecordingID = data.OtherServiceRecordingID;
                    OSRecordingvo.ResourceAllocationID = data.ResourceAllocationID;
                    OSRecordingvo.StartTime = data.StartTime;
                    OSRecordingvo.EndTime = data.EndTime;
                    OSRecordingvo.LineUp = data.LineUp;
                    OSRecordingvo.LineDown = data.LineDown;
                    OSRecordingvo.PilotOn = data.PilotOn;
                    OSRecordingvo.MeterSerialNo = data.MeterSerialNo;
                    OSRecordingvo.OpeningMeterReading = data.OpeningMeterReading;
                    OSRecordingvo.ClosingMeterReading = data.ClosingMeterReading;
                    OSRecordingvo.TotalDispensed = data.TotalDispensed;
                    OSRecordingvo.FirstSwing = data.FirstSwing;
                    OSRecordingvo.LastSwing = data.LastSwing;
                    OSRecordingvo.TimeAlongSide = data.TimeAlongSide;
                    OSRecordingvo.RecordStatus = data.RecordStatus;
                    OSRecordingvo.CreatedBy = data.CreatedBy;
                    OSRecordingvo.CreatedDate = data.CreatedDate;
                    OSRecordingvo.ModifiedBy = data.ModifiedBy;
                    OSRecordingvo.ModifiedDate = data.ModifiedDate;
                    OSRecordingvo.Remarks = data.Remarks;
                    OSRecordingvo.Deficiencies = data.Deficiencies;
                    OSRecordingvo.WaitingStartTime = data.WaitingStartTime;
                    OSRecordingvo.WaitingEndTime = data.WaitingEndTime;
                    OSRecordingvo.DelayReason = data.DelayReason != null ? data.DelayReason : "";
                    OSRecordingvo.DelayOtherReason = data.DelayOtherReason != null ? data.DelayOtherReason : "";
                    OSRecordingvo.BackToQuay = data.BackToQuay;
                    OSRecordingvo.IsCompleted = data.IsCompleted == "Y" ? true : false ;
                    OSRecordingvo.Extend = data.Extend == "Y" ? true : false;
                    OSRecordingvo.MeterNo = data.MeterNo!=null?data.MeterNo:"";
                    OSRecordingvo.BerthKey = data.BerthCode != null
                        ? data.PortCode + "." + data.QuayCode + "." + data.BerthCode
                        : null;
                }
            }
            return OSRecordingvo;
        }

    }
}
