using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IVesselETAChangeService
    {
        /// <summary>
        /// To Get VCN Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselETAChangeVO> GetArrivalVcns();

        /// <summary>
        /// To Get Vessel Information By VCN
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselETAChangeVO GetVesselInfoByVcns(string vcn);

        /// <summary>
        /// To Add Change ETA Data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselETAChangeVO PostVesselEtaChange(VesselETAChangeVO obj);

        /// <summary>
        /// To Get Change ETA Details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselETAChangeVO> ChangeEtaDetails(string vcn, string vesselName, string etaFrom, string etaTo, string agentNameSearch);

        /// <summary>
        /// To Get Change ETA Details by vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselETAChangeVO> ChangezEtaDetails(string vcn, int? vesselEatChangeId);
    }
}
