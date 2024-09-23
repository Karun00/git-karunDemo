using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class PilotageServiceRecordingMapExention
    {
        public static PilotageServiceRecordingVO MapToDTO(this PilotageServiceRecording data)
        {
            PilotageServiceRecordingVO PSRecordingvo = new PilotageServiceRecordingVO();
            if (data != null)
            {
                PSRecordingvo.PilotageServiceRecordingID = data.PilotageServiceRecordingID;
                PSRecordingvo.ResourceAllocationID = data.ResourceAllocationID;
                PSRecordingvo.StartTime = data.StartTime;
                //PSRecordingvo.ActualScheduledTime = data.ActualScheduledTime;
                PSRecordingvo.EndTime = data.EndTime;
                PSRecordingvo.PilotOnBoard = data.PilotOnBoard;
                PSRecordingvo.PilotOff = data.PilotOff;
                PSRecordingvo.WaitingStartTime = data.WaitingStartTime;
                PSRecordingvo.WaitingEndTime = data.WaitingEndTime;
                PSRecordingvo.AdditionalTugs = data.AdditionalTugs;
                PSRecordingvo.OffSteam = data.OffSteam == "Y" ? true : false;
                PSRecordingvo.MarineRevenueCleared = data.MarineRevenueCleared == "Y" ? true : false;
                PSRecordingvo.Remarks = data.Remarks;
                PSRecordingvo.Deficiencies = data.Deficiencies;
                PSRecordingvo.RecordStatus = data.RecordStatus;
                PSRecordingvo.CreatedBy = data.CreatedBy;
                PSRecordingvo.CreatedDate = data.CreatedDate;
                PSRecordingvo.ModifiedBy = data.ModifiedBy;
                PSRecordingvo.ModifiedDate = data.ModifiedDate;
                PSRecordingvo.DelayReason = data.DelayReason != null ? data.DelayReason : "";
                PSRecordingvo.DelayOtherReason = data.DelayOtherReason != null ? data.DelayOtherReason : "";
                PSRecordingvo.MOPSDelay = data.MOPSDelay;
            }
            return PSRecordingvo;
        }

        public static PilotageServiceRecording MapToEntity(this PilotageServiceRecordingVO vo)
        {
            PilotageServiceRecording PSRecording = new PilotageServiceRecording();
            if (vo != null)
            {
                PSRecording.PilotageServiceRecordingID = vo.PilotageServiceRecordingID;
                PSRecording.ResourceAllocationID = vo.ResourceAllocationID;
                PSRecording.StartTime = vo.StartTime;
                PSRecording.EndTime = vo.EndTime;
                PSRecording.ActualScheduledTime = vo.ActualScheduledTime;
                PSRecording.PilotOnBoard = vo.PilotOnBoard;
                PSRecording.PilotOff = vo.PilotOff;
                PSRecording.WaitingStartTime = vo.WaitingStartTime;
                PSRecording.WaitingEndTime = vo.WaitingEndTime;
                PSRecording.AdditionalTugs = vo.AdditionalTugs;
                PSRecording.MOPSDelay = vo.MOPSDelay;

                if (vo.OffSteam == true)
                {
                    PSRecording.OffSteam = "Y";
                }
                else
                {
                    PSRecording.OffSteam = "N";
                }

                if (vo.MarineRevenueCleared == true)
                {
                    PSRecording.MarineRevenueCleared = "Y";
                }
                else
                {
                    PSRecording.MarineRevenueCleared = "N";
                }
                PSRecording.Remarks = vo.Remarks;
                PSRecording.DelayReason = vo.DelayReason != null ? vo.DelayReason : "";
                PSRecording.DelayOtherReason = vo.DelayOtherReason != null ? vo.DelayOtherReason : "";
                PSRecording.Deficiencies = vo.Deficiencies;
                PSRecording.RecordStatus = vo.RecordStatus;
                PSRecording.CreatedBy = vo.CreatedBy;
                PSRecording.CreatedDate = vo.CreatedDate;
                PSRecording.ModifiedBy = vo.ModifiedBy;
                PSRecording.ModifiedDate = vo.ModifiedDate;
            }
            return PSRecording;
        }

        public static List<PilotageServiceRecording> MapToEntity(this List<PilotageServiceRecordingVO> vos)
        {
            List<PilotageServiceRecording> PSRecordingEntities = new List<PilotageServiceRecording>();
            if (vos != null)
            {
                foreach (var psrvo in vos)
                {
                    PSRecordingEntities.Add(psrvo.MapToEntity());
                }
            }
            return PSRecordingEntities;
        }
        public static List<PilotageServiceRecordingVO> MapTDTO(this IEnumerable<PilotageServiceRecording> entities)
        {
            List<PilotageServiceRecordingVO> PSRecordingvos = new List<PilotageServiceRecordingVO>();
            if (entities != null)
            {
                foreach (var PSRentity in entities)
                {
                    PSRecordingvos.Add(PSRentity.MapToDTO());
                }
            }
            return PSRecordingvos;
        }


        public static PilotageServiceRecordingVO MapToDTOObj(this IEnumerable<PilotageServiceRecording> entities)
        {
            PilotageServiceRecordingVO PSRecordingvo = new PilotageServiceRecordingVO();
            if (entities != null)
            {
                foreach (var data in entities)
                {
                    PSRecordingvo.PilotageServiceRecordingID = data.PilotageServiceRecordingID;
                    PSRecordingvo.ResourceAllocationID = data.ResourceAllocationID;
                    PSRecordingvo.StartTime = data.StartTime;
                    PSRecordingvo.ActualScheduledTime = data.ActualScheduledTime;
                    PSRecordingvo.EndTime = data.EndTime;
                    PSRecordingvo.PilotOnBoard = data.PilotOnBoard;
                    PSRecordingvo.PilotOff = data.PilotOff;
                    PSRecordingvo.WaitingStartTime = data.WaitingStartTime;
                    PSRecordingvo.WaitingEndTime = data.WaitingEndTime;
                    PSRecordingvo.AdditionalTugs = data.AdditionalTugs;
                    PSRecordingvo.DelayReason = data.DelayReason != null ? data.DelayReason : "";
                    PSRecordingvo.DelayOtherReason = data.DelayOtherReason != null ? data.DelayOtherReason : "";
                    PSRecordingvo.OffSteam = data.OffSteam == "Y" ? true : false;
                    PSRecordingvo.MarineRevenueCleared = data.MarineRevenueCleared == "Y" ? true : false;
                    PSRecordingvo.Remarks = data.Remarks;
                    PSRecordingvo.Deficiencies = data.Deficiencies;
                    PSRecordingvo.RecordStatus = data.RecordStatus;
                    PSRecordingvo.CreatedBy = data.CreatedBy;
                    PSRecordingvo.CreatedDate = data.CreatedDate;
                    PSRecordingvo.ModifiedBy = data.ModifiedBy;
                    PSRecordingvo.ModifiedDate = data.ModifiedDate;
                    PSRecordingvo.MOPSDelay = data.MOPSDelay;
                }
            }
            return PSRecordingvo;
        }



    }
}
