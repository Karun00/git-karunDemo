using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Data;

namespace IPMS.ServiceProxies.Clients
{
    public class TptDocumentUploadClient : UserClientBase<ITptDocumentUploadService>, ITptDocumentUploadService
    {

        public List<TerminalDelaysVO> TerminalDelaysDataDisplay(string path, string TemplateDoc)
        {
            return WrapOperationWithException(() => Channel.TerminalDelaysDataDisplay(path, TemplateDoc));
        }

        //public DataRowCollection TerminalDelayDataValid(DataColumnCollection a, DataRowCollection b)
        //public DataRowCollection TerminalDelayDataValid(string[] a, DataRowCollection b)
        public string[][] TerminalDelayDataValid(string[] a, string[][] b)
        {
            return WrapOperationWithException(() => Channel.TerminalDelayDataValid(a,b));
        }

        public string[][] OutTurnsDataValid(string[] a, string[][] b)
        {
            return WrapOperationWithException(() => Channel.OutTurnsDataValid(a, b));
        }
        public string[][] TerminalDataValid(string[] a, string[][] b)
        {
            return WrapOperationWithException(() => Channel.TerminalDataValid(a, b));
        }

        public string[][] RailPlanValid(string[] a, string[][] b)
        {
            return WrapOperationWithException(() => Channel.RailPlanValid(a, b));
        }

        public string[][] RailChangeNotificationsValid(string[] a, string[][] b)
        {
            return WrapOperationWithException(() => Channel.RailChangeNotificationsValid(a, b));
        }

        public string[][] ArrivalAndDepartureDataValid(string[] a, string[][] b)
        {
            return WrapOperationWithException(() => Channel.ArrivalAndDepartureDataValid(a, b));
        }

        public List<TerminalWeeklyDataVO> TerminalWeeklyDataDisplay(string path, string TemplateDoc)
        {
            return WrapOperationWithException(() => Channel.TerminalWeeklyDataDisplay(path,TemplateDoc));
        }

        public List<OutTurnVolumesVO> OutTurnVolumesDataDisplay(string path, string TemplateDoc)
        {
            return WrapOperationWithException(() => Channel.OutTurnVolumesDataDisplay(path,TemplateDoc));
        } 


        public string ValidateTerminalDelayDoc(string DelayDoc, string TemplateDoc)
        {
            return WrapOperationWithException(() => Channel.ValidateTerminalDelayDoc(DelayDoc,TemplateDoc));
        }

        public string ValidateOutturnVolumesDoc(string OutTurnVolumesDoc, string TemplateDoc)
        {
            return WrapOperationWithException(() => Channel.ValidateOutturnVolumesDoc(OutTurnVolumesDoc, TemplateDoc));
        }
        public string ValidateTerminalDataDoc(string TerminalDataDoc, string TemplateDoc)
        {
            return WrapOperationWithException(() => Channel.ValidateTerminalDataDoc(TerminalDataDoc, TemplateDoc));
        }
        public List<TerminalDelaysVO> InsertTerminalDelays(List<TerminalDelaysVO> data)
        {
            return WrapOperationWithException(() => Channel.InsertTerminalDelays(data));

        }
        
        public List<OutTurnVolumesVO> InsertOutTurnVolumes(List<OutTurnVolumesVO> data)            
        {
            return WrapOperationWithException(() => Channel.InsertOutTurnVolumes(data));
        } 

        public List<TerminalWeeklyDataVO> InsertTerminalData(List<TerminalWeeklyDataVO> data)
        {
            return WrapOperationWithException(() => Channel.InsertTerminalData(data));
        }

        public List<RailPlanVO> InsertRailPlan(List<RailPlanVO> data)
        {
            return WrapOperationWithException(() => Channel.InsertRailPlan(data));
        }

        public List<RailPlanVO> UpdateRailChangeNotifications(List<RailPlanVO> data)
        {
            return WrapOperationWithException(() => Channel.UpdateRailChangeNotifications(data));
        }

        public List<RailPlanVO> UpdateArrivalAndDepartureTimes(List<RailPlanVO> data)
        {
            return WrapOperationWithException(() => Channel.UpdateArrivalAndDepartureTimes(data));
        }

        
        //public List<TrainMonitoringVO> GetTrainMonitoringDetails(string plannedDate, string Corridor, string movementStatus)
        public List<TrainMonitoringVO> GetTrainMonitoringDetails(string FromDate, string ToDate)
        {
        return WrapOperationWithException(() => Channel.GetTrainMonitoringDetails(FromDate,ToDate));
            //return WrapOperationWithException(() => Channel.GetTrainMonitoringDetails(plannedDate,Corridor,movementStatus));
        }

       // public List<WagonDetailsVO> GetWagonDetailsFromTfrService(int TrainNo, string Origin)
        public List<WagonDetailsVO> GetWagonDetailsFromTfrService(int trainNo, string origin, DateTime originDate)
        {
        return WrapOperationWithException(() => Channel.GetWagonDetailsFromTfrService(trainNo,origin,originDate.Date));
        }


        public List<RailPlanVO> RailPlanDataDisplay(string path, string TemplateDoc)
        {
            return WrapOperationWithException(() => Channel.RailPlanDataDisplay(path, TemplateDoc));
        }

        public List<RailPlanVO> RailChangeNotificationsDataDisplay(string path, string TemplateDoc)
        {
            return WrapOperationWithException(() => Channel.RailChangeNotificationsDataDisplay(path, TemplateDoc));
        }

        public List<RailPlanVO> ArrivalAndDepartureDataDisplay(string path, string TemplateDoc)
        {
            return WrapOperationWithException(() => Channel.ArrivalAndDepartureDataDisplay(path, TemplateDoc));
        }

        //public Task<List<TerminalDelaysVO>> TerminalDelaysDataDisplayAsync(string path, string TemplateDoc)
        //{
        //    return WrapOperationWithException(() => Channel.TerminalDelaysDataDisplayAsync(path, TemplateDoc));
        //}

        //public Task<List<TerminalWeeklyDataVO>> TerminalWeeklyDataDisplayAsync(string path, string TemplateDoc)
        //{
        //    return WrapOperationWithException(() => Channel.TerminalWeeklyDataDisplayAsync(path, TemplateDoc));
        //}

        //public Task<List<OutTurnVolumesVO>> OutTurnVolumesDataDisplayAsync(string path, string TemplateDoc)
        //{
        //    return WrapOperationWithException(() => Channel.OutTurnVolumesDataDisplayAsync(path, TemplateDoc));
        //}


        //public Task<string> ValidateTerminalDelayDocAsync(string DelayDoc, string TemplateDoc)
        //{
        //    return WrapOperationWithException(() => Channel.ValidateTerminalDelayDocAsync(DelayDoc, TemplateDoc));
        //}

        //public Task<string> ValidateOutturnVolumesDocAsync(string OutTurnVolumesDoc, string TemplateDoc)
        //{
        //    return WrapOperationWithException(() => Channel.ValidateOutturnVolumesDocAsync(OutTurnVolumesDoc, TemplateDoc));
        //}
        //public Task<string> ValidateTerminalDataDocAsync(string TerminalDataDoc, string TemplateDoc)
        //{
        //    return WrapOperationWithException(() => Channel.ValidateTerminalDataDocAsync(TerminalDataDoc, TemplateDoc));
        //}
        //public Task<List<TerminalDelaysVO>> InsertTerminalDelaysAsync(List<TerminalDelaysVO> data)
        //{
        //    return WrapOperationWithException(() => Channel.InsertTerminalDelaysAsync(data));

        //}

        //public Task<List<OutTurnVolumesVO>> InsertOutTurnVolumesAsync(List<OutTurnVolumesVO> data)
        //{
        //    return WrapOperationWithException(() => Channel.InsertOutTurnVolumesAsync(data));
        //}

        //public Task<List<TerminalWeeklyDataVO>> InsertTerminalDataAsync(List<TerminalWeeklyDataVO> data)
        //{
        //    return WrapOperationWithException(() => Channel.InsertTerminalDataAsync(data));
        //}

        //public Task<List<RailPlanVO>> InsertRailPlanAsync(List<RailPlanVO> data)
        //{
        //    return WrapOperationWithException(() => Channel.InsertRailPlanAsync(data));
        //}

        //public Task<List<RailPlanVO>> UpdateRailChangeNotificationsAsync(List<RailPlanVO> data)
        //{
        //    return WrapOperationWithException(() => Channel.UpdateRailChangeNotificationsAsync(data));
        //}

        //public Task<List<RailPlanVO>> UpdateArrivalAndDepartureTimesAsync(List<RailPlanVO> data)
        //{
        //    return WrapOperationWithException(() => Channel.UpdateArrivalAndDepartureTimesAsync(data));
        //}

        //public Task<List<TrainMonitoringVO>> GetTrainMonitoringDetailsAsync(string plannedDate, string Corridor, string movementStatus)
        //public List<TrainMonitoringVO> GetTrainMonitoringDetailsAsync(string FromDate, string ToDate)
        //{
        //    return WrapOperationWithException(() => Channel.GetTrainMonitoringDetailsAsync(FromDate, ToDate));
        //    //return WrapOperationWithException(() => Channel.GetTrainMonitoringDetailsAsync(plannedDate, Corridor, movementStatus));
        //}

        //public Task<List<RailPlanVO>> RailPlanDataDisplayAsync(string path, string TemplateDoc)
        //{
        //    return WrapOperationWithException(() => Channel.RailPlanDataDisplayAsync(path, TemplateDoc));
        //}

        //public Task<List<RailPlanVO>> RailChangeNotificationsDataDisplayAsync(string path, string TemplateDoc)
        //{
        //    return WrapOperationWithException(() => Channel.RailChangeNotificationsDataDisplayAsync(path, TemplateDoc));
        //}

        //public Task<List<RailPlanVO>> ArrivalAndDepartureDataDisplayAsync(string path, string TemplateDoc)
        //{
        //    return WrapOperationWithException(() => Channel.ArrivalAndDepartureDataDisplayAsync(path, TemplateDoc));
        //}
    }
}