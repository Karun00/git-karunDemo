using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using System;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IServiceRequestService : IDisposable
    {
        [OperationContract]
        List<ServiceRequestVCNDetails> GetVCNDetailsForServiceRequest(string searchValue);

        [OperationContract]
        List<ServiceRequestVCNDetails> GetVCNDetails();

        [OperationContract]
        List<ServiceRequest_VO> GetServiceRequestDetails(string frmdate, string todate, string vcnSearch, string vesselName, string MovementType);

        [OperationContract]
        List<ServiceRequestCureentBerthnBollards> GetCurrentBerthAndBollards(string vcn);

        [OperationContract]
        List<ServiceRequest_VO> AddServiceRequest(ServiceRequest_VO servicedata);

        [OperationContract]
        List<ServiceRequest_VO> ModifyServiceRequest(ServiceRequest_VO servicedata);

        [OperationContract]
        List<ServiceRequest_VO> Cancel(ServiceRequest_VO servicedata);

        [OperationContract]
        List<ServiceRequest_VO> GetServiceRequest(string serviceid);

        [OperationContract]
        ServiceRequestVO GetServiceRequestReferenceData();

        [OperationContract]
        List<BollardVO> GetBollardAtBerth(string BerthCode);

        [OperationContract]
        SlotVO GetPreferredSlot(string PreferredDate);

        [OperationContract]
        void ApproveServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        void RejectServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        void ConfirmServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        void CancelServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        void ConfirmCancelServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        void CancelApproveServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        void CancelRejectServiceRequest(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        void ApproveServiceRequestShifting(string ServiceRequestID, string comments, string taskcode);

        [OperationContract]
        void RejectServiceRequestShiting(string ServiceRequestID, string comments, string taskcode);
        

    }
}
