using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.IO;
using System.Data;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ITptDocumentUploadService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<TerminalDelaysVO> TerminalDelaysDataDisplay(string path, string TemplateDoc);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string[][] TerminalDelayDataValid(string[] a, string[][] b);
        //DataRowCollection TerminalDelayDataValid(string[] a, DataRowCollection b);
       // DataRowCollection TerminalDelayDataValid(DataColumnCollection a, DataRowCollection b);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string[][] OutTurnsDataValid(string[] a, string[][] b);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string[][] TerminalDataValid(string[] a, string[][] b);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string[][] RailPlanValid(string[] a, string[][] b);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string[][] RailChangeNotificationsValid(string[] a, string[][] b);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string[][] ArrivalAndDepartureDataValid(string[] a, string[][] b);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<TerminalWeeklyDataVO> TerminalWeeklyDataDisplay(string path, string TemplateDoc);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<OutTurnVolumesVO> OutTurnVolumesDataDisplay(string path,string TemplateDoc);  
        
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<TerminalDelaysVO> InsertTerminalDelays(List<TerminalDelaysVO> data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<OutTurnVolumesVO> InsertOutTurnVolumes(List<OutTurnVolumesVO> data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<TerminalWeeklyDataVO> InsertTerminalData(List<TerminalWeeklyDataVO> data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RailPlanVO> InsertRailPlan(List<RailPlanVO> data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RailPlanVO> UpdateRailChangeNotifications(List<RailPlanVO> data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RailPlanVO> UpdateArrivalAndDepartureTimes(List<RailPlanVO> data);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<TrainMonitoringVO> GetTrainMonitoringDetails(string FromDate, string ToDate);
        //List<TrainMonitoringVO> GetTrainMonitoringDetails(string plannedDate, string Corridor, string movementStatus);
        

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RailPlanVO> RailPlanDataDisplay(string path, string TemplateDoc);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RailPlanVO> RailChangeNotificationsDataDisplay(string path, string TemplateDoc);
        
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RailPlanVO> ArrivalAndDepartureDataDisplay(string path, string TemplateDoc);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<WagonDetailsVO> GetWagonDetailsFromTfrService(int trainNo, string origin, DateTime originDate);
        //List<WagonDetailsVO> GetWagonDetailsFromTfrService(int TrainNo, string Origin);

    }
}
