using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using log4net;
using log4net.Config;
using System.Globalization;
namespace IPMS.Repository
{
    public class DepartureNoticeRepository : IDepartureNoticeRepository
    {
        private IUnitOfWork _unitOfWork;
        // private IAccountRepository accountRepository;
        private IUserRepository _userRepository;
        //private readonly ILog log;

        public DepartureNoticeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            //log = LogManager.GetLogger(typeof(DepartureNoticeRepository));
            // accountRepository = new AccountRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
        }

        #region GetTerminalOperatorForUser
        public UserMasterVO GetTerminalOperatorForUser(int userId, string portCode)
        {
            var userdetails = (from user in _unitOfWork.Repository<User>().Queryable()
                               join userpt in _unitOfWork.Repository<UserPort>().Queryable()
                               on user.UserID equals userpt.UserID
                               where userpt.UserID == userId && userpt.PortCode == portCode
                               select new UserMasterVO
                               {
                                   UserID = user.UserID,
                                   UserType = user.UserType,
                                   UserTypeID = user.UserTypeID
                               }).FirstOrDefault<UserMasterVO>();

            return userdetails;
        }
        #endregion

        #region GetPendingArrivalNotifications
        /// <summary>
        /// Get Pending ArrivalNotifications for Acknowledgement by AgentID and Portcode
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="portCode"></param>
        /// <param name="departureId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<DepartureNoticeVO> GetPendingArrivalNotifications(int? agentId, string portCode, string departureId, int? userId)
        {
            int? DepartureIDvalue = 0;
            if (!string.IsNullOrWhiteSpace(departureId))
            {
                DepartureIDvalue = int.Parse(departureId, CultureInfo.InvariantCulture);
            }

            List<DepartureNoticeVO> result = new List<DepartureNoticeVO>();
            string ConfigureName = ConfigName.DateFormat;
            var _AgentID = new SqlParameter("@AgentID", agentId);
            var _PortCode = new SqlParameter("@PortCode", portCode);
            var _ConfigName = new SqlParameter("@ConfigName", ConfigureName);
            var _DepartureID = new SqlParameter("@DepartureID", DepartureIDvalue);
            var _UserID = new SqlParameter("@UserID", userId);

            var Request = _unitOfWork.SqlQuery<DepartureNoticeVO>("usp_GetPendingArrivalNotifications @AgentID, @PortCode, @ConfigName,@DepartureID,@UserID",
                _AgentID, _PortCode, _ConfigName, _DepartureID, _UserID);

            result = Request.ToList();

            //Convert date-string for the solution of datetime error(5 hours late)
            foreach (var item in result)
            {
                if (item.EstimatedDatetimeOfSR != null)
                {
                    item.EstimatedDatetimeOfSRConverted = string.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm}", item.EstimatedDatetimeOfSR);
                }
            }

            return result;
        }
        #endregion

        #region GetPendingArrivalNotifications

        /// <summary>
        /// Get Pending ArrivalNotifications for Acknowledgement by AgentID and Portcode
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="portCode"></param>
        /// <param name="departureId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<DepartureNoticeVO> GetPendingArrivalNotifications(int? agentId, string portCode, string departureId,
            int? userId, string vcn, string vesselName, string submissionDateFrom, string submissionDateTo)
        {
            int? DepartureIDvalue = 0;
            if (!string.IsNullOrWhiteSpace(departureId))
            {
                DepartureIDvalue = int.Parse(departureId, CultureInfo.InvariantCulture);
            }

            List<DepartureNoticeVO> result = new List<DepartureNoticeVO>();
            string ConfigureName = ConfigName.DateFormat;
            var _AgentID = new SqlParameter("@AgentID", agentId);
            var _PortCode = new SqlParameter("@PortCode", portCode);
            var _ConfigName = new SqlParameter("@ConfigName", ConfigureName);
            var _DepartureID = new SqlParameter("@DepartureID", DepartureIDvalue);
            var _UserID = new SqlParameter("@UserID", userId);

            var Request =
                _unitOfWork.SqlQuery<DepartureNoticeVO>(
                    "usp_GetPendingArrivalNotifications @AgentID, @PortCode, @ConfigName,@DepartureID,@UserID",
                    _AgentID, _PortCode, _ConfigName, _DepartureID, _UserID);

            result = Request.ToList();

            //Convert date-string for the solution of datetime error(5 hours late)
            foreach (var item in result)
            {
                if (item.EstimatedDatetimeOfSR != null)
                {
                    item.EstimatedDatetimeOfSRConverted = string.Format(CultureInfo.InvariantCulture,
                        "{0:yyyy-MM-dd HH:mm}", item.EstimatedDatetimeOfSR);
                }
            }
            // if ((!string.IsNullOrWhiteSpace(vcn)) && (!string.IsNullOrWhiteSpace(vesselName)))
            // {
            string[] vesselNames = vesselName.Split('-');
            vesselName = vesselNames[0].ToString().Trim();
            if ((vcn != "NA") && (vesselName != "NA"))
            {
                result = result.FindAll(t => t.VCN.ToUpperInvariant().Contains(vcn.ToUpperInvariant()) && t.VesselName.ToUpperInvariant().Contains(vesselName.ToUpperInvariant()));
            }
            else if ((vcn != "NA") && (vesselName == "NA"))
            {
                result = result.FindAll(t => t.VCN.ToUpperInvariant().Contains(vcn.ToUpperInvariant()));
            }
            else if ((vcn == "NA") && (vesselName != "NA"))
            {
                result = result.FindAll(t => t.VesselName.ToUpperInvariant().Contains(vesselName.ToUpperInvariant()));
            }
            else if (!string.IsNullOrWhiteSpace(submissionDateFrom) && !string.IsNullOrWhiteSpace(submissionDateTo))
            {
                result = result.FindAll(t => (Convert.ToDateTime(t.SubmissionDate, CultureInfo.InvariantCulture).Date >= Convert.ToDateTime(submissionDateFrom, CultureInfo.InvariantCulture).Date && Convert.ToDateTime(t.SubmissionDate, CultureInfo.InvariantCulture).Date < Convert.ToDateTime(submissionDateTo, CultureInfo.InvariantCulture).AddDays(1).Date));
            }
            // }


            return result;
        }
        #endregion

        #region GetDepartureNoticeByID
        /// <summary>
        /// To Get DepartureNotice details based DepartureNoticeID
        /// </summary>
        /// <param name="DepartureID"></param>
        /// <returns></returns>
        public DepartureNoticeVO GetDepartureNoticeById(string DepartureID)
        {
            int DepartureNoticeId = Int32.Parse(DepartureID, CultureInfo.InvariantCulture);

            var DepartureNoticeDetails = (from a in _unitOfWork.Repository<DepartureNotice>().Query().Select()
                                          where a.DepartureID == DepartureNoticeId
                                          select a).FirstOrDefault().MapToDto();

            var _AgentID = DepartureNoticeDetails.AgentID;
            var _PortCode = DepartureNoticeDetails.PortCode;
            var _UserID = _userRepository.GetUserDetailsForRoleAndPortCodeByUserType(_PortCode, UserType.Agent, "", _AgentID).FirstOrDefault().UserID;

            var result = GetPendingArrivalNotifications(_AgentID, _PortCode, DepartureID, _UserID).FirstOrDefault();
            return result;
        }
        #endregion

        #region GetDepartureNoticeDetailsForWorkFlow
        /// <summary>
        /// To Get Departure Notice Details For WorkFlow approve/confirm/cancel
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="portCode"></param>
        /// <param name="departureNoticeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DepartureNoticeVO GetDepartureNoticeDetailsForWorkFlow(int? agentId, string portCode, string departureNoticeId, int? userId)
        {
            DepartureNoticeVO servicedtls = new DepartureNoticeVO();
            int? DepartureIDvalue = 0;
            if (!string.IsNullOrWhiteSpace(departureNoticeId))
            {
                DepartureIDvalue = int.Parse(departureNoticeId, CultureInfo.InvariantCulture);
            }
            string ConfigureName = ConfigName.DateFormat;
            var _AgentID = new SqlParameter("@AgentID", agentId);
            var _PortCode = new SqlParameter("@PortCode", portCode);
            var _ConfigName = new SqlParameter("@ConfigName", ConfigureName);
            var _DepartureID = new SqlParameter("@DepartureID", DepartureIDvalue);
            var _UserID = new SqlParameter("@UserID", userId);

            var Request = _unitOfWork.SqlQuery<DepartureNoticeVO>("usp_GetPendingArrivalNotifications @AgentID, @PortCode, @ConfigName,@DepartureID,@UserID",
                _AgentID, _PortCode, _ConfigName, _DepartureID, _UserID).FirstOrDefault();

            servicedtls = Request;
            return servicedtls;
        }
        #endregion

        #region GetDepartureNoticeServiceRequest
        /// <summary>
        /// Get Departure Notice details for ServiceRequest 
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
        public DepartureNoticeVO GetDepartureNoticeServiceRequest(string VCN)
        {
            //var DepartureDetails = (from a in _unitOfWork.Repository<DepartureNotice>().Query().Select()
            //                        where a.VCN == VCN && a.IsFinal == "Y"
            //                        select a).ToList().MapToDto();

            var DepartureDetails = (from a in _unitOfWork.Repository<DepartureNotice>().Queryable().Where(a => a.VCN == VCN && a.IsFinal == "Y")                                    
                                    select a).ToList();

            var List = DepartureDetails.MapToDto();

            var result = List.FirstOrDefault();

            return result;
        }
        #endregion

        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<RevenuePostingVO> DepartureNoticeVcnDetailsforAutocomplete(int? agentId, string portCode, int? userId, string searchValue)
        {

            
            var _AgentID = new SqlParameter("@AgentID", agentId);
            var _PortCode = new SqlParameter("@p_PortCode", portCode);
           // var _UserID = new SqlParameter("@UserID", userId);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            var vcndtls = _unitOfWork.SqlQuery<RevenuePostingVO>("dbo.usp_GetDepartureNoticeVCNSearch @p_SearchText, @p_PortCode,@AgentID ", Searchvalue, _PortCode, _AgentID).ToList();

            //List<RevenuePostingVO> vcnlistVO = new List<RevenuePostingVO>();
            //foreach (var an in vcndtls)
            //{
            //    RevenuePostingVO vcnlist = new RevenuePostingVO();
            //    vcnlist.vcn = an.VCN;
            //    vcnlistVO.Add(vcnlist);
            //}
            return vcndtls;

        }
        /// <summary>
        ///  /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<VesselVO> DepartureNoticeVesselDetailsforAutocomplete(int? agentId, string portCode, int? userId, string searchValue)
        {

            var _AgentID = new SqlParameter("@AgentID", agentId);
            var _PortCode = new SqlParameter("@p_PortCode", portCode);
          //  var _UserID = new SqlParameter("@UserID", userId);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            // var userId = new SqlParameter("@p_userid", userid);

            var _VesselInfo = _unitOfWork.SqlQuery<VesselVO>("dbo.usp_GetDepartureNoticeVesselSearch @p_SearchText, @p_PortCode,@AgentID ", Searchvalue, _PortCode, _AgentID).ToList();


            return _VesselInfo;
        }
    }
}
