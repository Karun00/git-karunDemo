using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Web.ServiceProxies;

namespace IPMS.ServiceProxies.Clients
{
    
    public class ServiceRequestClient : UserClientBase<IServiceRequestService>, IServiceRequestService
    {
        public List<ServiceRequestVCNDetails> GetVCNDetailsForServiceRequest(string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetVCNDetailsForServiceRequest(searchValue));
        }
        public List<ServiceRequestVCNDetails> GetVCNDetails()
        {
            return WrapOperationWithException(() => Channel.GetVCNDetails());
        }
        public List<ServiceRequest_VO> GetServiceRequestDetails(string frmdate, string todate, string vcnSearch, string vesselName, string MovementType)
        {
            return WrapOperationWithException(() => Channel.GetServiceRequestDetails(frmdate, todate, vcnSearch, vesselName, MovementType));
        }
        public List<ServiceRequestCureentBerthnBollards> GetCurrentBerthAndBollards(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetCurrentBerthAndBollards(vcn));
        }
        public List<ServiceRequest_VO> AddServiceRequest(ServiceRequest_VO servicedata)
        {
            return WrapOperationWithException(() => Channel.AddServiceRequest(servicedata));
        }
        public List<ServiceRequest_VO> ModifyServiceRequest(ServiceRequest_VO servicedata)
        {
            return WrapOperationWithException(() => Channel.ModifyServiceRequest(servicedata));
        }
        public List<ServiceRequest_VO> Cancel(ServiceRequest_VO servicedata)
        {
            return WrapOperationWithException(() => Channel.Cancel(servicedata));
        }
        public List<ServiceRequest_VO> GetServiceRequest(string serviceid)
        {
            return WrapOperationWithException(() => Channel.GetServiceRequest(serviceid));
        }
        public ServiceRequestVO GetServiceRequestReferenceData()
        {
            return WrapOperationWithException(() => Channel.GetServiceRequestReferenceData());
        }
        public List<BollardVO> GetBollardAtBerth(string BerthCode)
        {
            return WrapOperationWithException(() => Channel.GetBollardAtBerth(BerthCode));
        }
        public SlotVO GetPreferredSlot(string PreferredDate)
        {
            return WrapOperationWithException(() => Channel.GetPreferredSlot(PreferredDate));
        }        

        public void ApproveServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveServiceRequest(ServiceRequestID, comments, taskcode));
        }
        public void RejectServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectServiceRequest(ServiceRequestID, comments, taskcode));
        }
        public void ConfirmServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.ConfirmServiceRequest(ServiceRequestID, comments, taskcode));
        }
        public void CancelServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.CancelServiceRequest(ServiceRequestID, comments, taskcode));
        }
        public void ConfirmCancelServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.ConfirmCancelServiceRequest(ServiceRequestID, comments, taskcode));
        }
        public void CancelApproveServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.CancelApproveServiceRequest(ServiceRequestID, comments, taskcode));
        }
        public void CancelRejectServiceRequest(string ServiceRequestID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.CancelRejectServiceRequest(ServiceRequestID, comments, taskcode));
        }

        public void ApproveServiceRequestShifting(string ServiceRequestID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveServiceRequestShifting(ServiceRequestID, comments, taskcode));
        }
        public void RejectServiceRequestShiting(string ServiceRequestID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectServiceRequestShiting(ServiceRequestID, comments, taskcode));
        }
        
    }
}