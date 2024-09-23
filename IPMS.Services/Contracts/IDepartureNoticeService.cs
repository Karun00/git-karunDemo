using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IDepartureNoticeService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DepartureNoticeVO AddDepartureNotice(DepartureNoticeVO servicedata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        DepartureNoticeVO ModifyDepartureNotice(DepartureNoticeVO servicedata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<DepartureNoticeVO> GetPendingArrivalNotifications(string DepartureID, string VCN, string VesselName, string SubmissionDateFrom, string SubmissionDateTO);
        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenuePostingVO> DepartureNoticeVcnDetailsforAutocomplete(string searchvalue);


        /// <summary>
        ///  /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselVO> DepartureNoticeVesselDetailsforAutocomplete(string searchvalue);

        #region Workflow Integrated Methods
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void AcknowledgeDepartureNotice(string DepartureNoticeID, string comments, string taskcode);
        #endregion
    }
}
