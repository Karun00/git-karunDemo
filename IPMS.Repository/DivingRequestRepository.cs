using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using System.Data.SqlClient;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using System.Data.Entity;
using System.Globalization;

namespace IPMS.Repository
{
    public class DivingRequestRepository : IDivingRequestRepository
    {
        private IUnitOfWork _unitOfWork;

        public DivingRequestRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetPortQuays
        /// <summary>
        /// gets Quays data
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<QuayVO> GetPortQuays(string portCode)
        {
            var portquays = _unitOfWork.Repository<Quay>().Queryable().OrderBy(x => x.QuayName).Where(x => x.PortCode == portCode && x.RecordStatus == RecordStatus.Active).ToList();
            List<QuayVO> portquaysvo = new List<QuayVO>();
            foreach (var portquay in portquays)
            {
                QuayVO qvo = new QuayVO();
                qvo = QuayMapExtension.MapToDto(portquay);
                portquaysvo.Add(qvo);
            }
            return portquaysvo;
        }
        #endregion

        #region GetOtherLocations
        /// <summary>
        /// Gets other locations
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<LocationVO> GetOtherLocations(string portCode)
        {
            var otherlocation = (from a in _unitOfWork.Repository<Location>().Queryable()
                                 where a.RecordStatus == "A" && a.PortCode == portCode
                                 select a).OrderBy(a=>a.LocationName).ToList();
            List<LocationVO> locationvo = new List<LocationVO>();
            foreach (var location in otherlocation)
            {
                LocationVO otherlocationvo = new LocationVO();
                otherlocationvo = LocationMapExtension.MapToDTO(location);
                locationvo.Add(otherlocationvo);
            }
            return locationvo;
        }
        #endregion

        #region GetQuayBerths
        /// <summary>
        /// Gets berths data
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <returns></returns>
        public List<BerthVO> GetQuayBerths(string portCode, string quayCode)
        {
            var quayberths = _unitOfWork.Repository<Berth>().Queryable().OrderBy(x => x.BerthName).Where(x => x.QuayCode == quayCode && x.PortCode == portCode && x.RecordStatus == RecordStatus.Active && x.BerthType != DryDockStatus.DryDockType).ToList();
            List<BerthVO> BerthVOList = new List<BerthVO>();
            foreach (var berth in quayberths)
            {
                BerthVO objBerthVO = new BerthVO();
                objBerthVO = BerthMapExtension.MapToDto(berth);
                BerthVOList.Add(objBerthVO);
            }
            return BerthVOList;
        }
        #endregion

        #region GetBerthBollards
        /// <summary>
        /// Gets bollards data
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="quayCode"></param>
        /// <param name="berthCode"></param>
        /// <returns></returns>
        public List<BollardVO> GetBerthBollards(string portCode, string quayCode, string berthCode)
        {
            var berthbollards = _unitOfWork.Repository<Bollard>().Queryable().OrderBy(x => x.BollardName).Where(x => x.BerthCode == berthCode && x.QuayCode == quayCode && x.PortCode == portCode && x.RecordStatus == RecordStatus.Active).ToList();
            List<BollardVO> BollardVOList = new List<BollardVO>();
            foreach (var bollard in berthbollards)
            {
                BollardVO objBollardVO = new BollardVO();
                objBollardVO = BollardMapExtension.MapToDTO(bollard);
                BollardVOList.Add(objBollardVO);
            }

            return BollardVOList;
        }
        #endregion

        #region GetAllDivingRequests
        /// <summary>
        /// To Get AllDivingRequests
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<DivingRequestVO> GetAllDivingRequests(string portCode)
        {

            var portcode1 = new SqlParameter("@portcode", portCode);
            var DivingRequest = _unitOfWork.SqlQuery<DivingRequestVO>("dbo.usp_divingRequestGrid @portcode", portcode1).ToList();
            DivingRequest = DivingRequest.Where(s => s.FromPortCode == portCode).ToList();
            return DivingRequest;
        }
        #endregion

        #region GetDivingRequestOccupationById
        /// <summary>
        /// Gets all diving request occupation list
        /// </summary>
        /// <param name="portCode"></param>
        /// <param name="divingRequestId"></param>
        /// <returns></returns>
        public DivingRequestVO GetDivingRequestOccupationById(string portCode, int divingRequestId)
        {
            var portcode1 = new SqlParameter("@portcode", portCode);
            var DivingRequest = _unitOfWork.SqlQuery<DivingRequestVO>("dbo.usp_divingRequestOccupationGrid @portcode", portcode1).ToList();

            return DivingRequest.Find(e => e.DivingRequestID == divingRequestId);
        }
        #endregion

        #region GetAllDivingRequestOccupation
        /// <summary>
        /// Gets all diving request occupation list
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<DivingRequestVO> GetAllDivingRequestOccupation(string portCode)
        {
            var portcode1 = new SqlParameter("@portcode", portCode);
            var DivingRequest = _unitOfWork.SqlQuery<DivingRequestVO>("dbo.usp_divingRequestOccupationGrid @portcode", portcode1).ToList();
            DivingRequest = DivingRequest.Where(s => s.FromPortCode == portCode).ToList();

            return DivingRequest;
        

            //var DivingRequestOccupation = (from divingrequestvo in _unitOfWork.Repository<DivingRequest>().Query().Select()
            //                              join port in _unitOfWork.Repository<Port>().Query().Select() on divingrequestvo.FromPortCode equals port.PortCode
            //                               join bol in _unitOfWork.Repository<Bollard>().Query().Select() on divingrequestvo.FromBollardCode equals bol.BollardCode
            //                                join ber in _unitOfWork.Repository<Berth>().Query().Select() on divingrequestvo.FromBerthCode equals ber.BerthCode 
            //                              join qee in _unitOfWork.Repository<Quay>().Query().Select() on divingrequestvo.ToQuayCode equals qee.QuayCode
            //                               join loc in _unitOfWork.Repository<Location>().Query().Select() on loc.LocationID=divingrequestvo.OtherLocation

            //                               select new DivingRequestVO
            //                               {
            //                                   DivingRequestID = divingrequestvo.DivingRequestID,

            //                                   LocationType = divingrequestvo.LocationType,
            //                                   OtherLocation = (int)divingrequestvo.OtherLocation,
            //                                   QuayLocation = divingrequestvo.QuayLocation,
            //                                   //qees.QuayName,
            //                                   FromPortCode = divingrequestvo.FromPortCode,
            //                                   FromQuayCode = divingrequestvo.FromQuayCode,
            //                                   FromQuayName = qees.QuayName,
            //                                   FromBerthCode = divingrequestvo.FromBerthCode,
            //                                   FromBerthName = berths.BerthName,
            //                                   FromBollardCode = divingrequestvo.FromBollardCode,
            //                                   FromBollardName = bollards.BollardName,
            //                                   ToPortCode = divingrequestvo.ToPortCode,
            //                                   ToQuayCode = divingrequestvo.ToQuayCode,
            //                                   ToQuayName = qees.QuayName,
            //                                   ToBerthCode = divingrequestvo.ToBerthCode,
            //                                   ToBerthName = berths.BerthName,
            //                                   ToBollardCode = divingrequestvo.ToBollardCode,
            //                                   ToBollardName = bollards.BollardName,
            //                                   RequiredByDate = divingrequestvo.RequiredByDate,
            //                                   Remarks = divingrequestvo.Remarks,
            //                                   RecordStatus = divingrequestvo.RecordStatus,
            //                                   CreatedBy = divingrequestvo.CreatedBy,
            //                                   CreatedDate = divingrequestvo.CreatedDate,
            //                                   ModifiedBy = divingrequestvo.ModifiedBy,
            //                                   ModifiedDate = divingrequestvo.ModifiedDate,
            //                                   OcupationFromDate = Convert.ToString(divingrequestvo.OcupationFromDate),
            //                                   OcupationToDate = Convert.ToString(divingrequestvo.OcupationToDate),
            //                                   HoursOfOccupation1 = divingrequestvo.HoursOfOccupation1

            //                               });

            //return DivingRequestOccupation.ToList();
        }
        #endregion

        #region GetAllDivingTaskExecutions
        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 5th September 2014
        /// Purpose : To Get all diving task execution list by port code
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<DivingRequestVO> GetAllDivingTaskExecutions(string portCode)
        {
            var DivingTaskExecution = (from dr in _unitOfWork.Repository<DivingRequest>().Queryable()
                                          .Include(dr => dr.DivingOccupationApprovals)
                                          .Include(dr => dr.DivingRequestDivers)
                                          .Include(dr => dr.DivingCheckLists)
                                          .Include(dr => dr.DivingCheckLists.Select(dc => dc.DivingCheckListHazards))
                                          .Include(dr => dr.User)
                                          .Include(dr => dr.SubCategory)
                                          .Include(dr => dr.DivingOccupationApprovals.Select(w => w.WorkflowInstance))
                                          .Include(dr => dr.Location1)
                                          .Include(dr => dr.Location)
                                          .Include(dr => dr.Bollard)
                                          .Include(dr => dr.Bollard1)
                                          .Include(dr => dr.Bollard.Berth)
                                       where dr.FromPortCode == portCode && dr.RecordStatus == RecordStatus.Active
                                       && dr.DivingOccupationApprovals.Count > 0
                                       && dr.DivingOccupationApprovals.FirstOrDefault().WorkflowInstance.WorkflowTaskCode == "WFSA"
                                       select dr).OrderByDescending(dr=>dr.ModifiedDate).ToList();
            return DivingTaskExecution.MapToDTO();
        }
        #endregion

        #region GetDivingRequestByIDView
        /// <summary>
        /// Gets diving request details by diving request id
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public List<DivingRequestVO> GetDivingRequestByIdView(int requestId)
        {
            var divingRequestOccupation = (from divingrequestvo in _unitOfWork.Repository<DivingRequest>().Query().Select()

                                           join bol in _unitOfWork.Repository<Bollard>().Query().Select() on divingrequestvo.FromBollardCode equals bol.BollardCode into bollInfo
                                           from bollards in bollInfo.DefaultIfEmpty()
                                           join ber in _unitOfWork.Repository<Berth>().Query().Select() on bollards.BerthCode equals ber.BerthCode into berthInfo
                                           from berths in berthInfo.DefaultIfEmpty()
                                           join qee in _unitOfWork.Repository<Quay>().Query().Select() on berths.QuayCode equals qee.QuayCode into quayInfo
                                           from qees in quayInfo.DefaultIfEmpty()
                                           join port in _unitOfWork.Repository<Port>().Query().Select() on qees.PortCode equals port.PortCode into portInfo
                                           from ports in portInfo.DefaultIfEmpty()
                                           join usr in _unitOfWork.Repository<User>().Query().Select() on divingrequestvo.CreatedBy equals usr.UserID into userInfo
                                           from usrs in userInfo.DefaultIfEmpty()
                                           join emp in _unitOfWork.Repository<Employee>().Query().Select() on usrs.UserTypeID equals emp.EmployeeID into empInfo
                                           from emps in empInfo.DefaultIfEmpty()
                                           where divingrequestvo.DivingRequestID == requestId
                                           select new DivingRequestVO
                                           {
                                               DivingRequestID = divingrequestvo.DivingRequestID,
                                               DRN = divingrequestvo.DRN,
                                               OccupationReason = divingrequestvo.OccupationReason,
                                               LocationType = divingrequestvo.LocationType,
                                               OtherLocation = (int)divingrequestvo.OtherLocation,
                                               QuayLocation = divingrequestvo.QuayLocation,
                                               FromPortCode = divingrequestvo.FromPortCode,
                                               FromQuayCode = divingrequestvo.FromQuayCode,
                                               FromQuayName = qees.QuayName,
                                               FromBerthCode = divingrequestvo.FromBerthCode,
                                               FromBerthName = berths.BerthName,
                                               FromBollardCode = divingrequestvo.FromBollardCode,
                                               FromBollardName = bollards.BollardName,
                                               ToPortCode = divingrequestvo.ToPortCode,
                                               ToQuayCode = divingrequestvo.ToQuayCode,
                                               ToQuayName = qees.QuayName,
                                               ToBerthCode = divingrequestvo.ToBerthCode,
                                               ToBerthName = berths.BerthName,
                                               ToBollardCode = divingrequestvo.ToBollardCode,
                                               ToBollardName = bollards.BollardName,
                                               RequiredByDate = Convert.ToString(divingrequestvo.RequiredByDate, CultureInfo.InvariantCulture),
                                               Remarks = divingrequestvo.Remarks,
                                               RecordStatus = divingrequestvo.RecordStatus,
                                               CreatedBy = divingrequestvo.CreatedBy,
                                               CreatedDate = divingrequestvo.CreatedDate,
                                               ModifiedBy = divingrequestvo.ModifiedBy,
                                               ModifiedDate = divingrequestvo.ModifiedDate,
                                               OcupationFromDate = Convert.ToString(divingrequestvo.OcupationFromDate, CultureInfo.InvariantCulture),
                                               OcupationToDate = Convert.ToString(divingrequestvo.OcupationToDate, CultureInfo.InvariantCulture),
                                               HoursOfOccupation1 = divingrequestvo.HoursOfOccupation1,
                                               Port = ports.PortName,
                                               Raisedby = emps.FirstName + " " + emps.LastName,
                                               Berth = berths.BerthName,
                                               LocationorQuay = (divingrequestvo.LocationType == "O") ? divingrequestvo.OtherLocation.ToString() : divingrequestvo.QuayLocation.ToString(CultureInfo.InvariantCulture),

                                           });

            return divingRequestOccupation.ToList();

        }
        #endregion

        #region GetDivingRequestByID
        /// <summary>
        ///  Gets diving request by diving request id
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public DivingRequestVO GetDivingRequestById(int requestId)
        {
            var divingRequestOccupation = (from divingrequestvo in _unitOfWork.Repository<DivingRequest>().Query().Select()

                                           join bol in _unitOfWork.Repository<Bollard>().Query().Select() on divingrequestvo.FromBollardCode equals bol.BollardCode into bollInfo
                                           from bollards in bollInfo.DefaultIfEmpty()
                                           join ber in _unitOfWork.Repository<Berth>().Query().Select() on bollards.BerthCode equals ber.BerthCode into berthInfo
                                           from berths in berthInfo.DefaultIfEmpty()
                                           join qee in _unitOfWork.Repository<Quay>().Query().Select() on berths.QuayCode equals qee.QuayCode into quayInfo
                                           from qees in quayInfo.DefaultIfEmpty()
                                           join port in _unitOfWork.Repository<Port>().Query().Select() on qees.PortCode equals port.PortCode into portInfo

                                           from ports in portInfo.DefaultIfEmpty()
                                           join usr in _unitOfWork.Repository<User>().Query().Select() on divingrequestvo.CreatedBy equals usr.UserID into userInfo
                                           from usrs in userInfo.DefaultIfEmpty()
                                           join emp in _unitOfWork.Repository<Employee>().Query().Select() on usrs.UserTypeID equals emp.EmployeeID into empInfo
                                           from emps in empInfo.DefaultIfEmpty()
                                           where divingrequestvo.DivingRequestID == requestId
                                           select new DivingRequestVO
                       {
                           DivingRequestID = divingrequestvo.DivingRequestID,
                           DRN = divingrequestvo.DRN,
                           OccupationReason = divingrequestvo.OccupationReason,
                           LocationType = divingrequestvo.LocationType,
                           OtherLocation = (int)divingrequestvo.OtherLocation,
                           QuayLocation = divingrequestvo.QuayLocation,
                           FromPortCode = divingrequestvo.FromPortCode,
                           FromQuayCode = divingrequestvo.FromQuayCode,
                           FromQuayName = qees.QuayName,
                           FromBerthCode = divingrequestvo.FromBerthCode,
                           FromBerthName = berths.BerthName,
                           FromBollardCode = divingrequestvo.FromBollardCode,
                           FromBollardName = bollards.BollardName,
                           ToPortCode = divingrequestvo.ToPortCode,
                           ToQuayCode = divingrequestvo.ToQuayCode,
                           ToQuayName = qees.QuayName,
                           ToBerthCode = divingrequestvo.ToBerthCode,
                           ToBerthName = berths.BerthName,
                           ToBollardCode = divingrequestvo.ToBollardCode,
                           ToBollardName = bollards.BollardName,
                           RequiredByDate = Convert.ToString(divingrequestvo.RequiredByDate, CultureInfo.InvariantCulture),
                           Remarks = divingrequestvo.Remarks,
                           RecordStatus = divingrequestvo.RecordStatus,
                           CreatedBy = divingrequestvo.CreatedBy,
                           CreatedDate = divingrequestvo.CreatedDate,
                           ModifiedBy = divingrequestvo.ModifiedBy,
                           ModifiedDate = divingrequestvo.ModifiedDate,
                           OcupationFromDate = Convert.ToString(divingrequestvo.OcupationFromDate, CultureInfo.InvariantCulture),
                           OcupationToDate = Convert.ToString(divingrequestvo.OcupationToDate, CultureInfo.InvariantCulture),
                           HoursOfOccupation1 = divingrequestvo.HoursOfOccupation1,
                           Port = ports.PortName.ToString(),
                           Raisedby = emps.FirstName.ToString() + " " + emps.LastName.ToString(),
                           Berth = berths.BerthName.ToString(),
                           LocationorQuay = (divingrequestvo.LocationType == "O") ? divingrequestvo.OtherLocation.ToString() : divingrequestvo.QuayLocation.ToString()


                       }).FirstOrDefault<DivingRequestVO>();

            return divingRequestOccupation;

        }

        public DivingRequestVO GetDivingRequestByIdForNotification(string requestId)
        {
            var divingRequestOccupationVO = (from dr in _unitOfWork.Repository<DivingRequest>().Query().Tracking(true)
                                    .Include(dr => dr.DivingOccupationApprovals)
                                    .Include(dr => dr.User)
                                    .Include(dr => dr.Location1)
                                    .Include(dr => dr.Location)
                                    .Include(dr => dr.Bollard)
                                    .Include(dr => dr.Bollard1)
                                    .Include(dr => dr.Bollard.Berth)
                                    .Include(dr => dr.Bollard1.Berth)
                                    .Include(dr => dr.Bollard.Berth.Quay)
                                    .Include(dr => dr.Bollard1.Berth.Quay)
                                    .Select()
                                             where dr.DivingRequestID == int.Parse(requestId, CultureInfo.InvariantCulture)
                                             select new DivingRequestVO
                                                 {
                                                     DivingRequestID = dr.DivingRequestID,
                                                     FromPortCode = dr.FromPortCode ?? "NA",
                                                     FromBerthName = dr.FromBerthCode != null ? dr.Bollard.Berth.BerthName : "NA",
                                                     FromBollard = dr.FromBollardCode != null ? dr.Bollard.BollardName : "NA",
                                                     ToBollard = dr.ToBollardCode != null ? dr.Bollard1.BollardName : "NA",
                                                     RequiredByDate = Convert.ToString(dr.RequiredByDate, CultureInfo.InvariantCulture),
                                                     Remarks = dr.Remarks,
                                                     CreatedBy = dr.CreatedBy,
                                                     CreatedDate = dr.CreatedDate,
                                                     ModifiedBy = dr.ModifiedBy,
                                                     ModifiedDate = dr.ModifiedDate,
                                                     OcupationFromDate = Convert.ToString(dr.OcupationFromDate, CultureInfo.InvariantCulture),
                                                     OcupationToDate = Convert.ToString(dr.OcupationToDate, CultureInfo.InvariantCulture),
                                                     HoursOfOccupation = dr.HoursOfOccupation1,
                                                     Raisedby = dr.User.FirstName + " " + dr.User.LastName,
                                                     Berth = dr.FromBerthCode != null ? dr.Bollard.Berth.BerthName : "NA",
                                                     LocationorQuay = (dr.LocationType == "O") ? dr.Location1.LocationName : dr.Quay.QuayName
                                                 }).FirstOrDefault();
            return divingRequestOccupationVO;
        }

        public DivingRequestVO GetDivingRequestDetailsOnCompletion(string requestId)
        {
            var objDivingRequestVO = (from dr in _unitOfWork.Repository<DivingRequest>().Query().Tracking(true)
                                     .Include(dr => dr.DivingOccupationApprovals)
                                     .Include(dr => dr.User)
                                     .Include(dr => dr.Location1)
                                     .Include(dr => dr.Location)
                                     .Include(dr => dr.Bollard)
                                     .Include(dr => dr.Bollard1)
                                     .Include(dr => dr.Bollard.Berth)
                                     .Include(dr => dr.Bollard1.Berth)
                                     .Include(dr => dr.Bollard.Berth.Quay)
                                     .Include(dr => dr.Bollard1.Berth.Quay)
                                     .Select()
                                      join p in _unitOfWork.Repository<Port>().Query().Tracking(true).Select() on dr.FromPortCode equals p.PortCode
                                      join dc in _unitOfWork.Repository<DivingCheckList>().Query().Tracking(true).Select() on dr.DivingRequestID equals dc.DivingRequestID
                                      where dr.DivingRequestID == int.Parse(requestId, CultureInfo.InvariantCulture)
                                      select new DivingRequestVO
                                      {
                                          DRN = dr.DRN,
                                          DivingRequestID = dr.DivingRequestID,
                                          DivingSupervisorName = dc.DivingSupervisorName,
                                          StartTime = dr.StartTime.ToString(),
                                          StopTime = dr.StopTime.ToString(),
                                          PortCode = p.PortCode,
                                          PortName = p.PortName,
                                          CreatedBy = dr.CreatedBy,
                                          CreatedDate = dr.CreatedDate,
                                          ModifiedBy = dr.ModifiedBy,
                                          ModifiedDate = dr.ModifiedDate,
                                          LocationorQuay = (dr.LocationType == "O") ? dr.Location1.LocationName : dr.Quay.QuayName
                                      }).FirstOrDefault();
            return objDivingRequestVO;
        }
        #endregion

        #region GetAllLocations
        /// <summary>
        /// Gets all locations
        /// </summary>
        /// <returns></returns>
        public List<LocationVO> GetAllLocations(string portCode)
        {
            var locations = (from loc in _unitOfWork.Repository<Location>().Query().Select()
                             orderby loc.LocationName ascending
                             where loc.RecordStatus == "A" && loc.PortCode == portCode
                             orderby loc.LocationName ascending
                             select new LocationVO
                             {
                                 LocationID = loc.LocationID,
                                 LocationName = loc.LocationName,
                                 PortCode = loc.PortCode

                             }).ToList();
           
            var others = locations.Find(o => o.LocationName.ToLower() == "others"); 
            locations = locations.FindAll(o => o.LocationName.ToLower() != "others");

            if (others != null)
            {
                locations.Add(others);
            }

            return locations;
        }
        #endregion

        #region GetDivingrequestsForScroll
        /// <summary>
        /// To get Diving requests for scrolling : By Mahesh
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<DivingRequestVO> GetDivingRequestsForScroll(string portCode)
        {
            var portcode1 = new SqlParameter("@portcode", portCode);
            var DivingRequest = _unitOfWork.SqlQuery<DivingRequestVO>("dbo.usp_GetDivingreuestsForScroll @portcode", portcode1).ToList();
            // DivingRequest = DivingRequest.Where(s => s.FromPortCode == portcode).ToList();
            return DivingRequest;
        }
        #endregion

        #region GenerateDRN
        /// <summary>
        /// function to auto generate DRN Number
        /// </summary>
        /// <returns></returns>
        public string GenerateDRN()
        {
            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;
            int Day = DateTime.Now.Day;

            StringBuilder DRN = new StringBuilder();

            int count = (from a in _unitOfWork.Repository<DivingRequest>().Query().Select()
                         select a).Count();

            count = count + 1;
            string strCount = count.ToString(CultureInfo.InvariantCulture);
            string stMonth = Month.ToString(CultureInfo.InvariantCulture);
            string stDay = Day.ToString(CultureInfo.InvariantCulture);

            if (count <= 9)
                strCount = "0" + count.ToString(CultureInfo.InvariantCulture);

            if (Month <= 9)
                stMonth = "0" + Month.ToString(CultureInfo.InvariantCulture);
            if (Day <= 9)
                stDay = "0" + Day.ToString(CultureInfo.InvariantCulture);

            DRN.Append("DRN" + Year.ToString(CultureInfo.InvariantCulture) + stMonth + stDay + count.ToString(CultureInfo.InvariantCulture));

            return DRN.ToString();
        }
        #endregion

        #region AddDivingRequest
        /// <summary>
        /// Add / Inserts the new Diving Request
        /// </summary>
        /// <param name="divingRequest"></param>
        /// <returns></returns>
        public DivingRequestVO AddDivingRequest(DivingRequestVO divingRequest, int userId, string portCode)
        {
            DivingRequest obj = new DivingRequest();
            if (divingRequest != null)
            {
                if (divingRequest.LocationType == "Q")
                {
                    divingRequest.DRN = GenerateDRN();
                    divingRequest.FromPortCode = portCode;
                    divingRequest.ToPortCode = portCode;
                    divingRequest.RecordStatus = "A";
                    divingRequest.CreatedBy = userId;
                    divingRequest.CreatedDate = DateTime.Now;
                    divingRequest.ModifiedBy = userId;
                    divingRequest.ModifiedDate = DateTime.Now;
                    obj = divingRequest.MapToEntity();
                    obj.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<DivingRequest>().Insert(obj);
                    _unitOfWork.SaveChanges();
                    divingRequest = obj.MapToDTO();
                }
                else
                {
                    divingRequest.DRN = GenerateDRN();
                    divingRequest.FromPortCode = portCode;
                    divingRequest.ToPortCode = null;
                    divingRequest.FromQuayCode = null;
                    divingRequest.ToQuayCode = null;
                    divingRequest.FromBerthCode = null;
                    divingRequest.ToBerthCode = null;
                    divingRequest.FromBollardCode = null;
                    divingRequest.ToBollardCode = null;
                    divingRequest.QuayLocation = null;
                    divingRequest.RecordStatus = "A";
                    divingRequest.CreatedBy = userId;
                    divingRequest.CreatedDate = DateTime.Now;
                    divingRequest.ModifiedBy = userId;
                    divingRequest.ModifiedDate = DateTime.Now;
                    obj = divingRequest.MapToEntity();
                    obj.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<DivingRequest>().Insert(obj);
                    _unitOfWork.SaveChanges();
                    divingRequest = obj.MapToDTO();
                }
            }

            return divingRequest;
        }
        #endregion

        #region ModifyDivingCheckList
        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 5th September 2014
        /// Purpose : To Modify/Update Diving Checklist Data 
        /// </summary>
        /// <param name="divingRequestVO"></param>
        /// <returns></returns>
        public DivingRequestVO ModifyDivingChecklist(DivingRequestVO divingRequestVO, int userId)
        {
            DivingRequest divingrequest = new DivingRequest();
            divingrequest = DivingRequestMapExtension.MapToEntity(divingRequestVO);
            DivingCheckList divingchecklist = new DivingCheckList();
            List<DivingCheckListHazard> lstDivingCheckListHazard = new List<DivingCheckListHazard>();

            if (divingRequestVO != null)
            {
                divingchecklist = DivingCheckListMapExtension.MapToEntity(divingRequestVO.DivingCheckList);
                lstDivingCheckListHazard = divingRequestVO.DivingCheckList.DivingCheckListHazard.MapToEntity();
            }

            divingchecklist.RecordStatus = "A";

            if (divingchecklist.DivingCheckListID != 0)
            {
                divingchecklist.ModifiedBy = userId; //UserID;
                divingchecklist.ModifiedDate = divingrequest.ModifiedDate;
                divingchecklist.ObjectState = ObjectState.Modified;

                _unitOfWork.Repository<DivingCheckList>().Update(divingchecklist);
            }
            else
            {
                divingchecklist.DivingRequestID = divingrequest.DivingRequestID;
                divingchecklist.CreatedBy = userId;
                divingchecklist.CreatedDate = divingrequest.ModifiedDate;
                divingchecklist.ModifiedBy = userId;
                divingchecklist.ModifiedDate = divingrequest.ModifiedDate;

                divingchecklist.ObjectState = ObjectState.Added;

                _unitOfWork.Repository<DivingCheckList>().Insert(divingchecklist);
                _unitOfWork.SaveChanges();
            }

            List<DivingCheckListHazard> divingCheckListHazard = _unitOfWork.Repository<DivingCheckListHazard>().Queryable()
                .Where(e => e.DivingCheckListID == divingchecklist.DivingCheckListID).ToList();

            if (divingCheckListHazard.Count > 0)
            {
                foreach (DivingCheckListHazard divingchecklisthazard in divingCheckListHazard)
                {
                    _unitOfWork.Repository<DivingCheckListHazard>().Delete(divingchecklisthazard);
                }
                _unitOfWork.SaveChanges();
            }

            if (lstDivingCheckListHazard.Count > 0)
            {
                foreach (DivingCheckListHazard divingchecklisthazard in lstDivingCheckListHazard)
                {
                    divingchecklisthazard.DivingCheckListID = divingchecklist.DivingCheckListID;
                    divingchecklisthazard.RecordStatus = "A";
                    divingchecklisthazard.CreatedBy = userId;
                    divingchecklisthazard.CreatedDate = divingchecklist.ModifiedDate;
                    divingchecklisthazard.ModifiedBy = userId;
                    divingchecklisthazard.ModifiedDate = divingchecklist.ModifiedDate;

                    _unitOfWork.Repository<DivingCheckListHazard>().Insert(divingchecklisthazard);
                }
            }

            _unitOfWork.ExecuteSqlCommand("update DivingRequest set ModifiedBy = @p0, ModifiedDate = @p1 where DivingRequestID = @p2", userId, DateTime.Now, divingrequest.DivingRequestID);

            _unitOfWork.SaveChanges();
            return divingRequestVO;
        }
        #endregion

        #region ModifyDivingTaskExecution
        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 5th September 2014
        /// Purpose : To  Modify/Update Diving Task Execution Data
        /// </summary>
        /// <param name="divingRequestVO"></param>
        /// <returns></returns>
        public DivingRequestVO ModifyDivingTaskExecution(DivingRequestVO divingRequestVO, int userId)
        {
            if (divingRequestVO != null)
            {
                if (string.IsNullOrEmpty(divingRequestVO.StartTime) || string.IsNullOrWhiteSpace(divingRequestVO.StartTime))
                {
                    divingRequestVO.StartTime = "";
                }

                if (string.IsNullOrEmpty(divingRequestVO.StopTime) || string.IsNullOrWhiteSpace(divingRequestVO.StopTime))
                {
                    divingRequestVO.StopTime = "";
                }
            }

            DivingRequest divingrequest = new DivingRequest();
            divingrequest = DivingRequestMapExtension.MapToEntity(divingRequestVO);

            List<DivingRequestDiver> lstdivingrequestdiver = new List<DivingRequestDiver>();
            if (divingRequestVO != null)
            {
                if (divingRequestVO.DivingRequestDivers1.Count > 0)
                {
                    foreach (DivingRequestDiverVO divervo in divingRequestVO.DivingRequestDivers1)
                    {
                        lstdivingrequestdiver.Add(divervo.MapToEntity1("New"));
                    }
                }
            }

            if (divingRequestVO != null)
            {
                if (divingRequestVO.DivingRequestDivers2.Count > 0)
                {
                    foreach (DivingRequestDiverVO divervo in divingRequestVO.DivingRequestDivers2)
                    {
                        lstdivingrequestdiver.Add(divervo.MapToEntity1("StandBy"));
                    }
                }
            }

            if (divingRequestVO != null)
            {
                if (divingRequestVO.DivingRequestDivers3.Count > 0)
                {
                    foreach (DivingRequestDiverVO divervo in divingRequestVO.DivingRequestDivers3)
                    {
                        lstdivingrequestdiver.Add(divervo.MapToEntity1("Add"));
                    }
                }
            }

            divingrequest.RecordStatus = "A";

            divingrequest.ModifiedBy = userId;
            divingrequest.ObjectState = ObjectState.Modified;

            _unitOfWork.Repository<DivingRequest>().Update(divingrequest);

            List<DivingRequestDiver> DivingRequestDiver = _unitOfWork.Repository<DivingRequestDiver>().Queryable().Where(dr => dr.DivingRequestID == divingrequest.DivingRequestID).ToList();

            if (DivingRequestDiver.Count > 0)
            {
                foreach (DivingRequestDiver diver in DivingRequestDiver)
                {
                    _unitOfWork.Repository<DivingRequestDiver>().Delete(diver);
                }
                _unitOfWork.SaveChanges();
            }

            if (lstdivingrequestdiver.Count > 0)
            {
                foreach (DivingRequestDiver diver in lstdivingrequestdiver)
                {
                    diver.DivingRequestID = divingrequest.DivingRequestID;
                    diver.CreatedBy = userId;
                    diver.CreatedDate = divingrequest.ModifiedDate;
                    diver.ModifiedBy = userId;
                    diver.ModifiedDate = divingrequest.ModifiedDate;
                    diver.RecordStatus = "A";
                    diver.ObjectState = ObjectState.Added;

                    _unitOfWork.Repository<DivingRequestDiver>().Insert(diver);
                }
            }

            _unitOfWork.ExecuteSqlCommand("update DivingRequest set ModifiedBy = @p0, ModifiedDate = @p1 where DivingRequestID = @p2", userId, DateTime.Now, divingrequest.DivingRequestID);
            _unitOfWork.SaveChanges();

            return divingRequestVO;
        }
        #endregion
    }
}
