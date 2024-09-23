using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface ITptDocumentUploadService : IDisposable
    {
        [OperationContract]
        List<TerminalDelaysVO> TerminalDelaysDataDisplay(string path, string TemplateDoc);

        [OperationContract]
        string[][] TerminalDelayDataValid(string[] a, string[][] b);
        //DataRowCollection TerminalDelayDataValid(string[] a, DataRowCollection b);
       // DataRowCollection TerminalDelayDataValid(DataColumnCollection a, DataRowCollection b);

        [OperationContract]
        string[][] OutTurnsDataValid(string[] a, string[][] b);

        [OperationContract]
        string[][] TerminalDataValid(string[] a, string[][] b);

        [OperationContract]
        string[][] RailPlanValid(string[] a, string[][] b);

        [OperationContract]
        string[][] RailChangeNotificationsValid(string[] a, string[][] b);

        [OperationContract]
        string[][] ArrivalAndDepartureDataValid(string[] a, string[][] b);



        [OperationContract]
        List<OutTurnVolumesVO> OutTurnVolumesDataDisplay(string path, string TemplateDoc);

        [OperationContract]
        List<TerminalWeeklyDataVO> TerminalWeeklyDataDisplay(string path, string TemplateDoc);

        [OperationContract]
        string ValidateTerminalDelayDoc(string UploadDoc,string TemplateDoc);

        [OperationContract]
        string ValidateOutturnVolumesDoc(string UploadDoc, string TemplateDoc);

        [OperationContract]
        string ValidateTerminalDataDoc(string UploadDoc, string TemplateDoc);
        
        [OperationContract]
        List<TerminalDelaysVO> InsertTerminalDelays(List<TerminalDelaysVO> data);

        [OperationContract]
        List<OutTurnVolumesVO> InsertOutTurnVolumes(List<OutTurnVolumesVO> data);

        [OperationContract]
        List<TerminalWeeklyDataVO> InsertTerminalData(List<TerminalWeeklyDataVO> data);

        [OperationContract]
        List<RailPlanVO> InsertRailPlan(List<RailPlanVO> data);

        [OperationContract] 
        List<RailPlanVO> UpdateRailChangeNotifications(List<RailPlanVO> data);

        [OperationContract] 
        List<RailPlanVO> UpdateArrivalAndDepartureTimes(List<RailPlanVO> data);

        [OperationContract]
        List<TrainMonitoringVO> GetTrainMonitoringDetails(string FromDate, string ToDate);
        //List<TrainMonitoringVO> GetTrainMonitoringDetails(string plannedDate, string Corridor, string movementStatus);

        [OperationContract]
        List<WagonDetailsVO> GetWagonDetailsFromTfrService(int trainNo, string origin, DateTime originDate);
        //List<WagonDetailsVO> GetWagonDetailsFromTfrService(int TrainNo, string Origin);

        [OperationContract]
        List<RailPlanVO> RailPlanDataDisplay(string path, string TemplateDoc);
         
        [OperationContract]
        List<RailPlanVO> RailChangeNotificationsDataDisplay(string path, string TemplateDoc);
         
        [OperationContract]
        List<RailPlanVO> ArrivalAndDepartureDataDisplay(string path, string TemplateDoc);

         
        //[OperationContract]
        //Task<List<TerminalDelaysVO>> TerminalDelaysDataDisplayAsync(string path, string TemplateDoc);

        //[OperationContract]
        //Task<List<OutTurnVolumesVO>> OutTurnVolumesDataDisplayAsync(string path, string TemplateDoc);

        //[OperationContract]
        //Task<List<TerminalWeeklyDataVO>> TerminalWeeklyDataDisplayAsync(string path, string TemplateDoc);

        //[OperationContract]
        // Task<string> ValidateTerminalDelayDocAsync(string UploadDoc, string TemplateDoc);

        //[OperationContract]
        // Task<string> ValidateOutturnVolumesDocAsync(string UploadDoc, string TemplateDoc);

        //[OperationContract]
        // Task<string> ValidateTerminalDataDocAsync(string UploadDoc, string TemplateDoc);

        //[OperationContract]
        // Task<List<TerminalDelaysVO>> InsertTerminalDelaysAsync(List<TerminalDelaysVO> data);

        //[OperationContract]
        // Task<List<OutTurnVolumesVO>> InsertOutTurnVolumesAsync(List<OutTurnVolumesVO> data);

        //[OperationContract]
        // Task<List<TerminalWeeklyDataVO>> InsertTerminalDataAsync(List<TerminalWeeklyDataVO> data);

        //[OperationContract]
        // Task<List<RailPlanVO>> InsertRailPlanAsync(List<RailPlanVO> data);

        //[OperationContract]
        // Task<List<RailPlanVO>> UpdateRailChangeNotificationsAsync(List<RailPlanVO> data);

        //[OperationContract]
        // Task<List<RailPlanVO>> UpdateArrivalAndDepartureTimesAsync(List<RailPlanVO> data);

        //[OperationContract] 
        //Task<List<TrainMonitoringVO>> GetTrainMonitoringDetailsAsync(string FromDate, string ToDate);
        // //Task<List<TrainMonitoringVO>> GetTrainMonitoringDetailsAsync(string plannedDate, string Corridor, string movementStatus);

        //[OperationContract]
        // Task<List<RailPlanVO>> RailPlanDataDisplayAsync(string path, string TemplateDoc);

        //[OperationContract]
        // Task<List<RailPlanVO>> RailChangeNotificationsDataDisplayAsync(string path, string TemplateDoc);

        //[OperationContract]
        // Task<List<RailPlanVO>> ArrivalAndDepartureDataDisplayAsync(string path, string TemplateDoc);
    }
}
