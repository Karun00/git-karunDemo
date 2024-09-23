using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    public class DepartureNoticeClient : UserClientBase<IDepartureNoticeService>, IDepartureNoticeService
    {
        public List<DepartureNoticeVO> GetPendingArrivalNotifications(string DepartureID, string VCN, string VesselName, string SubmissionDateFrom, string SubmissionDateTO)
        {
            return WrapOperationWithException(() => Channel.GetPendingArrivalNotifications(DepartureID, VCN, VesselName, SubmissionDateFrom, SubmissionDateTO));
        }
        public List<DepartureNoticeVO> AddDepartureNotice(DepartureNoticeVO servicedata)
        {
            return WrapOperationWithException(() => Channel.AddDepartureNotice(servicedata));
        }
        public List<DepartureNoticeVO> ModifyDepartureNotice(DepartureNoticeVO servicedata)
        {
            return WrapOperationWithException(() => Channel.ModifyDepartureNotice(servicedata));
        }
        //public Task<List<DepartureNoticeVO>> ModifyDepartureNoticeAsync(DepartureNoticeVO servicedata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyDepartureNoticeAsync(servicedata));
        //}
        //public Task<List<DepartureNoticeVO>> AddDepartureNoticeAsync(DepartureNoticeVO servicedata)
        //{
        //    return WrapOperationWithException(() => Channel.AddDepartureNoticeAsync(servicedata));
        //}
        public List<RevenuePostingVO> DepartureNoticeVcnDetailsforAutocomplete(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.DepartureNoticeVcnDetailsforAutocomplete(searchvalue));
        }

        public List<VesselVO> DepartureNoticeVesselDetailsforAutocomplete(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.DepartureNoticeVesselDetailsforAutocomplete(searchvalue));
        }
        #region Workflow Integrated Methods
        public void AcknowledgeDepartureNotice(string DepartureNoticeID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.AcknowledgeDepartureNotice(DepartureNoticeID, comments, taskcode));
        }
        #endregion
    }
}