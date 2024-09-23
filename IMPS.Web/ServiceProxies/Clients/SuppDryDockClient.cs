using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using IPMS.Web.ServiceProxies.Contracts;
using System.Collections.Generic;

namespace IPMS.ServiceProxies.Clients
{
    public class SuppDryDockClient : UserClientBase<ISuppDryDockService>, ISuppDryDockService
    {
        public List<SuppDryDockVO> GetSuppDryDockApplicationList()
        {
            return WrapOperationWithException(() => Channel.GetSuppDryDockApplicationList());
        }

        public SuppDryDockVO PostSuppDryDockApplication(SuppDryDockVO data)
        {
            return WrapOperationWithException(() => Channel.PostSuppDryDockApplication(data));
        }

        public SuppDryDockVO PutSuppDryDockApplication(SuppDryDockVO data)
        {
            return WrapOperationWithException(() => Channel.PutSuppDryDockApplication(data));
        }
        public void ApproveSuppDryDock(string SuppDryDockID, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveSuppDryDock(SuppDryDockID, remarks, taskcode));
        }

        public void RejectSuppDryDock(string SuppDryDockID, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectSuppDryDock(SuppDryDockID, remarks, taskcode));
        }
        public void ConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.ConfirmSuppDryDock(SuppDryDockID, comments, taskcode));
        }
        public void CancelSuppDryDock(string SuppDryDockID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.CancelSuppDryDock(SuppDryDockID, comments, taskcode));
        }
        public List<SuppDryDockVO> GetSuppDryDock(int SuppDryDockID)
        {
            return WrapOperationWithException(() => Channel.GetSuppDryDock(SuppDryDockID));
        }
        public List<ServiceRequestVCNDetails> GetSuppVCNDetails(string searchvalue)
        {
            return WrapOperationWithException(() => Channel.GetSuppVCNDetails(searchvalue));
        }

        public SuppDryDockVO GetSuppDryDockVCN(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetSuppDryDockVCN(vcn));
        }

        public List<SubCategory> GetDocumentTypes()
        {
            return WrapOperationWithException(() => Channel.GetDocumentTypes());
        }

        public SuppDryDockVO Cancel(SuppDryDockVO data)
        {
            return WrapOperationWithException(() => Channel.Cancel(data));
        }
        public void ApproveCancelConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveCancelConfirmSuppDryDock(SuppDryDockID, comments, taskcode));
        }
        public void RejectCancelConfirmSuppDryDock(string SuppDryDockID, string comments, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectCancelConfirmSuppDryDock(SuppDryDockID, comments, taskcode));
        }
    }
}