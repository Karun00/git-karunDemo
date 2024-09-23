using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface ITptDocumentUploadRepository
    { 
        List<TerminalDelaysVO> InsertTerminalDelays(List<TerminalDelaysVO> data, int userId);

        List<OutTurnVolumesVO> InsertOutTurnVolumes(List<OutTurnVolumesVO> data, int _UserId);

        List<TerminalWeeklyDataVO> InsertTerminalData(List<TerminalWeeklyDataVO> data, int _UserId);

        List<RailPlanVO> InsertRailPlan(List<RailPlanVO> data, int _UserId);

        List<TrainMonitoringVO> GetTrainMonitoringDetails(string plannedDate, string Corridor, string movementStatus);
        //List<Port> GetPortDetails();
    }
}
