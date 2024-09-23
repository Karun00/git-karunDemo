using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using IPMS.Domain;
using System.Data;

namespace IPMS.Repository
{
    public class DashBoardRepository : IDashBoardRepository
    {
       
        private IUnitOfWork _unitOfWork;
        public DashBoardRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<DashBoardVO> DashBoardDetails(int UserID, string fromDate, string toDate, string PortCode)
        {
            var userid = new SqlParameter("@userid", UserID);
            var fromdate = new SqlParameter("@fromdate", fromDate);
            var todate = new SqlParameter("@todate", toDate);
            var portcode = new SqlParameter("@portcode", PortCode);

            var pt = _unitOfWork.SqlQuery<DashBoardVO>("Select * from  dbo.udf_GetDashboardDetails(@userid,@fromdate,@todate,@portcode)", userid, fromdate, todate, portcode).ToList();

            pt.FirstOrDefault().fromDate = Convert.ToDateTime(fromDate, CultureInfo.InvariantCulture);
            pt.FirstOrDefault().toDate = Convert.ToDateTime(toDate, CultureInfo.InvariantCulture);

            return pt;
        }

        public List<PlannedMovementsDtlsVO> GetPlannedMovementsCount(string portcode)
        {
            //return ExecuteFaultHandledOperation(() =>
            //{
            var portCode = new SqlParameter("@portcode ", portcode);
            //var LoginId = new SqlParameter("@UserId", _UserId);
            var plannedmvnts = _unitOfWork.SqlQuery<PlannedMovementsDtlsVO>("dbo.USP_PLANNED_MOVEMENT_DSHCOUNT @portcode", portCode).ToList();

            if (plannedmvnts != null && plannedmvnts.Count > 0)
            {
                for (int i = 0; i < plannedmvnts.Count; i++)
                {
                    plannedmvnts[i].PlannedMovementsCount = plannedmvnts[i].PlannedMovtsArrivalCount + plannedmvnts[i].PlannedMovtsSailingCount + plannedmvnts[i].PlannedMovtsShiftingCount + plannedmvnts[i].PlannedMovtsWarpingCount;

                }
            }
            else
            {
                plannedmvnts = new List<PlannedMovementsDtlsVO>() { new PlannedMovementsDtlsVO() { PlannedMovementsCount = 0, PlannedMovtsShiftingCount = 0, PlannedMovtsWarpingCount = 0, PlannedMovtsSailingCount = 0, PlannedMovtsArrivalCount = 0 } };
            }

            return plannedmvnts;


            //});
            //PlannedMovementsDtlsVO plannedmovements = new PlannedMovementsDtlsVO();
            //plannedmovements.PlannedMovementsCount = 0;
            //DateTime date = DateTime.Today;
            //var arrival = (from v in _unitOfWork.Repository<VesselCallMovement>().Queryable()
            //               join a in _unitOfWork.Repository<ArrivalNotification>().Queryable()
            //               on v.VCN equals a.VCN
            //               join ve in _unitOfWork.Repository<Vessel>().Queryable()
            //               on a.VesselID equals ve.VesselID
            //               join su in _unitOfWork.Repository<SubCategory>().Queryable() on ve.VesselType equals su.SubCatCode
            //                                          into temp
            //               from t in temp.DefaultIfEmpty()
            //               join ag in _unitOfWork.Repository<Agent>().Queryable() on a.AgentID equals ag.AgentID
            //               into temp1
            //               from t1 in temp1.DefaultIfEmpty()

            //               join s in _unitOfWork.Repository<ServiceRequest>().Queryable() on v.ServiceRequestID equals s.ServiceRequestID
            //               join sr in _unitOfWork.Repository<SubCategory>().Queryable() on s.MovementType equals sr.SubCatCode
            //               join slr in _unitOfWork.Repository<SubCategory>().Queryable() on v.SlotStatus equals slr.SubCatCode
            //               into temp2
            //               from t2 in temp2.DefaultIfEmpty()


            //               where v.MovementType == MovementTypes.ARRIVAL && v.MovementDateTime.Value >= date && v.RecordStatus == RecordStatus.Active && a.PortCode == portcode
            //               select v).Count();


            //var sailing = (from v in _unitOfWork.Repository<VesselCallMovement>().Queryable()
            //               join a in _unitOfWork.Repository<ArrivalNotification>().Queryable()
            //               on v.VCN equals a.VCN
            //               join ve in _unitOfWork.Repository<Vessel>().Queryable()
            //               on a.VesselID equals ve.VesselID
            //               join su in _unitOfWork.Repository<SubCategory>().Queryable() on ve.VesselType equals su.SubCatCode
            //                                          into temp
            //               from t in temp.DefaultIfEmpty()
            //               join ag in _unitOfWork.Repository<Agent>().Queryable() on a.AgentID equals ag.AgentID
            //               into temp1
            //               from t1 in temp1.DefaultIfEmpty()

            //               join s in _unitOfWork.Repository<ServiceRequest>().Queryable() on v.ServiceRequestID equals s.ServiceRequestID
            //               join sr in _unitOfWork.Repository<SubCategory>().Queryable() on s.MovementType equals sr.SubCatCode
            //               join slr in _unitOfWork.Repository<SubCategory>().Queryable() on v.SlotStatus equals slr.SubCatCode
            //               into temp2
            //               from t2 in temp2.DefaultIfEmpty()


            //               where v.MovementType == MovementTypes.SAILING && v.MovementDateTime.Value >= date && v.RecordStatus == RecordStatus.Active && a.PortCode == portcode
            //               select v).Count();
            //var shifting = (from v in _unitOfWork.Repository<VesselCallMovement>().Queryable()
            //                join a in _unitOfWork.Repository<ArrivalNotification>().Queryable()
            //                on v.VCN equals a.VCN
            //                join ve in _unitOfWork.Repository<Vessel>().Queryable()
            //                on a.VesselID equals ve.VesselID
            //                join su in _unitOfWork.Repository<SubCategory>().Queryable() on ve.VesselType equals su.SubCatCode
            //                                           into temp
            //                from t in temp.DefaultIfEmpty()
            //                join ag in _unitOfWork.Repository<Agent>().Queryable() on a.AgentID equals ag.AgentID
            //                into temp1
            //                from t1 in temp1.DefaultIfEmpty()

            //                join s in _unitOfWork.Repository<ServiceRequest>().Queryable() on v.ServiceRequestID equals s.ServiceRequestID
            //                join sr in _unitOfWork.Repository<SubCategory>().Queryable() on s.MovementType equals sr.SubCatCode
            //                join slr in _unitOfWork.Repository<SubCategory>().Queryable() on v.SlotStatus equals slr.SubCatCode
            //                into temp2
            //                from t2 in temp2.DefaultIfEmpty()


            //                where v.MovementType == MovementTypes.SHIFTING && v.MovementDateTime.Value >= date && v.RecordStatus == RecordStatus.Active && a.PortCode == portcode
            //                select v).Count();
            //var wappring = (from v in _unitOfWork.Repository<VesselCallMovement>().Queryable()
            //                join a in _unitOfWork.Repository<ArrivalNotification>().Queryable()
            //                on v.VCN equals a.VCN
            //                join ve in _unitOfWork.Repository<Vessel>().Queryable()
            //                on a.VesselID equals ve.VesselID
            //                join su in _unitOfWork.Repository<SubCategory>().Queryable() on ve.VesselType equals su.SubCatCode
            //                                           into temp
            //                from t in temp.DefaultIfEmpty()
            //                join ag in _unitOfWork.Repository<Agent>().Queryable() on a.AgentID equals ag.AgentID
            //                into temp1
            //                from t1 in temp1.DefaultIfEmpty()

            //                join s in _unitOfWork.Repository<ServiceRequest>().Queryable() on v.ServiceRequestID equals s.ServiceRequestID
            //                join sr in _unitOfWork.Repository<SubCategory>().Queryable() on s.MovementType equals sr.SubCatCode
            //                join slr in _unitOfWork.Repository<SubCategory>().Queryable() on v.SlotStatus equals slr.SubCatCode
            //                into temp2
            //                from t2 in temp2.DefaultIfEmpty()


            //                where v.MovementType == MovementTypes.WARPING && v.MovementDateTime.Value >= date && v.RecordStatus == RecordStatus.Active && a.PortCode == portcode
            //                select v).Count();


            //plannedmovements.PlannedMovtsArrivalCount = arrival;
            //plannedmovements.PlannedMovtsSailingCount = sailing;
            //plannedmovements.PlannedMovtsShiftingCount = shifting;
            //plannedmovements.PlannedMovtsWarpingCount = wappring;
            //plannedmovements.PlannedMovementsCount = arrival + sailing + shifting + wappring;
            //return plannedmovements;

        }

        public AnchorageDtlsVO GetAnchorageCount(string portcode)
        {


            var fromdate = new SqlParameter("@FromDate", DateTime.Now);
            var Portcode = new SqlParameter("@Port", portcode);
            //var portcode = new SqlParameter("@Port", PortCode);
            var vcn = new SqlParameter("@VCN", DBNull.Value);
            var todate = new SqlParameter("@ToDate", DBNull.Value);
            AnchorageDtlsVO anchoragedata = new AnchorageDtlsVO();
            Dictionary<string, int> iValues = new Dictionary<string, int>();
            var anchoragelist = _unitOfWork.SqlQuery<AnchorageDtlsVO>("dbo.usp_rpt_anchoredreport @VCN,@Port,@FromDate,@ToDate", vcn, Portcode, fromdate, todate).ToList();
            var Query = anchoragelist.GroupBy(
                (item => item.Reason),
                (key, elements) => new
                {
                    Reson = key,
                    count = elements
                        //.Distinct()
                            .Count()
                });

            foreach (var item in Query)
            {
                iValues.Add(item.Reson, item.count);
            }


            anchoragedata.LstofAnchorage = iValues;


            return anchoragedata;
        }
        public List<PortWiseCountVO> GetPortWiseCount(string portcode)
        {
            //PortWiseCountVO portwisecount = new PortWiseCountVO();
            //portwisecount.PlannedMovementsCount = 0;
            var Portcode = new SqlParameter("@PortCode", portcode);
            var portwisecountList = _unitOfWork.SqlQuery<PortWiseCountVO>("dbo.usp_GetBerthDtls @PortCode", Portcode).ToList();

            //var Query1 = portwisecountList.GroupBy(
            //     (item => ite),
            //     (key, elements) => new
            //     {
            //         AnchoredCount = key,
            //         count = elements
            //             //.Distinct()
            //                 .Count()
            //     });

            return portwisecountList;

        }
        public List<WegoDashBoardVO> GetWegoDashBoradDetails(DateTime fromDate, DateTime toDate, string PortCode)
        {

            var fromdate = new SqlParameter("@FromDate", fromDate);
            var todate = new SqlParameter("@ToDate", toDate);
            var portcode = new SqlParameter("@PortCode", PortCode);

            var fromdate1 = new SqlParameter("@fromDate", fromDate);
            var todate1 = new SqlParameter("@toDate", toDate);
            var portcode1 = new SqlParameter("@portCode", PortCode);


            var vesselsCount = _unitOfWork.SqlQuery<WegoVesselCountVO>("dbo.usp_GetWegoVesselCargoType @fromDate, @toDate, @portCode", fromdate1, todate1, portcode1).ToList();

            var vesselDetails = _unitOfWork.SqlQuery<WegoVesselDetailsVO>("dbo.usp_GetWegoDashBoardDetails @FromDate, @ToDate, @PortCode", fromdate, todate, portcode).ToList();

            int arrivalCommodity = 0;

            foreach (var item in vesselsCount)
            {
                if (item.VesselAutomative > 0) { arrivalCommodity = arrivalCommodity + 1; }
                if (item.VesselBreakBulk > 0) { arrivalCommodity = arrivalCommodity + 1; }
                if (item.VesselContainer > 0) { arrivalCommodity = arrivalCommodity + 1; }
                if (item.VesselBulk > 0) { arrivalCommodity = arrivalCommodity + 1; }
                if (item.VesselLiquidBulk > 0) { arrivalCommodity = arrivalCommodity + 1; }
                if (item.VesselNonOperational > 0) { arrivalCommodity = arrivalCommodity + 1; }
                if (item.VesselBunkers > 0) { arrivalCommodity = arrivalCommodity + 1; }
                if (item.VesselPassengers > 0) { arrivalCommodity = arrivalCommodity + 1; }

            }
            if (arrivalCommodity == 0) { arrivalCommodity = 1; }

            var automotive = vesselDetails.FindAll(item => item.CargoCode == SubCategoryConstants.Automotive);
            var objAutomotive = new WegoVesselVCNVO();

            if (automotive.Count > 0 && vesselsCount.Count > 0)
            {
                objAutomotive.NoofVessels = Convert.ToString(vesselsCount[0].VesselAutomative);
                objAutomotive.GRT = Convert.ToString(Math.Round(automotive[0].GRT / vesselsCount[0].VesselAutomative, 2));
                objAutomotive.LOA = Convert.ToString(Math.Round(automotive[0].LOA / vesselsCount[0].VesselAutomative, 2));
                objAutomotive.STAT = Convert.ToString(Math.Round(automotive[0].STAT / vesselsCount[0].VesselAutomative, 2));
                objAutomotive.VesselDelayAnchorage = Convert.ToString(Math.Round(automotive[0].VesselDelayAnchorage / vesselsCount[0].VesselAutomative, 2));
                objAutomotive.NPAManueringTime = Convert.ToString(Math.Round(automotive[0].NPAManueringTime / vesselsCount[0].VesselAutomative, 2));
                objAutomotive.PilotageIn = Convert.ToString(Math.Round(automotive[0].PilotageIn / vesselsCount[0].VesselAutomative, 2));
                objAutomotive.MarineServiceTimeIn = Convert.ToString(Math.Round(automotive[0].MarineServiceTimeIn / vesselsCount[0].VesselAutomative, 2));
                objAutomotive.MarineServiceTimeOut = Convert.ToString(Math.Round(automotive[0].MarineServiceTimeOut / vesselsCount[0].VesselAutomative, 2));

                objAutomotive.ShipWorkingHour = Convert.ToString(Math.Round(automotive[0].Volumes / automotive[0].StartEndCargo, 2));
                objAutomotive.BerthProductivity = Convert.ToString(Math.Round(automotive[0].Volumes / automotive[0].LastLineOffFirstLineIn, 2));
                objAutomotive.ShipProductivityIndicator = Convert.ToString(Math.Round(automotive[0].Volumes / automotive[0].STAT, 2));
                objAutomotive.TotalVolumes = Convert.ToString(Math.Round(automotive[0].Volumes, 2));
                objAutomotive.ParcelSizes = null;
                objAutomotive.PreCargoWorking = Convert.ToString(Math.Round(automotive[0].PreCargoWorking, 2));
                objAutomotive.WorkingTime = Convert.ToString(Math.Round(automotive[0].WorkingTime, 2));
                objAutomotive.DepartureWaiting = Convert.ToString(Math.Round(automotive[0].DepartureWaiting, 2));
            }

            var breakbulk = vesselDetails.FindAll(item => item.CargoCode == SubCategoryConstants.Breakbulk);
            var objBreakBulk = new WegoVesselVCNVO();

            if (breakbulk.Count > 0 && vesselsCount.Count > 0)
            {
                objBreakBulk.NoofVessels = Convert.ToString(vesselsCount[0].VesselBreakBulk);
                objBreakBulk.GRT = Convert.ToString(Math.Round(breakbulk[0].GRT / vesselsCount[0].VesselBreakBulk, 2));
                objBreakBulk.LOA = Convert.ToString(Math.Round(breakbulk[0].LOA / vesselsCount[0].VesselBreakBulk, 2));
                objBreakBulk.STAT = Convert.ToString(Math.Round(breakbulk[0].STAT / vesselsCount[0].VesselBreakBulk, 2));
                objBreakBulk.VesselDelayAnchorage = Convert.ToString(Math.Round(breakbulk[0].VesselDelayAnchorage / vesselsCount[0].VesselBreakBulk, 2));
                objBreakBulk.NPAManueringTime = Convert.ToString(Math.Round(breakbulk[0].NPAManueringTime / vesselsCount[0].VesselBreakBulk, 2));
                objBreakBulk.PilotageIn = Convert.ToString(Math.Round(breakbulk[0].PilotageIn / vesselsCount[0].VesselBreakBulk, 2));
                objBreakBulk.MarineServiceTimeIn = Convert.ToString(Math.Round(breakbulk[0].MarineServiceTimeIn / vesselsCount[0].VesselBreakBulk, 2));
                objBreakBulk.MarineServiceTimeOut = Convert.ToString(Math.Round(breakbulk[0].MarineServiceTimeOut / vesselsCount[0].VesselBreakBulk, 2));

                objBreakBulk.ShipWorkingHour = Convert.ToString(Math.Round(breakbulk[0].Volumes / breakbulk[0].StartEndCargo, 2));
                objBreakBulk.BerthProductivity = Convert.ToString(Math.Round(breakbulk[0].Volumes / breakbulk[0].LastLineOffFirstLineIn, 2));
                var stat = breakbulk[0].STAT <= 0 ? 1 : breakbulk[0].STAT;
                objBreakBulk.ShipProductivityIndicator = Convert.ToString(Math.Round(breakbulk[0].Volumes / stat, 2));
                objBreakBulk.TotalVolumes = Convert.ToString(Math.Round(breakbulk[0].Volumes, 2));
                objBreakBulk.ParcelSizes = null;
                objBreakBulk.PreCargoWorking = Convert.ToString(Math.Round(breakbulk[0].PreCargoWorking, 2));
                objBreakBulk.WorkingTime = Convert.ToString(Math.Round(breakbulk[0].WorkingTime, 2));
                objBreakBulk.DepartureWaiting = Convert.ToString(Math.Round(breakbulk[0].DepartureWaiting, 2));
            }

            var objBulk = new WegoVesselVCNVO();
            var bulk = vesselDetails.FindAll(item => item.CargoCode == SubCategoryConstants.Bulk);

            if (bulk.Count > 0 && bulk[0].GRT > 0 && vesselsCount.Count > 0)
            {
                objBulk.NoofVessels = Convert.ToString(vesselsCount[0].VesselBulk);
                objBulk.GRT = Convert.ToString(Math.Round(bulk[0].GRT / vesselsCount[0].VesselBulk, 2));
                objBulk.LOA = Convert.ToString(Math.Round(bulk[0].LOA / vesselsCount[0].VesselBulk, 2));
                objBulk.STAT = Convert.ToString(Math.Round(bulk[0].STAT / vesselsCount[0].VesselBulk, 2));
                objBulk.VesselDelayAnchorage = Convert.ToString(Math.Round(bulk[0].VesselDelayAnchorage / vesselsCount[0].VesselBulk, 2));
                objBulk.NPAManueringTime = Convert.ToString(Math.Round(bulk[0].NPAManueringTime / vesselsCount[0].VesselBulk, 2));
                objBulk.PilotageIn = Convert.ToString(Math.Round(bulk[0].PilotageIn / vesselsCount[0].VesselBulk, 2));
                objBulk.MarineServiceTimeIn = Convert.ToString(Math.Round(bulk[0].MarineServiceTimeIn / vesselsCount[0].VesselBulk, 2));
                objBulk.MarineServiceTimeOut = Convert.ToString(Math.Round(bulk[0].MarineServiceTimeOut / vesselsCount[0].VesselBulk, 2));

                objBulk.ShipWorkingHour = Convert.ToString(Math.Round(bulk[0].Volumes / bulk[0].StartEndCargo, 2));
                objBulk.BerthProductivity = Convert.ToString(Math.Round(bulk[0].Volumes / bulk[0].LastLineOffFirstLineIn, 2));
                objBulk.ShipProductivityIndicator = Convert.ToString(Math.Round(bulk[0].Volumes / bulk[0].STAT, 2));
                objBulk.TotalVolumes = Convert.ToString(Math.Round(bulk[0].Volumes, 2));
                objBulk.ParcelSizes = null;
                objBulk.PreCargoWorking = Convert.ToString(Math.Round(bulk[0].PreCargoWorking, 2));
                objBulk.WorkingTime = Convert.ToString(Math.Round(bulk[0].WorkingTime, 2));
                objBulk.DepartureWaiting = Convert.ToString(Math.Round(bulk[0].DepartureWaiting, 2));
            }



            var objContainer = new WegoVesselVCNVO();
            var containers = vesselDetails.FindAll(item => item.CargoCode == SubCategoryConstants.Container);

            if (containers.Count > 0 && vesselsCount.Count > 0)
            {
                objContainer.NoofVessels = Convert.ToString(vesselsCount[0].VesselContainer);
                objContainer.GRT = Convert.ToString(Math.Round(containers[0].GRT / vesselsCount[0].VesselContainer, 2));
                objContainer.LOA = Convert.ToString(Math.Round(containers[0].LOA / vesselsCount[0].VesselContainer, 2));
                objContainer.STAT = Convert.ToString(Math.Round(containers[0].STAT / vesselsCount[0].VesselContainer, 2));
                objContainer.VesselDelayAnchorage = Convert.ToString(Math.Round(containers[0].VesselDelayAnchorage / vesselsCount[0].VesselContainer, 2));
                objContainer.NPAManueringTime = Convert.ToString(Math.Round(containers[0].NPAManueringTime / vesselsCount[0].VesselContainer, 2));
                objContainer.PilotageIn = Convert.ToString(Math.Round(containers[0].PilotageIn / vesselsCount[0].VesselContainer, 2));
                objContainer.MarineServiceTimeIn = Convert.ToString(Math.Round(containers[0].MarineServiceTimeIn / vesselsCount[0].VesselContainer, 2));
                objContainer.MarineServiceTimeOut = Convert.ToString(Math.Round(containers[0].MarineServiceTimeOut / vesselsCount[0].VesselContainer, 2));

                objContainer.ShipWorkingHour = Convert.ToString(Math.Round(containers[0].Volumes / containers[0].StartEndCargo, 2));
                objContainer.BerthProductivity = Convert.ToString(Math.Round(containers[0].Volumes / containers[0].LastLineOffFirstLineIn, 2));
                objContainer.ShipProductivityIndicator = Convert.ToString(Math.Round(containers[0].Volumes / containers[0].STAT, 2));
                objContainer.TotalVolumes = Convert.ToString(Math.Round(containers[0].Volumes, 2));
                objContainer.ParcelSizes = null;
                objContainer.PreCargoWorking = Convert.ToString(Math.Round(containers[0].PreCargoWorking, 2));
                objContainer.WorkingTime = Convert.ToString(Math.Round(containers[0].WorkingTime, 2));
                objContainer.DepartureWaiting = Convert.ToString(Math.Round(containers[0].DepartureWaiting, 2));
            }

            var objLqdBulk = new WegoVesselVCNVO();
            var liquidbulk = vesselDetails.FindAll(item => item.CargoCode == SubCategoryConstants.LiquidBulk);
            if (liquidbulk.Count > 0 && vesselsCount.Count > 0)
            {
                objLqdBulk.NoofVessels = Convert.ToString(vesselsCount[0].VesselLiquidBulk);
                objLqdBulk.GRT = Convert.ToString(Math.Round(liquidbulk[0].GRT / vesselsCount[0].VesselLiquidBulk, 2));
                objLqdBulk.LOA = Convert.ToString(Math.Round(liquidbulk[0].LOA / vesselsCount[0].VesselLiquidBulk, 2));
                objLqdBulk.STAT = Convert.ToString(Math.Round(liquidbulk[0].STAT / vesselsCount[0].VesselLiquidBulk, 2));
                objLqdBulk.VesselDelayAnchorage = Convert.ToString(Math.Round(liquidbulk[0].VesselDelayAnchorage / vesselsCount[0].VesselLiquidBulk, 2));
                objLqdBulk.NPAManueringTime = Convert.ToString(Math.Round(liquidbulk[0].NPAManueringTime / vesselsCount[0].VesselLiquidBulk, 2));
                objLqdBulk.PilotageIn = Convert.ToString(Math.Round(liquidbulk[0].PilotageIn / vesselsCount[0].VesselLiquidBulk, 2));
                objLqdBulk.MarineServiceTimeIn = Convert.ToString(Math.Round(liquidbulk[0].MarineServiceTimeIn / vesselsCount[0].VesselLiquidBulk, 2));
                objLqdBulk.MarineServiceTimeOut = Convert.ToString(Math.Round(liquidbulk[0].MarineServiceTimeOut / vesselsCount[0].VesselLiquidBulk, 2));

                objLqdBulk.ShipWorkingHour = Convert.ToString(Math.Round(liquidbulk[0].Volumes / liquidbulk[0].StartEndCargo, 2));
                objLqdBulk.BerthProductivity = Convert.ToString(Math.Round(liquidbulk[0].Volumes / liquidbulk[0].LastLineOffFirstLineIn, 2));
                objLqdBulk.ShipProductivityIndicator = Convert.ToString(Math.Round(liquidbulk[0].Volumes / liquidbulk[0].STAT, 2));
                objLqdBulk.TotalVolumes = Convert.ToString(Math.Round(liquidbulk[0].Volumes, 2));
                objLqdBulk.ParcelSizes = null;
                objLqdBulk.PreCargoWorking = Convert.ToString(Math.Round(liquidbulk[0].PreCargoWorking, 2));
                objLqdBulk.WorkingTime = Convert.ToString(Math.Round(liquidbulk[0].WorkingTime, 2));
                objLqdBulk.DepartureWaiting = Convert.ToString(Math.Round(liquidbulk[0].DepartureWaiting, 2));
            }

            var objNonOperal = new WegoVesselVCNVO();
            var nonOperational = vesselDetails.FindAll(item => item.CargoCode == SubCategoryConstants.NonCargoWorking);

            if (nonOperational.Count > 0 && vesselsCount.Count > 0)
            {
                objNonOperal.NoofVessels = Convert.ToString(vesselsCount[0].VesselNonOperational);
                objNonOperal.GRT = Convert.ToString(Math.Round(nonOperational[0].GRT / vesselsCount[0].VesselNonOperational, 2));
                objNonOperal.LOA = Convert.ToString(Math.Round(nonOperational[0].LOA / vesselsCount[0].VesselNonOperational, 2));
                objNonOperal.STAT = Convert.ToString(Math.Round(nonOperational[0].STAT / vesselsCount[0].VesselNonOperational, 2));
                objNonOperal.VesselDelayAnchorage = Convert.ToString(Math.Round(nonOperational[0].VesselDelayAnchorage / vesselsCount[0].VesselNonOperational, 2));
                objNonOperal.NPAManueringTime = Convert.ToString(Math.Round(nonOperational[0].NPAManueringTime / vesselsCount[0].VesselNonOperational, 2));
                objNonOperal.PilotageIn = Convert.ToString(Math.Round(nonOperational[0].PilotageIn / vesselsCount[0].VesselNonOperational, 2));
                objNonOperal.MarineServiceTimeIn = Convert.ToString(Math.Round(nonOperational[0].MarineServiceTimeIn / vesselsCount[0].VesselNonOperational, 2));
                objNonOperal.MarineServiceTimeOut = Convert.ToString(Math.Round(nonOperational[0].MarineServiceTimeOut / vesselsCount[0].VesselNonOperational, 2));

                objNonOperal.ShipWorkingHour = Convert.ToString(Math.Round(nonOperational[0].Volumes / nonOperational[0].StartEndCargo, 2));
                objNonOperal.BerthProductivity = Convert.ToString(Math.Round(nonOperational[0].Volumes / nonOperational[0].LastLineOffFirstLineIn, 2));
                objNonOperal.ShipProductivityIndicator = Convert.ToString(Math.Round(nonOperational[0].Volumes / nonOperational[0].STAT, 2));
                objNonOperal.TotalVolumes = Convert.ToString(Math.Round(nonOperational[0].Volumes, 2));
                objNonOperal.ParcelSizes = null;
                objNonOperal.PreCargoWorking = Convert.ToString(Math.Round(nonOperational[0].PreCargoWorking, 2));
                objNonOperal.WorkingTime = Convert.ToString(Math.Round(nonOperational[0].WorkingTime, 2));
                objNonOperal.DepartureWaiting = Convert.ToString(Math.Round(nonOperational[0].DepartureWaiting, 2));
            }


            var objBunkers = new WegoVesselVCNVO();
            var bunkers = vesselDetails.FindAll(item => item.CargoCode == SubCategoryConstants.Bunkers);
            if (bunkers.Count > 0 && vesselsCount.Count > 0)
            {
                objBunkers.NoofVessels = Convert.ToString(vesselsCount[0].VesselBunkers);
                objBunkers.GRT = Convert.ToString(Math.Round(bunkers[0].GRT / vesselsCount[0].VesselBunkers, 2));
                objBunkers.LOA = Convert.ToString(Math.Round(bunkers[0].LOA / vesselsCount[0].VesselBunkers, 2));
                objBunkers.STAT = Convert.ToString(Math.Round(bunkers[0].STAT / vesselsCount[0].VesselBunkers, 2));
                objBunkers.VesselDelayAnchorage = Convert.ToString(Math.Round(bunkers[0].VesselDelayAnchorage / vesselsCount[0].VesselBunkers, 2));
                objBunkers.NPAManueringTime = Convert.ToString(Math.Round(bunkers[0].NPAManueringTime / vesselsCount[0].VesselBunkers, 2));
                objBunkers.PilotageIn = Convert.ToString(Math.Round(bunkers[0].PilotageIn / vesselsCount[0].VesselBunkers, 2));
                objBunkers.MarineServiceTimeIn = Convert.ToString(Math.Round(bunkers[0].MarineServiceTimeIn / vesselsCount[0].VesselBunkers, 2));
                objBunkers.MarineServiceTimeOut = Convert.ToString(Math.Round(bunkers[0].MarineServiceTimeOut / vesselsCount[0].VesselBunkers, 2));

                objBunkers.ShipWorkingHour = Convert.ToString(Math.Round(bunkers[0].Volumes / bunkers[0].StartEndCargo, 2));
                objBunkers.BerthProductivity = Convert.ToString(Math.Round(bunkers[0].Volumes / bunkers[0].LastLineOffFirstLineIn, 2));
                objBunkers.ShipProductivityIndicator = Convert.ToString(Math.Round(bunkers[0].Volumes / bunkers[0].STAT, 2));
                objBunkers.TotalVolumes = Convert.ToString(Math.Round(bunkers[0].Volumes, 2));
                objBunkers.ParcelSizes = null;
                objBunkers.PreCargoWorking = Convert.ToString(Math.Round(bunkers[0].PreCargoWorking, 2));
                objBunkers.WorkingTime = Convert.ToString(Math.Round(bunkers[0].WorkingTime, 2));
                objBunkers.DepartureWaiting = Convert.ToString(Math.Round(bunkers[0].DepartureWaiting, 2));
            }


            var objPassengers = new WegoVesselVCNVO();
            var passengers = vesselDetails.FindAll(item => item.CargoCode == SubCategoryConstants.Passengers);
            if (passengers.Count > 0 && vesselsCount.Count > 0)
            {
                objPassengers.NoofVessels = Convert.ToString(vesselsCount[0].VesselPassengers);
                objPassengers.GRT = Convert.ToString(Math.Round(passengers[0].GRT / vesselsCount[0].VesselPassengers, 2));
                objPassengers.LOA = Convert.ToString(Math.Round(passengers[0].LOA / vesselsCount[0].VesselPassengers, 2));
                objPassengers.STAT = Convert.ToString(Math.Round(passengers[0].STAT / vesselsCount[0].VesselPassengers, 2));
                objPassengers.VesselDelayAnchorage = Convert.ToString(Math.Round(passengers[0].VesselDelayAnchorage / vesselsCount[0].VesselPassengers, 2));
                objPassengers.NPAManueringTime = Convert.ToString(Math.Round(passengers[0].NPAManueringTime / vesselsCount[0].VesselPassengers, 2));
                objPassengers.PilotageIn = Convert.ToString(Math.Round(passengers[0].PilotageIn / vesselsCount[0].VesselPassengers, 2));
                objPassengers.MarineServiceTimeIn = Convert.ToString(Math.Round(passengers[0].MarineServiceTimeIn / vesselsCount[0].VesselPassengers, 2));
                objPassengers.MarineServiceTimeOut = Convert.ToString(Math.Round(passengers[0].MarineServiceTimeOut / vesselsCount[0].VesselPassengers, 2));


                objPassengers.ShipWorkingHour = Convert.ToString(Math.Round(passengers[0].Volumes / passengers[0].StartEndCargo, 2));
                objPassengers.BerthProductivity = Convert.ToString(Math.Round(passengers[0].Volumes / passengers[0].LastLineOffFirstLineIn, 2));
                objPassengers.ShipProductivityIndicator = Convert.ToString(Math.Round(passengers[0].Volumes / passengers[0].STAT, 2));
                objPassengers.TotalVolumes = Convert.ToString(Math.Round(passengers[0].Volumes, 2));
                objPassengers.ParcelSizes = null;
                objPassengers.PreCargoWorking = Convert.ToString(Math.Round(passengers[0].PreCargoWorking, 2));
                objPassengers.WorkingTime = Convert.ToString(Math.Round(passengers[0].WorkingTime, 2));
                objPassengers.DepartureWaiting = Convert.ToString(Math.Round(passengers[0].DepartureWaiting, 2));
            }


            var objALL = new WegoVesselVCNVO();

            objALL.NoofVessels = Convert.ToString(Convert.ToInt32(objAutomotive.NoofVessels) + Convert.ToInt32(objBreakBulk.NoofVessels) + Convert.ToInt32(objBulk.NoofVessels) + Convert.ToInt32(objContainer.NoofVessels) + Convert.ToInt32(objLqdBulk.NoofVessels) + Convert.ToInt32(objNonOperal.NoofVessels) + Convert.ToInt32(objBunkers.NoofVessels) + Convert.ToInt32(objPassengers.NoofVessels));
            objALL.GRT = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.GRT) + Convert.ToDouble(objBreakBulk.GRT) + Convert.ToDouble(objBulk.GRT) + Convert.ToDouble(objContainer.GRT) + Convert.ToDouble(objLqdBulk.GRT) + Convert.ToDouble(objNonOperal.GRT) + Convert.ToDouble(objBunkers.GRT) + Convert.ToDouble(objPassengers.GRT)) / arrivalCommodity, 2));
            objALL.LOA = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.LOA) + Convert.ToDouble(objBreakBulk.LOA) + Convert.ToDouble(objBulk.LOA) + Convert.ToDouble(objContainer.LOA) + Convert.ToDouble(objLqdBulk.LOA) + Convert.ToDouble(objNonOperal.LOA) + Convert.ToDouble(objBunkers.LOA) + Convert.ToDouble(objPassengers.LOA)) / arrivalCommodity, 2));
            objALL.STAT = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.STAT) + Convert.ToDouble(objBreakBulk.STAT) + Convert.ToDouble(objBulk.STAT) + Convert.ToDouble(objContainer.STAT) + Convert.ToDouble(objLqdBulk.STAT) + Convert.ToDouble(objNonOperal.STAT) + Convert.ToDouble(objBunkers.STAT) + Convert.ToDouble(objPassengers.STAT)) / arrivalCommodity, 2));
            objALL.VesselDelayAnchorage = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.VesselDelayAnchorage) + Convert.ToDouble(objBreakBulk.VesselDelayAnchorage) + Convert.ToDouble(objBulk.VesselDelayAnchorage) + Convert.ToDouble(objContainer.VesselDelayAnchorage) + Convert.ToDouble(objLqdBulk.VesselDelayAnchorage) + Convert.ToDouble(objNonOperal.VesselDelayAnchorage) + Convert.ToDouble(objBunkers.VesselDelayAnchorage) + Convert.ToDouble(objPassengers.VesselDelayAnchorage)) / arrivalCommodity, 2));
            objALL.NPAManueringTime = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.NPAManueringTime) + Convert.ToDouble(objBreakBulk.NPAManueringTime) + Convert.ToDouble(objBulk.NPAManueringTime) + Convert.ToDouble(objContainer.NPAManueringTime) + Convert.ToDouble(objLqdBulk.NPAManueringTime) + Convert.ToDouble(objNonOperal.NPAManueringTime) + Convert.ToDouble(objBunkers.NPAManueringTime) + Convert.ToDouble(objPassengers.NPAManueringTime)) / arrivalCommodity, 2));
            objALL.PilotageIn = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.PilotageIn) + Convert.ToDouble(objBreakBulk.PilotageIn) + Convert.ToDouble(objBulk.PilotageIn) + Convert.ToDouble(objContainer.PilotageIn) + Convert.ToDouble(objLqdBulk.PilotageIn) + Convert.ToDouble(objNonOperal.PilotageIn) + Convert.ToDouble(objBunkers.PilotageIn) + Convert.ToDouble(objPassengers.PilotageIn)) / arrivalCommodity, 2));
            objALL.MarineServiceTimeIn = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.MarineServiceTimeIn) + Convert.ToDouble(objBreakBulk.MarineServiceTimeIn) + Convert.ToDouble(objBulk.MarineServiceTimeIn) + Convert.ToDouble(objContainer.MarineServiceTimeIn) + Convert.ToDouble(objLqdBulk.MarineServiceTimeIn) + Convert.ToDouble(objNonOperal.MarineServiceTimeIn) + Convert.ToDouble(objBunkers.MarineServiceTimeIn) + Convert.ToDouble(objPassengers.MarineServiceTimeIn)) / arrivalCommodity, 2));
            objALL.MarineServiceTimeOut = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.MarineServiceTimeOut) + Convert.ToDouble(objBreakBulk.MarineServiceTimeOut) + Convert.ToDouble(objBulk.MarineServiceTimeOut) + Convert.ToDouble(objContainer.MarineServiceTimeOut) + Convert.ToDouble(objLqdBulk.MarineServiceTimeOut) + Convert.ToDouble(objNonOperal.MarineServiceTimeOut) + Convert.ToDouble(objBunkers.MarineServiceTimeOut) + Convert.ToDouble(objPassengers.MarineServiceTimeOut)) / arrivalCommodity, 2));

            objALL.ShipWorkingHour = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.ShipWorkingHour) + Convert.ToDouble(objBreakBulk.ShipWorkingHour) + Convert.ToDouble(objBulk.ShipWorkingHour) + Convert.ToDouble(objContainer.ShipWorkingHour) + Convert.ToDouble(objLqdBulk.ShipWorkingHour) + Convert.ToDouble(objNonOperal.ShipWorkingHour) + Convert.ToDouble(objBunkers.ShipWorkingHour) + Convert.ToDouble(objPassengers.ShipWorkingHour)) / arrivalCommodity, 2));
            objALL.BerthProductivity = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.BerthProductivity) + Convert.ToDouble(objBreakBulk.BerthProductivity) + Convert.ToDouble(objBulk.BerthProductivity) + Convert.ToDouble(objContainer.BerthProductivity) + Convert.ToDouble(objLqdBulk.BerthProductivity) + Convert.ToDouble(objNonOperal.BerthProductivity) + Convert.ToDouble(objBunkers.BerthProductivity) + Convert.ToDouble(objPassengers.BerthProductivity)) / arrivalCommodity, 2));
            objALL.ShipProductivityIndicator = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.ShipProductivityIndicator) + Convert.ToDouble(objBreakBulk.ShipProductivityIndicator) + Convert.ToDouble(objBulk.ShipProductivityIndicator) + Convert.ToDouble(objContainer.ShipProductivityIndicator) + Convert.ToDouble(objLqdBulk.ShipProductivityIndicator) + Convert.ToDouble(objNonOperal.ShipProductivityIndicator) + Convert.ToDouble(objBunkers.ShipProductivityIndicator) + Convert.ToDouble(objPassengers.ShipProductivityIndicator)) / arrivalCommodity, 2));
            objALL.TotalVolumes = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.TotalVolumes) + Convert.ToDouble(objBreakBulk.TotalVolumes) + Convert.ToDouble(objBulk.TotalVolumes) + Convert.ToDouble(objContainer.TotalVolumes) + Convert.ToDouble(objLqdBulk.TotalVolumes) + Convert.ToDouble(objNonOperal.TotalVolumes) + Convert.ToDouble(objBunkers.TotalVolumes) + Convert.ToDouble(objPassengers.TotalVolumes)) / arrivalCommodity, 2));
            objALL.ParcelSizes = null;
            objALL.PreCargoWorking = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.PreCargoWorking) + Convert.ToDouble(objBreakBulk.PreCargoWorking) + Convert.ToDouble(objBulk.PreCargoWorking) + Convert.ToDouble(objContainer.PreCargoWorking) + Convert.ToDouble(objLqdBulk.PreCargoWorking) + Convert.ToDouble(objNonOperal.PreCargoWorking) + Convert.ToDouble(objBunkers.PreCargoWorking) + Convert.ToDouble(objPassengers.PreCargoWorking)) / arrivalCommodity, 2));
            objALL.WorkingTime = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.WorkingTime) + Convert.ToDouble(objBreakBulk.WorkingTime) + Convert.ToDouble(objBulk.WorkingTime) + Convert.ToDouble(objContainer.WorkingTime) + Convert.ToDouble(objLqdBulk.WorkingTime) + Convert.ToDouble(objNonOperal.WorkingTime) + Convert.ToDouble(objBunkers.WorkingTime) + Convert.ToDouble(objPassengers.WorkingTime)) / arrivalCommodity, 2));
            objALL.DepartureWaiting = Convert.ToString(Math.Round((Convert.ToDouble(objAutomotive.DepartureWaiting) + Convert.ToDouble(objBreakBulk.DepartureWaiting) + Convert.ToDouble(objBulk.DepartureWaiting) + Convert.ToDouble(objContainer.DepartureWaiting) + Convert.ToDouble(objLqdBulk.DepartureWaiting) + Convert.ToDouble(objNonOperal.DepartureWaiting) + Convert.ToDouble(objBunkers.DepartureWaiting) + Convert.ToDouble(objPassengers.DepartureWaiting)) / arrivalCommodity, 2));


            List<WegoDashBoardVO> list = new List<WegoDashBoardVO>
        {
            new WegoDashBoardVO {WegoKPI = "Number of Vessels", Automotive= objAutomotive.NoofVessels, BreakBulk = objBreakBulk.NoofVessels, Bulk = objBulk.NoofVessels, Container = objContainer.NoofVessels,  LiquidBulk = objLqdBulk.NoofVessels, NonOperational = objNonOperal.NoofVessels, Bunkers = objBunkers.NoofVessels, Passengers = objPassengers.NoofVessels,ALL = objALL.NoofVessels},            
            new WegoDashBoardVO {WegoKPI = "Average GRT (Tons)", Automotive = objAutomotive.GRT, BreakBulk = objBreakBulk.GRT, Bulk = objBulk.GRT, Container = objContainer.GRT,  LiquidBulk = objLqdBulk.GRT, NonOperational = objNonOperal.GRT, Bunkers = objBunkers.GRT, Passengers = objPassengers.GRT,ALL = objALL.GRT},            
            new WegoDashBoardVO {WegoKPI = "Average LOA (Meters)", Automotive= objAutomotive.LOA, BreakBulk = objBreakBulk.LOA, Bulk = objBulk.LOA, Container = objContainer.LOA,  LiquidBulk = objLqdBulk.LOA, NonOperational = objNonOperal.LOA, Bunkers = objBunkers.LOA, Passengers = objPassengers.LOA, ALL = objALL.LOA},            
            new WegoDashBoardVO {WegoKPI = "Parcel Sizes", Automotive= objAutomotive.ParcelSizes, BreakBulk = objBreakBulk.ParcelSizes, Bulk = objBulk.ParcelSizes, Container = objContainer.ParcelSizes,  LiquidBulk = objLqdBulk.ParcelSizes, NonOperational = objNonOperal.ParcelSizes, Bunkers = objBunkers.ParcelSizes, Passengers = objPassengers.ParcelSizes, ALL = objALL.ParcelSizes},            
            new WegoDashBoardVO {WegoKPI = "Total Volumes Handled", Automotive= objAutomotive.TotalVolumes, BreakBulk = objBreakBulk.TotalVolumes, Bulk = objBulk.TotalVolumes, Container = objContainer.TotalVolumes,  LiquidBulk = objLqdBulk.TotalVolumes, NonOperational = objNonOperal.TotalVolumes, Bunkers = objBunkers.TotalVolumes, Passengers = objPassengers.TotalVolumes, ALL = objALL.TotalVolumes},                
            new WegoDashBoardVO {WegoKPI = "Ship Turnaround Time (Hours)", Automotive= objAutomotive.STAT, BreakBulk = objBreakBulk.STAT, Bulk = objBulk.STAT, Container = objContainer.STAT,  LiquidBulk = objLqdBulk.STAT, NonOperational = objNonOperal.STAT, Bunkers = objBunkers.STAT, Passengers = objPassengers.STAT, ALL = objALL.STAT},            
            new WegoDashBoardVO {WegoKPI = "Ship Productivity Indicator (Tons/Hour)", Automotive= objAutomotive.ShipProductivityIndicator, BreakBulk = objBreakBulk.ShipProductivityIndicator, Bulk = objBulk.ShipProductivityIndicator, Container = objContainer.ShipProductivityIndicator,  LiquidBulk = objLqdBulk.ShipProductivityIndicator, NonOperational = objNonOperal.ShipProductivityIndicator, Bunkers = objBunkers.ShipProductivityIndicator, Passengers = objPassengers.ShipProductivityIndicator, ALL = objALL.ShipProductivityIndicator},            
            new WegoDashBoardVO {WegoKPI = "Vessel Delay at Anchorage (Hours)", Automotive= objAutomotive.VesselDelayAnchorage, BreakBulk = objBreakBulk.VesselDelayAnchorage, Bulk = objBulk.VesselDelayAnchorage, Container = objContainer.VesselDelayAnchorage,  LiquidBulk = objLqdBulk.VesselDelayAnchorage, NonOperational = objNonOperal.VesselDelayAnchorage, Bunkers = objBunkers.VesselDelayAnchorage, Passengers = objPassengers.VesselDelayAnchorage, ALL = objALL.VesselDelayAnchorage},            
            new WegoDashBoardVO {WegoKPI = "Berth Productivity (Tons/Hour)", Automotive= objAutomotive.BerthProductivity, BreakBulk = objBreakBulk.BerthProductivity, Bulk = objBulk.BerthProductivity, Container = objContainer.BerthProductivity,  LiquidBulk = objLqdBulk.BerthProductivity, NonOperational = objNonOperal.BerthProductivity, Bunkers = objBunkers.BerthProductivity, Passengers = objPassengers.BerthProductivity, ALL = objALL.BerthProductivity},            
            new WegoDashBoardVO {WegoKPI = "Ship Working Hour (Tons/Hour)", Automotive= objAutomotive.ShipWorkingHour, BreakBulk = objBreakBulk.ShipWorkingHour, Bulk = objBulk.ShipWorkingHour, Container = objContainer.ShipWorkingHour,  LiquidBulk = objLqdBulk.ShipWorkingHour, NonOperational = objNonOperal.ShipWorkingHour, Bunkers = objBunkers.ShipWorkingHour, Passengers = objPassengers.ShipWorkingHour, ALL = objALL.ShipWorkingHour},                     
            new WegoDashBoardVO {WegoKPI = "NPA Manoeuvering Time-In (Hours)", Automotive= objAutomotive.NPAManueringTime, BreakBulk = objBreakBulk.NPAManueringTime, Bulk = objBulk.NPAManueringTime, Container = objContainer.NPAManueringTime,  LiquidBulk = objLqdBulk.NPAManueringTime, NonOperational = objNonOperal.NPAManueringTime, Bunkers = objBunkers.NPAManueringTime, Passengers = objPassengers.NPAManueringTime, ALL = objALL.NPAManueringTime},            
            new WegoDashBoardVO {WegoKPI = "Pilotage-In (Hours)", Automotive= objAutomotive.PilotageIn, BreakBulk = objBreakBulk.PilotageIn, Bulk = objBulk.PilotageIn, Container = objContainer.PilotageIn,  LiquidBulk = objLqdBulk.PilotageIn, NonOperational = objNonOperal.PilotageIn, Bunkers = objBunkers.PilotageIn,Passengers = objPassengers.PilotageIn, ALL = objALL.PilotageIn},            
            new WegoDashBoardVO {WegoKPI = "Marine Service Time - In (Hours)", Automotive= objAutomotive.MarineServiceTimeIn, BreakBulk = objBreakBulk.MarineServiceTimeIn, Bulk = objBulk.MarineServiceTimeIn, Container = objContainer.MarineServiceTimeIn,  LiquidBulk = objLqdBulk.MarineServiceTimeIn, NonOperational = objNonOperal.MarineServiceTimeIn, Bunkers = objBunkers.MarineServiceTimeIn, Passengers = objPassengers.MarineServiceTimeIn,ALL = objALL.MarineServiceTimeIn},                        
            new WegoDashBoardVO {WegoKPI = "Marine Service Time – Out  (Hours)", Automotive= objAutomotive.MarineServiceTimeOut, BreakBulk = objBreakBulk.MarineServiceTimeOut, Bulk = objBulk.MarineServiceTimeOut, Container = objContainer.MarineServiceTimeOut,  LiquidBulk = objLqdBulk.MarineServiceTimeOut, NonOperational = objNonOperal.MarineServiceTimeOut, Bunkers = objBunkers.MarineServiceTimeOut, Passengers = objPassengers.MarineServiceTimeOut, ALL = objALL.MarineServiceTimeOut},                      
            new WegoDashBoardVO {WegoKPI = "Pre Cargo Working (Hours)", Automotive= objAutomotive.PreCargoWorking, BreakBulk = objBreakBulk.PreCargoWorking, Bulk = objBulk.PreCargoWorking, Container = objContainer.PreCargoWorking,  LiquidBulk = objLqdBulk.PreCargoWorking, NonOperational = objNonOperal.PreCargoWorking, Bunkers = objBunkers.PreCargoWorking, Passengers = objPassengers.PreCargoWorking, ALL = objALL.PreCargoWorking},            
            new WegoDashBoardVO {WegoKPI = "Working Time (Hours)", Automotive= objAutomotive.WorkingTime, BreakBulk = objBreakBulk.WorkingTime, Bulk = objBulk.WorkingTime, Container = objContainer.WorkingTime,  LiquidBulk = objLqdBulk.WorkingTime, NonOperational = objNonOperal.WorkingTime, Bunkers = objBunkers.WorkingTime, Passengers = objPassengers.WorkingTime, ALL = objALL.WorkingTime},            
            new WegoDashBoardVO {WegoKPI = "Departure Waiting (Hours)", Automotive= objAutomotive.DepartureWaiting, BreakBulk = objBreakBulk.DepartureWaiting, Bulk = objBulk.DepartureWaiting, Container = objContainer.DepartureWaiting,  LiquidBulk = objLqdBulk.DepartureWaiting, NonOperational = objNonOperal.DepartureWaiting, Bunkers = objBunkers.DepartureWaiting, Passengers = objPassengers.DepartureWaiting, ALL = objALL.DepartureWaiting},            

        };

            return list;
        }
        //Total Movements Dashboard
        public List<TotalMovementsDashBoardVO> TotalMovementsDashboardDetails(DateTime fromDate, DateTime toDate)
        {

            var fromdate = new SqlParameter("@FromDate", fromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            var todate = new SqlParameter("@ToDate", toDate.ToString("yyyy-MM-dd HH:mm:ss"));
            //var portcode = new SqlParameter("@PortCode", PortCode);

            var vesselDetails = _unitOfWork.SqlQuery<TotalMovementsDashBoardVO>("dbo.usp_rpt_Total_Movements_Dashboard @FromDate, @ToDate", fromdate, todate).ToList();

            return vesselDetails;
        }
        //GetAllPorts
        public List<GetAllPorts> GetAllPorts()
        {
            var getallports = _unitOfWork.SqlQuery<GetAllPorts>("dbo.usp_rpt_GetArrivalPorts").ToList();

            return getallports;
        }
        public List<WegoBerthUtilizationVO> GetWegoBerthUtilizationData(string fromDate, string toDate, string PortCode)
        {
            DateTime frmDT = !string.IsNullOrEmpty(fromDate) ? Convert.ToDateTime(fromDate) : DateTime.MinValue;
            DateTime toDT = !string.IsNullOrEmpty(toDate) ? Convert.ToDateTime(toDate) : DateTime.MinValue;

            var fromdate = new SqlParameter("@FromDate", frmDT);
            var todate = new SqlParameter("@ToDate", toDT);
            var portcode = new SqlParameter("@PortCode", PortCode);

            var wegoBerthDetails = _unitOfWork.SqlQuery<WegoBerthUtilizationVO>("dbo.usp_GetWegoBerthUtilization @FromDate, @ToDate, @PortCode", fromdate, todate, portcode).ToList();

            return wegoBerthDetails;

        }

        public List<CargoTypeDashboardVO> CargoTypeDashboard(string fromDate, string toDate, string portcode)
        {
            DateTime frmDT = !string.IsNullOrEmpty(fromDate) ? Convert.ToDateTime(fromDate) : DateTime.MinValue;
            DateTime toDT = !string.IsNullOrEmpty(toDate) ? Convert.ToDateTime(toDate) : DateTime.MinValue;
            //if (portcode == "null")
            //{ portcode = (DBNull.Value).ToString(); }
            var fromdate = new SqlParameter("@FromDate", frmDT);
            var todate = new SqlParameter("@ToDate", toDT);
            var Portcode = new SqlParameter("@PortCode", portcode);
               if (portcode == "null")
               {
                   Portcode = new SqlParameter("@PortCode", DBNull.Value);
               }
          

            var cargoDetails = _unitOfWork.SqlQuery<CargoTypeDashboardVO>("dbo.usp_Cargo_wise_count @FromDate, @ToDate, @PortCode", fromdate, todate, Portcode).ToList();

            return cargoDetails;

        }
    }
    public class Reslt
    {
        public string val1 { set; get; }
        public int val2 { set; get; }
    }
}

