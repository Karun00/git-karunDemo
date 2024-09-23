using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Web.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IVesselETAChangeService : IDisposable
    {
        /// <summary>
        /// To Get VCN Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VesselETAChangeVO> GetArrivalVcns();

        /// <summary>
        /// To Get Vessel Information By VCN
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
        [OperationContract]
        VesselETAChangeVO GetVesselInfoByVcns(string vcn);

        /// <summary>
        /// To Add Change ETA Data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        VesselETAChangeVO PostVesselEtaChange(VesselETAChangeVO obj);

        /// <summary>
        /// To Get Change ETA Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<VesselETAChangeVO> ChangeEtaDetails(string vcn, string vesselName, string etaFrom, string etaTo, string agentNameSearch);

        /// <summary>
        /// To Get Change ETA Details by vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="vcnvesselEatChangeId"></param>
        /// <returns></returns>
        [OperationContract]
        List<VesselETAChangeVO> ChangezEtaDetails(string vcn, int? vesselEatChangeId);

        /// <summary>
        /// To Get Change ETA Details Asynchronously
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        //[OperationContract]
        //Task<List<VesselETAChangeVO>> ChangeETADetailsAsync();
    }
}
