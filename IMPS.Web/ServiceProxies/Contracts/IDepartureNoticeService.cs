using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IDepartureNoticeService : IDisposable
    {
        [OperationContract]
        List<DepartureNoticeVO> AddDepartureNotice(DepartureNoticeVO servicedata);

        [OperationContract]
        List<DepartureNoticeVO> ModifyDepartureNotice(DepartureNoticeVO servicedata);

        [OperationContract]
        List<DepartureNoticeVO> GetPendingArrivalNotifications(string DepartureID, string VCN, string VesselName, string SubmissionDateFrom, string SubmissionDateTO);
        
        //[OperationContract]
        //Task<List<DepartureNoticeVO>> AddDepartureNoticeAsync(DepartureNoticeVO servicedata);

        //[OperationContract]
        //Task<List<DepartureNoticeVO>> ModifyDepartureNoticeAsync(DepartureNoticeVO servicedata);
        [OperationContract]
        List<RevenuePostingVO> DepartureNoticeVcnDetailsforAutocomplete(string searchvalue);

        [OperationContract]
        List<VesselVO> DepartureNoticeVesselDetailsforAutocomplete(string searchvalue);

        #region Workflow Integrated Methods
        [OperationContract]
        void AcknowledgeDepartureNotice(string DepartureNoticeID, string comments, string taskcode);
        #endregion
    }
}
