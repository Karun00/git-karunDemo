using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IVesselCallAnchorageService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselCallVO> GetAnchorageRecordingList(string vcn, string vesselName, string etaFrom, string etaTo);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselCallVO> GetzAnchorageRecordingList(string vcn);

        //[OperationContract]
        //[FaultContract(typeof(Exception))]
        //VesselCallAnchorageVO PostVesselCallAnchorageData(VesselCallAnchorageVO vesselcallanchoragedata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetReasons();


        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetGeneralConfigs();


        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselCallVO ModifyVesselCallAnchorageData(VesselCallVO vesselCallAnchorageData);

        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<RevenuePostingVO> VesselCallVcnDetailsforAutocomplete(string searchvalue);


        /// <summary>
        ///  /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselVO> VesselCallVesselDetailsforAutocomplete(string searchvalue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void VesselCallAnchorageNotification(int vesselCallAnchorageId, string portCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void VesselCallNotification(int vesselCallAnchorageId, string portCode, string vcn);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        VcnCloseVO VcnClose(string vcn);
        
    }
}
