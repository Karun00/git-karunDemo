using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using IPMS.Web.ServiceProxies.Contracts;
using System.Collections.Generic;

namespace IPMS.ServiceProxies.Clients
{
    public class SuppDryDockExtensionClient : UserClientBase<ISuppDryDockExtensionService>, ISuppDryDockExtensionService
    {

        public SuppDryDockExtensionVO PostSuppDryDockExtension(SuppDryDockExtensionVO data)
        {
            return WrapOperationWithException(() => Channel.PostSuppDryDockExtension(data));
        }

        public SuppDryDockExtensionVO PutSuppDryDockExtension(SuppDryDockExtensionVO data)
        {
            return WrapOperationWithException(() => Channel.PutSuppDryDockExtension(data));
        }

        public List<ServiceRequestVCNsForDryDockExts> GetSuppVCNDetailsForExtension()
        {
            return WrapOperationWithException(() => Channel.GetSuppVCNDetailsForExtension());
        }
        public AgentVO GetSuppVCNDetailsForExtensionByVCN(string vcn)
        {
            return WrapOperationWithException(() => Channel.GetSuppVCNDetailsForExtensionByVCN(vcn));
        }
        public List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionList()
        {
            return WrapOperationWithException(() => Channel.GetSuppDryDockExtensionList());
        }

        public List<ServiceRequestVCNsForDryDockExts> GetSuppDryDockExtensionByID(string SuppDryDockExtensionID)
        {
            return WrapOperationWithException(() => Channel.GetSuppDryDockExtensionByID(SuppDryDockExtensionID));
        }

        public void ApproveSuppDryDockExtension(string suppdrydockextensionid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.ApproveSuppDryDockExtension(suppdrydockextensionid, remarks, taskcode));
        }

        public void RejectSuppDryDockExtension(string suppdrydockextensionid, string remarks, string taskcode)
        {
            WrapOperationWithException(() => Channel.RejectSuppDryDockExtension(suppdrydockextensionid, remarks, taskcode));
        }
    }
}
