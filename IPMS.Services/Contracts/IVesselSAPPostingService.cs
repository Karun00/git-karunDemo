using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IVesselSAPPostingService
    {
        /// <summary>
        ///  To Get Vessels for SAP Posting
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselSAPPostingVO> GetVesselsList(string SearchColumn, string searchValue);

        /// <summary>
        /// To Post Vessel SAP Details
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselSAPPostingVO PostVesselSAP(VesselSAPPostingVO value);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SAPPostingVO> GetSAPVesselPostGrid();
    }
}
