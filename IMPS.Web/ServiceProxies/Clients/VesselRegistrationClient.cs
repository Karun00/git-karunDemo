using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    public class VesselRegistrationClient : UserClientBase<IVesselRegistrationService>, IVesselRegistrationService
    {
        /// <summary>
        ///  To get vessel reference data
        /// </summary>
        /// <returns></returns>
        public VesselRegistrationReferenceVO GetVesselRegistrationReferenceData()
        {
            return WrapOperationWithException(() => Channel.GetVesselRegistrationReferenceData());
        }

        /// <summary>
        /// To get veseel registration data
        /// </summary>
        /// <returns></returns>
        public List<VesselVO> GetVesselRegistrationData()
        {
            return WrapOperationWithException(() => Channel.GetVesselRegistrationData());
        }

        public List<VesselVO> GetSearchVesselData(string imoNo, string vesselName, string portOfRegistry, string vesselNationality, string vesselType, string callSign)
        {
            return WrapOperationWithException(() => Channel.GetSearchVesselData(imoNo, vesselName, portOfRegistry, vesselNationality, vesselType, callSign));
        }

        /// <summary>
        ///  To view vessel registration data from pending tasks 
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<VesselVO> GetzVesselRegistrationData(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetzVesselRegistrationData(vcn));
        }

        /// <summary>
        /// To add vessel regitration data
        /// </summary>
        /// <param name="vesselData"></param>
        /// <returns></returns>
        public VesselVO AddVesselRegistrationDetails(VesselVO vesselData)
        {
            return WrapOperationWithException(() => Channel.AddVesselRegistrationDetails(vesselData));
        }

        /// <summary>
        /// To modify vessel registration data
        /// </summary>
        /// <param name="vesselData"></param>
        /// <returns></returns>
        public VesselVO ModifyVesselRegistrationDetails(VesselVO vesselData)
        {
            return WrapOperationWithException(() => Channel.ModifyVesselRegistrationDetails(vesselData));
        }

        /// <summary>
        /// To approve vessel registration request
        /// </summary>
        /// <param name="imoNum"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void ApproveVesselRegistration(string imoNum, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.ApproveVesselRegistration(imoNum, remarks, taskCode));
        }

        /// <summary>
        /// To verify vessel registration request
        /// </summary>
        /// <param name="imoNum"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void VerifyVesselRegistration(string imoNum, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.VerifyVesselRegistration(imoNum, remarks, taskCode));
        }

        /// <summary>
        /// To reject vessel registration request 
        /// </summary>
        /// <param name="imoNum"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void RejectVesselRegistration(string imoNum, string remarks, string taskCode)
        {
            WrapOperationWithException(() => Channel.RejectVesselRegistration(imoNum, remarks, taskCode));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imo"></param>
        /// <returns></returns>
        public int CheckIMOExists(string imo)
        {
           return WrapOperationWithException(() => Channel.CheckIMOExists(imo));
        }

        public VesselVO GetVesselDetailsFromService(string imo)
        {
            return WrapOperationWithException(() => Channel.GetVesselDetailsFromService(imo));
        }
    }
}