using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface ISupplymentaryServiceRequestService
    {
        /// <summary>
        /// Author  : Sandeep Appana 
        /// Date    : 21st August 2014
        /// Purpose : To Get ServiceType details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategoryVO> GetServiceType();

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 22nd August 2014
        /// Purpose : To Get Supplymentary Service Request details
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppServiceRequestVO> GetSupplymentaryServiceRequestList(string frmdate, string todate, string vcnSearch, string vesselName);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 23rd August 2014
        /// Purpose : To Add Supplmentary Service Request Data
        /// </summary>
        /// <param name="suppServiceRequestVO"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppServiceRequestVO PostSupplymentaryServiceRequest(SuppServiceRequestVO suppServiceRequestVO);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 25th August 2014
        /// Purpose : To Add Supplmentary Service Request Data
        /// </summary>
        /// <param name="suppServiceRequestVO"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppServiceRequestVO ModifySupplymentaryServiceRequest(SuppServiceRequestVO suppServiceRequestVO);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppServiceRequestVO> AllSuppHotWorkInspectionDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppServiceRequestVO> AllSuppDockUnDockTimeDetails();

        /// <summary>
        /// Author  : Srini 
        /// Date    : 15th sep 2014
        /// Purpose : To Get Supplymentary Service Request details by SuppServiceRequestID
    /// </summary>
    /// <param name="SuppServiceRequestID"></param>
    /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppServiceRequestVO GetSupplymentaryServiceRequest(string SuppServiceRequestId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="VCN"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppServiceRequestVO> GetSupplymentaryServiceRequestListVcn(string VCN);
        

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveSupplymentaryServiceRequest(string suppservicerequestid, string remarks, string taskcode);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void VerifySupplymentaryServiceRequest(string suppservicerequestid, string remarks, string taskcode);
        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectSupplymentaryServiceRequest(string suppservicerequestid, string remarks, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<IMDGInformationVO> GetIMDGForSupplymentaryServiceRequest(string vcn);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        VesselCallMovementVO GetEtbEtubFromVcn(string vcn);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        SuppServiceRequestVO Cancel(SuppServiceRequestVO servicedata);

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 3rd June 2014
        /// Purpose: To get VCN by search value 
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="AgentUserID"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceRequestVCNDetails> GetVCNDetailsForSuppServiceRequest(string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="portcode"></param>
        /// <param name="agentUserId"></param>
        /// <param name="toUserId"></param>
        /// <param name="empId"></param>
        /// <param name="frmdate"></param>
        /// <param name="todate"></param>
        /// <param name="vcnSearch"></param>
        /// <param name="vesselName"></param>
        /// <returns></returns>
        
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SuppServiceRequestVO> GetSupplementaryGridDetails( string frmdate, string todate, string vcnSearch, string vesselName);

    }
}
