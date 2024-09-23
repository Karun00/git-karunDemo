using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using Core.Repository.Providers.EntityFramework;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IVesselRegistrationService
    {
        /// <summary>
        /// To get vessel reference data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselRegistrationReferenceVO GetVesselRegistrationReferenceData();

        /// <summary>
        /// To get veseel registration data
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselVO> GetVesselRegistrationData();

        /// <summary>
        /// To view vessel registration data from pending tasks 
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselVO> GetzVesselRegistrationData(string vcn);

        /// <summary>
        ///  To add vessel regitration data
        /// </summary>
        /// <param name="vesselData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselVO AddVesselRegistrationDetails(VesselVO vesselData);

        /// <summary>
        /// To modify vessel registration data
        /// </summary>
        /// <param name="vesselData"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselVO ModifyVesselRegistrationDetails(VesselVO vesselData);

        /// <summary>
        ///  To approve vessel registration request
        /// </summary>
        /// <param name="imoNum"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveVesselRegistration(string imoNum, string remarks, string taskCode);

        /// <summary>
        /// To verify vessel registration request
        /// </summary>
        /// <param name="imoNum"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void VerifyVesselRegistration(string imoNum, string remarks, string taskCode);

        /// <summary>
        /// To reject vessel registration request 
        /// </summary>
        /// <param name="imoNum"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectVesselRegistration(string imoNum, string remarks, string taskCode);

        /// <summary>
        /// Get Search Vesseldata
        /// </summary>
        /// <param name="imoNo"></param>
        /// <param name="vesselName"></param>
        /// <param name="portOfRegistry"></param>
        /// <param name="vesselNationality"></param>
        /// <param name="vesselType"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<VesselVO> GetSearchVesselData(string imoNo, string vesselName, string portOfRegistry, string vesselNationality, string vesselType, string callSign);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int CheckIMOExists(string imo);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselVO GetVesselDetailsFromService(string imo);

    }
}
