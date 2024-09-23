using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using IPMS.Domain;

namespace IPMS.Repository
{
    public class VoyageMonitoringRepository : IVoyageMonitoringRepository
    {
        private IUnitOfWork _unitOfWork;

        public VoyageMonitoringRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetVesselDetails
        /// <summary>
        /// Get Vessel Details
        /// </summary>
        /// <param name="VesselID"></param>
        /// <returns></returns>
        public Vessel GetVesselDetails(int vesselId)
        {
            var objVesselDetails = (from v in _unitOfWork.Repository<Vessel>().Query().Select()
                                    where v.VesselID == vesselId
                                    select v).FirstOrDefault<Vessel>();
            return objVesselDetails;
        }
        #endregion

        #region GetVCNDetailsVoyage
        /// <summary>
        /// Get VCN Details Voyage
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<VoyageMonitoringVO> GetVcnDetailsVoyage(string portCode, string searchValue, int agentId=0)
        {
            var portcode = new SqlParameter("@p_PortCode", portCode);
            var searchtxt = new SqlParameter("@p_SearchText", searchValue);
            var agentid = new SqlParameter("@p_agentId", agentId);

            var vcndtls = _unitOfWork.SqlQuery<VoyageMonitoringVO>("dbo.[usp_GetVoyageMonitoringDtls] @p_SearchText ,@p_PortCode,@p_agentId", searchtxt, portcode, agentid).ToList();

            return vcndtls;
        }
        #endregion

        #region GetVCNDetailsVoyage_VCN
        /// <summary>
        /// Get VCN Details for Voyage by VCN
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<VoyageMonitoringVO> GetVcnDetailsVoyageVcn(string vcn, string portCode)
        {
            var vcndtls = _unitOfWork.SqlQuery<VoyageMonitoringVO>("dbo.[usp_VoyageMonitoringDtls]").ToList();
            vcndtls = vcndtls.Where(i => i.portcode == portCode).ToList();
            vcndtls = vcndtls.Where(i => i.VCN == vcn).ToList();
            return vcndtls;
        }
        #endregion

        #region GetServiceRequestDetails
        /// <summary>
        /// To get Service Request Grid details in Voyage Monitoring
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<VoyageMonitoringVO> GetServiceRequestDetails(string vcn)
        {
            var vcn1 = new SqlParameter("@VCN", string.IsNullOrWhiteSpace(vcn) ? (object)DBNull.Value : vcn);
            var applicant = _unitOfWork.SqlQuery<VoyageMonitoringVO>("dbo.[usp_Voyage_ServiceRequest_Dtls] @VCN", vcn1).ToList();

            return applicant;
        }
        #endregion

        #region GetChangeAtaAndAtdDetails
        /// <summary>
        /// Get Change of ATA and ATD Details
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<VoyageMonitoringVO> GetChangeAtaAndAtdDetails(string vcn)
        {
            var applicant = from a in _unitOfWork.Repository<VesselETAChange>().Queryable().AsEnumerable<VesselETAChange>()
                            where a.VCN == vcn
                            select new VoyageMonitoringVO
                            {
                                VCN = a.VCN,
                                NewATA = a.ETA,
                                NewATD = a.ETD,
                                ChangeReason = a.Remarks,
                                VesselETAChangeID = a.VesselETAChangeID
                            };

            return applicant.OrderByDescending(x=>x.VesselETAChangeID).ToList<VoyageMonitoringVO>();
        }
        #endregion

        #region GetAnchorageDetails
        /// <summary>
        /// Get Anchorage Details
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<VoyageMonitoringVO> GetAnchorageDetails(string vcn)
        {
            List<VoyageMonitoringVO> result = new List<VoyageMonitoringVO>();
            var applicant = (from a in _unitOfWork.Repository<VesselCallAnchorage>().Queryable().AsEnumerable<VesselCallAnchorage>()
                             where a.VCN == vcn && a.RecordStatus == RecordStatus.Active 
                             orderby a.VesselCallAnchorageID descending
                             select new VoyageMonitoringVO
                             {
                                 VCN = a.VCN,
                                 PortLimitEnterTime = a.CreatedDate,
                                 AnchorageDownTime = a.AnchorDropTime,
                                 AnchorageUpTime = a.AnchorAweighTime,
                                 BreakWaterInTime = a.BearingDistanceFromBreakWater
                             }).ToList();
            result = applicant;
            return result;
        }
        #endregion
        public List<VoyageMonitoringVO> GetPortAndBreakLimitDetails(string vcn)
        {

            var result = (from vc in _unitOfWork.Repository<VesselCall>().Queryable()
                             where vc.VCN == vcn
                             select new VoyageMonitoringVO
                             {
                                 VCN = vc.VCN,
                                 PortLimitIn=vc.PortLimitIn,
                                 PortLimitOut=vc.PortLimitOut,
                                 BreakWaterIn=vc.BreakWaterIn,
                                 BreakWaterOut=vc.BreakWaterOut
                                 
                             }).ToList();
            return result;
        }
        public List<VoyageMonitoringVO> GetBerthDetails(string vcn, string portCode)
        {
            var result = _unitOfWork.SqlQuery<VoyageMonitoringVO>("select SUB.SubCatName AS MovementName,B.BerthName AS BerthName, VM.ATB,VM.ATUB from VesselCallMovement vm left join Berth B on b.BerthCode =vm.FromPositionBerthCode and b.QuayCode = vm.FromPositionQuayCode and vm.FromPositionPortCode= @p0 INNER JOIN SubCategory SUB ON SUB.SubCatCode = VM.MovementType where VCN = @p1", portCode, vcn).ToList();
            return result;
        }
        public UserMasterVO GetUserDetails(int UserID, string PortCode)
        {
            var userdetails = (from user in _unitOfWork.Repository<User>().Queryable()
                               join userpt in _unitOfWork.Repository<UserPort>().Queryable()
                               on user.UserID equals userpt.UserID
                               where userpt.UserID == UserID && userpt.PortCode == PortCode
                               select new UserMasterVO
                               {
                                   UserID = user.UserID,
                                   UserType = user.UserType,
                                   UserTypeID = user.UserTypeID
                               }).FirstOrDefault<UserMasterVO>();
            return userdetails;
        }
    }
}
