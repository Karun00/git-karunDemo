using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IServiceRequestService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceRequestVCNDetails> GetVCNDetailsForServiceRequest(string searchValue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceRequestVCNDetails> GetVCNDetails();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceRequest_VO> GetServiceRequestDetails(string frmdate, string todate, string vcnSearch, string vesselName, string MovementType);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<BollardVO> GetBollardAtBerth(string BerthCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ServiceRequest_VO AddServiceRequest(ServiceRequest_VO servicedata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ServiceRequest_VO ModifyServiceRequest(ServiceRequest_VO servicedata);
       
        [OperationContract]
        [FaultContract(typeof(Exception))]
        ServiceRequest_VO Cancel(ServiceRequest_VO servicedata);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceRequest_VO> GetServiceRequest(string serviceid);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ServiceRequestVO GetServiceRequestReferenceData(string PortCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ServiceRequestCureentBerthnBollards> GetCurrentBerthAndBollards(string vcn, string PortCode);
        
        [OperationContract]
        [FaultContract(typeof(Exception))]
        SlotVO GetPreferredSlot(string PreferredDate);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ConfirmServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void CancelServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ConfirmCancelServiceRequest(string ServiceRequestID, string comments, string taskcode);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        void CancelApproveServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void CancelRejectServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void ApproveServiceRequestShifting(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        void RejectServiceRequestShiting(string ServiceRequestID, string comments, string taskcode);

        
    }
}
