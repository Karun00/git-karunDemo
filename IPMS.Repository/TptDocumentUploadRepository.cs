using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
 
namespace IPMS.Repository
{
    public class TptDocumentUploadRepository : ITptDocumentUploadRepository
    {
        private IUnitOfWork _unitOfWork;
        private IUnitOfWork _unitOfWork1;
      //  private readonly ILog log;
        
        public TptDocumentUploadRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork1 = unitOfWork;
            XmlConfigurator.Configure();
         //   log = LogManager.GetLogger(typeof(TptDocumentUploadRepository));
             
        } 
         
        #region  InsertTerminalDelays
        /// <summary>
        /// To Add Terminal Delay Values Data
        /// </summary>
        /// <param name="data">Terminal Delays Data to be Inserted</param>
        /// <param name="userId">User who is saving the data</param>
        /// <returns></returns>
        public List<TerminalDelaysVO> InsertTerminalDelays(List<TerminalDelaysVO> data, int userId)
        {
            if (data != null)
            {
                List<TerminalDelay> terminalDelayValues = data.MapToEntity();

                foreach (var item in data)
                {
                    //var TerminalDelayId = new SqlParameter("@TerminalDelayId", 1);//item.TerminalDelayID
                    var parPortCode = new SqlParameter("@PortCode", item.PortCode);
                    var parIMONo = new SqlParameter("@IMONo", item.IMONo);
                    var parArrivalDate = new SqlParameter("@ArrivalDate", Convert.ToDateTime(item.ArrivalDate));
                    var parTerminal = new SqlParameter("@Terminal", item.Terminal);
                    var parCargoType = new SqlParameter("@CargoType", item.CargoType);
                    var parReasonForDelay = new SqlParameter("@ReasonForDelay", item.ReasonForDelay);
                    var parDelayDuration = new SqlParameter("@DelayDuration", item.DelayDuration);
                    var parUOM = new SqlParameter("@UOM", item.UnitOfMeasure);
                    var parComments = new SqlParameter("@Comments", item.Comments);
                    var parCreatedBy = new SqlParameter("@CreatedBy", userId);


                    _unitOfWork.ExecuteSqlCommand(
                        "dbo.usp_InsertTerminalDelay @PortCode,@IMONo,@ArrivalDate,@Terminal,@CargoType,@ReasonForDelay,@DelayDuration,@UOM,@Comments,@CreatedBy",
                        parPortCode, parIMONo, parArrivalDate, parTerminal, parCargoType, parReasonForDelay,
                        parDelayDuration, parUOM, parComments, parCreatedBy);

                }
            }
            return data;
        } 
        #endregion 

         #region  Insert OutTurn Volumes
        /// <summary>
        /// To Add Out Turn Volumes Data
        /// </summary>
        /// <param name="Data">Out Turn Volumes data to be inserted</param>
        /// <param name="_UserId"> User who is saving the data</param>
        /// <returns></returns>
        public List<OutTurnVolumesVO> InsertOutTurnVolumes(List<OutTurnVolumesVO> data, int _UserId)
        {
            if (data != null)
            {
                //try
                //{
                List<OutTurnVolume> OutTurnVolumeValues = data.MapToEntity();

                foreach (var item in data)
                {
                    //var TerminalDelayId = new SqlParameter("@TerminalDelayId", 1);//item.TerminalDelayID
                    var parPortCode = new SqlParameter("@PortCode", item.PortCode);
                    var parIMONo = new SqlParameter("@IMONo", item.IMONo);
                    var parArrivalDate = new SqlParameter("@ArrivalDate", item.ArrivalDate);
                        //Convert.ToDateTime(item.ArrivalDate)"01-09-2015"
                    var parTerminal = new SqlParameter("@Terminal", item.Terminal);
                    var parCargoType = new SqlParameter("@CargoType", item.CargoType);
                    var parOutTurnVolume = new SqlParameter("@OutTurnVolume", item.OutTurnVolume);
                    var parFirstCraneSwing = new SqlParameter("@FirstCraneSwing", "01-09-2015"); //item.FirstCraneSwing
                    var parLastCraneSwing = new SqlParameter("@LastCraneSwing", "01-12-2015"); //item.LastCraneSwing
                    var parNoOfCranes = new SqlParameter("@NoC", item.NoOfCranes);
                    var parUOM = new SqlParameter("@UOM", item.UnitOfMeasure);
                    var parComments = new SqlParameter("@Comments", item.Comments);
                    var parCreatedBy = new SqlParameter("@CreatedBy", _UserId);

                    // log.Error("Exception = ", ex);
                    _unitOfWork.ExecuteSqlCommand(
                        "dbo.usp_InsertOutTurnVolume @PortCode,@IMONo,@ArrivalDate,@Terminal,@CargoType,@OutTurnVolume,@FirstCraneSwing,@LastCraneSwing,@NoC,@UOM,@Comments,@CreatedBy",
                        parPortCode, parIMONo, parArrivalDate, parTerminal, parCargoType, parOutTurnVolume,
                        parFirstCraneSwing, parLastCraneSwing, parNoOfCranes, parUOM, parComments, parCreatedBy);

                }
                //}
                //catch (Exception ex)
                //{
                //    log.Error("Exception = ", ex);
                //}
            }
            return data;
        } 
         
        #endregion 

        #region  Insert Terminal Data
        /// <summary>
        /// To Add Out Turn Volumes Data
        /// </summary>
        /// <param name="Data">Terminal data to be inserted</param>
        /// <param name="_UserId"> User who is saving the data</param>
        /// <returns></returns>
        public List<TerminalWeeklyDataVO> InsertTerminalData(List<TerminalWeeklyDataVO> data, int _UserId)
        {
            if (data != null)
            {
                List<TerminalData> terminalDataValues = data.MapToEntity();

                foreach (var item in data)
                {
                    var parPortCode = new SqlParameter("@PortCode", item.PortCode);
                    var parTerminal = new SqlParameter("@Terminal", item.Terminal);
                    var parCargoType = new SqlParameter("@CargoType", item.CargoType);
                    var parWeekNo = new SqlParameter("@WeekNo", item.WeekNo);
                    var parWeekEnding = new SqlParameter("@WeekEnding", item.WeekEnding);
                    var parPerformanceArea = new SqlParameter("@PerformanceArea", item.PerformanceArea);
                    var parMeasure = new SqlParameter("@Measure", item.Measure);
                    var parPlanned = new SqlParameter("@Planned", item.Planned);
                    var parActual = new SqlParameter("@Actual", item.Actual);
                    var parUOM = new SqlParameter("@UOM", item.UnitOfMeasure);
                    var parComments = new SqlParameter("@Comments", item.Comments);
                    var parCreatedBy = new SqlParameter("@CreatedBy", _UserId);

                    _unitOfWork.ExecuteSqlCommand(
                        "dbo.usp_InsertTerminalData @PortCode,@Terminal,@CargoType,@WeekNo,@WeekEnding,@PerformanceArea,@Measure,@Planned,@Actual,@UOM,@Comments,@CreatedBy",
                        parPortCode, parTerminal, parCargoType, parWeekNo, parWeekEnding, parPerformanceArea, parMeasure,
                        parPlanned, parActual, parUOM, parComments, parCreatedBy);

                }
            }
            return data;
        }

        #endregion 

        #region  Insert Rail Plan
        /// <summary>
        /// To Add Rail Plan Data
        /// </summary>
        /// <param name="Data">Rail Plan to be inserted</param>
        /// <param name="_UserId"> User who is saving the data</param>
        /// <returns></returns>
        public List<RailPlanVO> InsertRailPlan(List<RailPlanVO> data, int _UserId)
        {
            //List<RailPlan> RailPlanValues = data.MapToEntity();
            if (data != null)
            {
                foreach (var item in data)
                {
                    var parPortCode = new SqlParameter("@PortCode", item.PortCode);
                    var parCorridor = new SqlParameter("@Corridor", item.Corridor);
                    var parPlannedDate = new SqlParameter("@PlannedDate", item.PlannedDate);
                    var parSchedule = new SqlParameter("@Schedule", item.Schedule);
                    var parTrainNo = new SqlParameter("@TrainNo", item.TrainNo);
                    var parOrigin = new SqlParameter("@Origin", item.Origin);
                    var parDestination = new SqlParameter("@Destination", item.Destination);
                    var parBreakType = new SqlParameter("@BreakType", item.BreakType);
                    var parPlannedETD = new SqlParameter("@PlannedETD", item.PlannedETD);
                    var parPlannedETA = new SqlParameter("@PlannedETA", item.PlannedETA);
                    var parLoco = new SqlParameter("@Loco", item.Loco);
                    var parLocoQty = new SqlParameter("@LocoQty", item.LocoQty);
                    var parNWBRef = new SqlParameter("@NWBRef", item.NWBRef);
                    var parPlannedTons = new SqlParameter("@PlannedTons", item.PlannedTons);
                    var parLoad = new SqlParameter("@Load", item.Load);
                    var parRemark = new SqlParameter("@Remark", item.Remark);
                    var parYQ = new SqlParameter("@YQ", item.YQ);
                    var parTrainStatus = new SqlParameter("@TrainStatus", item.TrainStatus);

                    var parTerminal = new SqlParameter("@Terminal", item.Terminal);
                    var parCreatedBy = new SqlParameter("@CreatedBy", _UserId);

                    _unitOfWork.ExecuteSqlCommand(
                        "dbo.usp_InsertRailPlan @PortCode,@Corridor,@PlannedDate,@Schedule,@TrainNo,@Origin,@Destination,@BreakType,@PlannedETD,@PlannedETA,@Loco,@LocoQty,@NWBRef,@PlannedTons,@Load,@Remark,@YQ,@TrainStatus,@CreatedBy",
                        parPortCode, parCorridor, parPlannedDate, parSchedule, parTrainNo, parOrigin, parDestination,
                        parBreakType, parPlannedETD, parPlannedETA, parLoco, parLocoQty, parNWBRef, parPlannedTons,
                        parLoad, parRemark, parYQ, parTrainStatus, parCreatedBy);

                }
            }
            return data;
        }

        #endregion 

        #region  Update Rail Change Notifications
        /// <summary>
        /// To Update Rail Change Notifications
        /// </summary>
        /// <param name="Data">Rail Change Notifications to be updated</param>
        /// <param name="_UserId"> User who is saving the data</param>
        /// <returns></returns>
        public List<RailPlanVO> UpdateRailChangeNotifications(List<RailPlanVO> data, int _UserId)
        {
            if (data != null)
            {
                //List<RailPlan> RailPlanValues = data.MapToEntity();

                foreach (var item in data)
                {
                    var parPortCode = new SqlParameter("@PortCode", item.PortCode);
                    var parCorridor = new SqlParameter("@Corridor", item.Corridor);
                    var parPlannedDate = new SqlParameter("@PlannedDate", item.PlannedDate);
                    var parTrainNo = new SqlParameter("@TrainNo", item.TrainNo);
                    var parReasonForChange = new SqlParameter("@ReasonForChange", item.ReasonForChange);
                    var parTrainStatus = new SqlParameter("@TrainStatus", item.TrainStatus);
                    var parNewETD = new SqlParameter("@NewETD", item.NewETD);
                    var parNewETA = new SqlParameter("@NewETA", item.NewETA);
                    var parCreatedBy = new SqlParameter("@CreatedBy", _UserId);

                    _unitOfWork.ExecuteSqlCommand(
                        "dbo.usp_UpdateRailChangeNotifications @PortCode,@Corridor,@PlannedDate,@TrainNo,@ReasonForChange,@TrainStatus,@NewETD,@NewETA,@CreatedBy",
                        parPortCode, parCorridor, parPlannedDate, parTrainNo, parReasonForChange, parTrainStatus,
                        parNewETD, parNewETA, parCreatedBy);

                }
            }
            return data;
        }

        #endregion 

        #region  Update Arrival and Departure Times
        /// <summary>
        /// To Update Arrival and Departure Times
        /// </summary>
        /// <param name="Data">Actual Arrival and Departure Times to be updated</param>
        /// <param name="_UserId"> User who is saving the data</param>
        /// <returns></returns>
        public List<RailPlanVO> UpdateArrivalAndDepartureTimes(List<RailPlanVO> data, int _UserId)
        {
            if (data != null)
            {
                //List<RailPlan> RailPlanValues = data.MapToEntity();

                foreach (var item in data)
                {
                    var parPortCode = new SqlParameter("@PortCode", item.PortCode);
                    var parCorridor = new SqlParameter("@Corridor", item.Corridor);
                    var parPlannedDate = new SqlParameter("@PlannedDate", item.PlannedDate);
                    var parTrainNo = new SqlParameter("@TrainNo", item.TrainNo);
                    var parTrainMovement = new SqlParameter("@TrainMovement", item.TrainMovement);
                    var parTrainStatus = new SqlParameter("@TrainStatus", item.TrainStatus);
                    //'00:00:00'
                    var parATD = (item.ATD == null)
                        ? new SqlParameter("@ATD", "00:00:00")
                        : new SqlParameter("@ATD", item.ATD);
                    var parATA = (item.ATA == null)
                        ? new SqlParameter("@ATA", "00:00:00")
                        : new SqlParameter("@ATA", item.ATA);

                    var parCreatedBy = new SqlParameter("@CreatedBy", _UserId);

                    _unitOfWork.ExecuteSqlCommand(
                        "dbo.usp_UpdateArrivalAndDepartureTimes @PortCode,@Corridor,@PlannedDate,@TrainNo,@TrainMovement,@ATD,@ATA,@CreatedBy",
                        parPortCode, parCorridor, parPlannedDate, parTrainNo, parTrainMovement, parATD, parATA,
                        parCreatedBy);

                }
            }
            return data;
        }

        #endregion 

        public List<TrainMonitoringVO> GetTrainMonitoringDetails(string plannedDate, string Corridor, string movementStatus)
        {
            List<RailPlanVO> RailPlanValues = new List<RailPlanVO>();
           
            var parPlannedDate = new SqlParameter("@plannedDate",plannedDate);
            var parCorridor = new SqlParameter("@Corridor", Corridor);
            var parmovementStatus = new SqlParameter("@movementStatus", movementStatus);

            var railplan = _unitOfWork.SqlQuery<TrainMonitoringVO>("dbo.usp_GetTrainMonitoringDetails @Corridor,@PlannedDate,@movementStatus",
              parCorridor, parPlannedDate, parmovementStatus).ToList();

            return railplan;

        }
        public int CheckDuplicateTerminalDataRecord(int weekNo, string PerformArea, string measure, string UoM, string cargoType)
        {

            var parCargoType = new SqlParameter("@CargoType", cargoType);
            var parWeekNo = new SqlParameter("@WeekNo", weekNo);
            var parPerformanceArea = new SqlParameter("@PerformanceArea", PerformArea);
            var parMeasure = new SqlParameter("@Measure", measure);
            var parUoM = new SqlParameter("@UOM", UoM);

            var recCount = _unitOfWork.ExecuteSqlCommand("dbo.usp_CheckDuplicateTerminalDataRecord @CargoType,@WeekNo,@PerformanceArea,@Measure,@UOM",
              parCargoType, parWeekNo, parPerformanceArea, parMeasure, parUoM);

            return recCount;
 
        }
     }
}